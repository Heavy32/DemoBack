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

        public StorageItemService(IStorageItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var dao = _mapper.Map<StorageItemDao>(item);
            var created = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<StorageItem>(created);
        }

        public async Task<StorageItem> UpdateAsync(StorageItem item, CancellationToken ct = default)
        {
            var dao = _mapper.Map<StorageItemDao>(item);
            var updated = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<StorageItem>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }
    }
}
