using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Storage;
using ClosedXML.Excel;
using FlowCycle.Domain.Storage.Exceptions;

namespace FlowCycle.Domain.Storage
{
    public class StorageItemImportService : IStorageItemImportService
    {
        private readonly IStorageItemRepository _repository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public StorageItemImportService(
            IStorageItemRepository repository,
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

        public async Task<IEnumerable<StorageItem>> ImportFromExcelAsync(Stream fileStream, CancellationToken ct = default)
        {
            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1); // First worksheet
            var rows = worksheet.RowsUsed().Skip(2); // Skip header row

            var StorageItems = new List<StorageItemDao>();

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

                var rawArrivalDate = row.Cell(11).GetValue<DateTime>();
                var arrivalDate = rawArrivalDate.Kind == DateTimeKind.Utc
                    ? rawArrivalDate
                    : DateTime.SpecifyKind(rawArrivalDate, DateTimeKind.Utc);

                // Optionally get ExpirationDate from Excel, or set a default
                var expirationDate = DateTime.MinValue; // TODO: update if you have a column for this

                var StorageItem = new StorageItemDao
                {
                    Name = row.Cell(3).GetString(),
                    Code = row.Cell(5).GetString(),
                    Amount = row.Cell(7).GetValue<int>(),
                    SinglePrice = row.Cell(8).GetValue<double>(),
                    Measure = row.Cell(6).GetString(),
                    Supplier = supplierDao,
                    Category = categoryDao,
                    Project = projectDao,
                    ArrivalDate = arrivalDate,
                    ExpirationDate = expirationDate,
                    VAT = row.Cell(9).GetValue<double>(),
                    IsArchived = false,
                    TotalPrice = row.Cell(10).GetValue<double>(),
                    ArchivedCount = 0,
                    UpdateDate = DateTime.UtcNow,
                    CreateDate = DateTime.UtcNow
                };

                StorageItems.Add(StorageItem);
            }

            // Save all items to the database
            foreach (var item in StorageItems)
            {
                var safeArrivalDate = item.ArrivalDate.Kind == DateTimeKind.Utc
                    ? item.ArrivalDate
                    : DateTime.SpecifyKind(item.ArrivalDate, DateTimeKind.Utc);
                var dao = new StorageItemDao
                {
                    Name = item.Name,
                    Code = item.Code,
                    Amount = item.Amount,
                    SinglePrice = item.SinglePrice,
                    Measure = item.Measure,
                    Supplier = item.Supplier,
                    Category = item.Category,
                    Project = item.Project,
                    ArrivalDate = safeArrivalDate,
                    ExpirationDate = item.ExpirationDate,
                    VAT = item.VAT,
                    IsArchived = item.IsArchived,
                    TotalPrice = item.TotalPrice,
                    ArchivedCount = item.ArchivedCount,
                    UpdateDate = item.UpdateDate,
                    CreateDate = item.CreateDate
                };
                await _repository.CreateAsync(dao, ct);
            }

            return StorageItems.Select(_mapper.Map<StorageItem>);
        }
    }
}