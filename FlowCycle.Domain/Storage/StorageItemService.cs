using AutoMapper;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Domain.Storage
{
    public class StorageItemService : IStorageItemService
    {
        private readonly IStorageItemRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProjectRepository _projectRepository;

        public StorageItemService(IStorageItemRepository repository, IMapper mapper, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IProjectRepository projectRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<StorageItem>> GetAllAsync(CancellationToken ct = default)
        {
            var daos = await _repository.GetAllAsync(ct);
            return daos.Select(_mapper.Map<StorageItem>);
        }

        public async Task<StorageItem?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return dao == null ? null : _mapper.Map<StorageItem>(dao);
        }

        public async Task<StorageItem> CreateAsync(StorageItem item, CancellationToken ct = default)
        {
            // Fetch related entities by ID
            var category = await _categoryRepository.GetByIdAsync(item.CategoryId, ct);
            var supplier = await _supplierRepository.GetByIdAsync(item.SupplierId, ct);
            var project = await _projectRepository.GetByIdAsync(item.ProjectId, ct);
            var dao = _mapper.Map<StorageItemDao>(item);
            dao.Category = category;
            dao.Supplier = supplier;
            dao.Project = project;
            var created = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<StorageItem>(created);
        }

        public async Task<StorageItem> UpdateAsync(StorageItem item, CancellationToken ct = default)
        {
            // Fetch related entities by ID
            var category = await _categoryRepository.GetByIdAsync(item.CategoryId, ct);
            var supplier = await _supplierRepository.GetByIdAsync(item.SupplierId, ct);
            var project = await _projectRepository.GetByIdAsync(item.ProjectId, ct);
            var dao = _mapper.Map<StorageItemDao>(item);
            dao.Category = category;
            dao.Supplier = supplier;
            dao.Project = project;
            var updated = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<StorageItem>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }

        public async Task<IEnumerable<StorageItem>> GetListAsync(StorageItemFilter? filter = null, CancellationToken ct = default)
        {
            var filterDao = filter != null ? _mapper.Map<StorageItemFilterDao>(filter) : null;
            var daos = await _repository.GetListAsync(filterDao, ct);
            return daos.Select(_mapper.Map<StorageItem>);
        }
    }
}
