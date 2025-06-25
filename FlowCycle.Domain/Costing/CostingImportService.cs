using AutoMapper;
using ClosedXML.Excel;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.UnitOfWork;

namespace FlowCycle.Domain.Costing
{
    public class CostingImportService : ICostingImportService
    {
        private readonly ICostingRepository _costingRepository;
        private readonly ICostingMaterialRepository _costingMaterialRepository;
        private readonly ICostingLaborRepository _costingLaborRepository;
        private readonly ICostingOverheadRepository _costingOverheadRepository;
        private readonly ICostingMaterialTypeRepository _costingMaterialTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CostingImportService(
            ICostingRepository costingRepository,
            ICostingMaterialRepository costingMaterialRepository,
            ICostingLaborRepository costingLaborRepository,
            ICostingOverheadRepository costingOverheadRepository,
            IMapper mapper,
            ICostingMaterialTypeRepository costingMaterialTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _costingRepository = costingRepository;
            _costingMaterialRepository = costingMaterialRepository;
            _costingLaborRepository = costingLaborRepository;
            _costingOverheadRepository = costingOverheadRepository;
            _costingMaterialTypeRepository = costingMaterialTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CostingModel>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync(ct);
            try
            {
                using var workbook = new XLWorkbook(fileStream);
                // Process first page for costing data
                var costings = await ProcessCostingPage(workbook.Worksheet(1), ct);
                // Process subsequent pages for related entities
                for (int pageNumber = 2; pageNumber <= workbook.Worksheets.Count; pageNumber++)
                {
                    var worksheet = workbook.Worksheet(pageNumber);
                    await ProcessEntityPage(worksheet, ct);
                }
                await _unitOfWork.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
                return costings.Select(c => _mapper.Map<CostingModel>(c)).ToList();
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }

        private async Task<List<CostingDao>> ProcessCostingPage(IXLWorksheet worksheet, CancellationToken ct)
        {
            List<CostingDao> result = new List<CostingDao>();
            for (int row = 3; worksheet.Cell(row, 3).GetValue<string>() != ""; row++)
            {
                var projectName = worksheet.Cell(row, 10).GetValue<string>();
                var productCode = worksheet.Cell(row, 4).GetValue<string>();
                var costingTypeName = worksheet.Cell(row, 5).GetValue<string>();
                var productName = worksheet.Cell(row, 3).GetValue<string>();

                var uom = worksheet.Cell(row, 6).GetValue<string>();
                var quantity = worksheet.Cell(row, 7).GetValue<decimal>();
                var unitCost = worksheet.Cell(row, 8).GetValue<decimal>();
                var totalCost = worksheet.Cell(row, 9).GetValue<decimal>();

                var project = await _costingRepository.GetProjectByNameAsync(projectName, ct);
                var costingType = await _costingRepository.GetCostingTypeByNameAsync(costingTypeName, ct);

                var costing = new CostingDao
                {
                    ProjectId = project.Id,
                    CostingTypeId = costingType.Id,
                    ProductName = productName,
                    ProductCode = productCode,
                    Uom = uom,
                    Quantity = quantity,
                    UnitCost = unitCost,
                    TotalCost = totalCost,
                    Project = project,
                    CostingType = costingType,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var newcosting = await _costingRepository.CreateAsync(costing, ct);
                result.Add(newcosting);
            }

            return result;
        }


        private async Task ProcessEntityPage(IXLWorksheet worksheet, CancellationToken ct)
        {
            var startRow = 2; // Skip header row
            var endRow = worksheet.LastRowUsed().RowNumber();

            var costingName = worksheet.Cell(3, 3).GetValue<string>();
            var costing = await _costingRepository.GetByNameAsync(costingName, ct);
            for (var i = startRow; i < endRow; i++)
            {
                var pageTitle = worksheet.Cell(i, 1).GetValue<string>();
                switch (pageTitle)
                {
                    case "Материалы":
                        await ProcessMaterialsSection(worksheet, i, endRow, costing, ct);
                        break;
                    case "Трудозатраты":
                        await ProcessLaborSection(worksheet, i, endRow, costing, ct);
                        break;
                    case "Накладные расходы":
                        await ProcessOverheadSection(worksheet, i, endRow, costing, ct);
                        break;
                }
            }

        }

        private async Task ProcessMaterialsSection(IXLWorksheet worksheet, int startRow, int endRow, CostingDao costing, CancellationToken ct)
        {
            // Dynamically determine startRow and endRow
            int firstDataRow = startRow;
            while (string.IsNullOrWhiteSpace(worksheet.Cell(firstDataRow, 1).GetValue<string>()) && firstDataRow <= endRow)
                firstDataRow++;
            int lastDataRow = firstDataRow;
            while (!string.IsNullOrWhiteSpace(worksheet.Cell(lastDataRow, 1).GetValue<string>()))
                lastDataRow++;
            lastDataRow--;

            firstDataRow += 2;

            for (int row = firstDataRow; row <= lastDataRow; row++)
            {
                var materialTypeName = worksheet.Cell(row, 4).GetValue<string>();
                if (string.IsNullOrWhiteSpace(materialTypeName)) continue;

                var materialType = await _costingMaterialTypeRepository.GetByNameAsync(materialTypeName.ToLower(), ct);
                if (materialType == null) continue;

                var material = new CostingMaterialDao
                {
                    Costing = costing,
                    CostingMaterialType = materialType,
                    Uom = worksheet.Cell(row, 5).GetValue<string>(),
                    UnitPrice = worksheet.Cell(row, 6).GetValue<decimal>(),
                    QtyPerProduct = worksheet.Cell(row, 7).GetValue<decimal>(),
                    TotalValue = worksheet.Cell(row, 8).GetValue<decimal>()
                };

                await _costingMaterialRepository.CreateAsync(material, ct);
            }
        }

        private async Task ProcessLaborSection(IXLWorksheet worksheet, int startRow, int endRow, CostingDao costing, CancellationToken ct)
        {
            // Dynamically determine startRow and endRow
            int firstDataRow = startRow;
            while (string.IsNullOrWhiteSpace(worksheet.Cell(firstDataRow, 1).GetValue<string>()) && firstDataRow <= endRow)
                firstDataRow++;
            int lastDataRow = firstDataRow;
            while (!string.IsNullOrWhiteSpace(worksheet.Cell(lastDataRow, 1).GetValue<string>()))
                lastDataRow++;
            lastDataRow--;

            firstDataRow += 2;

            for (int row = firstDataRow; row <= lastDataRow; row++)
            {
                var laborName = worksheet.Cell(row, 3).GetValue<string>();
                if (string.IsNullOrWhiteSpace(laborName)) continue;

                var labor = new CostingLaborDao
                {
                    Costing = costing,
                    LaborName = laborName,
                    Hours = worksheet.Cell(row, 4).GetValue<decimal>(),
                    HourRate = worksheet.Cell(row, 5).GetValue<decimal>(),
                    TotalValue = worksheet.Cell(row, 6).GetValue<decimal>()
                };

                await _costingLaborRepository.CreateAsync(labor, ct);
            }
        }

        private async Task ProcessOverheadSection(IXLWorksheet worksheet, int startRow, int endRow, CostingDao costing, CancellationToken ct)
        {
            // Dynamically determine startRow and endRow
            int firstDataRow = startRow;
            while (string.IsNullOrWhiteSpace(worksheet.Cell(firstDataRow, 1).GetValue<string>()) && firstDataRow <= endRow)
                firstDataRow++;
            int lastDataRow = firstDataRow;
            while (!string.IsNullOrWhiteSpace(worksheet.Cell(lastDataRow, 1).GetValue<string>()))
                lastDataRow++;
            lastDataRow--;

            firstDataRow += 2;

            for (int row = firstDataRow; row <= lastDataRow; row++)
            {
                var overheadTypeName = worksheet.Cell(row, 4).GetValue<string>();
                if (string.IsNullOrWhiteSpace(overheadTypeName)) continue;

                var overheadType = await _costingOverheadRepository.GetByNameAsync(overheadTypeName, ct);
                if (overheadType == null) continue;

                var overhead = new CostingOverheadDao
                {
                    Costing = costing,
                    OverheadTypeId = overheadType.Id,
                    Name = worksheet.Cell(row, 3).GetValue<string>(),
                    Uom = worksheet.Cell(row, 5).GetValue<string>(),
                    UnitPrice = worksheet.Cell(row, 7).GetValue<decimal>(),
                    QtyPerProduct = worksheet.Cell(row, 6).GetValue<decimal>(),
                    TotalValue = worksheet.Cell(row, 8).GetValue<decimal>()
                };

                await _costingOverheadRepository.CreateAsync(overhead, ct);
            }
        }
    }
}