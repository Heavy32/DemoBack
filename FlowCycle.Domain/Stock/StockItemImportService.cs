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
            var rows = worksheet.RowsUsed().Skip(1); // Skip header row

            var stockItems = new List<StockItem>();

            foreach (var row in rows)
            {
                var supplierName = row.Cell(6).GetString();
                var categoryName = row.Cell(7).GetString();
                var projectName = row.Cell(8).GetString();

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

                var supplier = _mapper.Map<Supplier>(supplierDao);
                var category = _mapper.Map<Category>(categoryDao);
                var project = _mapper.Map<Project>(projectDao);

                var stockItem = new StockItem
                {
                    Name = row.Cell(1).GetString(),
                    Code = row.Cell(2).GetString(),
                    Amount = row.Cell(3).GetValue<int>(),
                    SinglePrice = row.Cell(4).GetValue<double>(),
                    Measure = row.Cell(5).GetString(),
                    Supplier = supplier,
                    Category = category,
                    Project = project,
                    ReceiptDate = row.Cell(9).GetValue<DateTime>(),
                    VAT = row.Cell(10).GetValue<double>(),
                    IsArchived = false
                };

                stockItems.Add(stockItem);
            }

            // Save all items to the database
            foreach (var item in stockItems)
            {
                var dao = _mapper.Map<StockItemDao>(item);
                await _repository.CreateAsync(dao, ct);
            }

            return stockItems;
        }
    }
} 