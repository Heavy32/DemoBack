using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Storage;
using ClosedXML.Excel;
using FlowCycle.Domain.Stock.Exceptions;

namespace FlowCycle.Domain.Stock
{
    public class StockItemImportService : IStockItemImportService
    {
        private readonly IStockItemRepository _repository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public StockItemImportService(
            IStockItemRepository repository,
            ISupplierRepository supplierRepository,
            ICategoryRepository categoryRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _repository = repository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockItem>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1); // First worksheet
            var rows = worksheet.RowsUsed().Skip(2); // Skip header row

            var stockItems = new List<StockItemDao>();

            foreach (var row in rows)
            {
                var supplierName = row.Cell(12).GetString();
                var categoryName = row.Cell(4).GetString();
                var projectName = row.Cell(13).GetString();

                // Find or validate inner entities
                var supplierDao = await _supplierRepository.GetByNameAsync(supplierName, ct);
                if (supplierDao == null)
                {
                    throw new EntityNotFoundException("Supplier", supplierName);
                }

                var categoryDao = await _categoryRepository.GetByNameAsync(categoryName, ct);
                if (categoryDao == null)
                {
                    throw new EntityNotFoundException("Category", categoryName);
                }

                var projectDao = await _projectRepository.GetByNameAsync(projectName, ct);
                if (projectDao == null)
                {
                    throw new EntityNotFoundException("Project", projectName);
                }

                var rawDate = row.Cell(11).GetValue<DateTime>();
                var receiptDate = rawDate.Kind == DateTimeKind.Utc
                    ? rawDate
                    : DateTime.SpecifyKind(rawDate, DateTimeKind.Utc);

                var stockItem = new StockItemDao
                {
                    Name = row.Cell(3).GetString(),
                    Code = row.Cell(5).GetString(),
                    Amount = row.Cell(7).GetValue<int>(),
                    SinglePrice = row.Cell(8).GetValue<double>(),
                    Measure = row.Cell(6).GetString(),
                    Supplier = supplierDao,
                    Category = categoryDao,
                    Project = projectDao,
                    ReceiptDate = receiptDate,
                    VAT = row.Cell(9).GetValue<double>(),
                    IsArchived = false,
                    TotalPrice = row.Cell(10).GetValue<double>(),
                };

                stockItems.Add(stockItem);
            }

            // Save all items to the database
            foreach (var item in stockItems)
            {
                var safeReceiptDate = item.ReceiptDate.Kind == DateTimeKind.Utc
                    ? item.ReceiptDate
                    : DateTime.SpecifyKind(item.ReceiptDate, DateTimeKind.Utc);
                var dao = new StockItemDao
                {
                    Name = item.Name,
                    Code = item.Code,
                    Amount = item.Amount,
                    SinglePrice = item.SinglePrice,
                    Measure = item.Measure,
                    Supplier = item.Supplier,
                    Category = item.Category,
                    Project = item.Project,
                    ReceiptDate = safeReceiptDate,
                    VAT = item.VAT,
                    IsArchived = item.IsArchived,
                    TotalPrice = item.TotalPrice,
                };
                await _repository.CreateAsync(dao, ct);
            }

            return stockItems.Select(_mapper.Map<StockItem>);
        }
    }
} 