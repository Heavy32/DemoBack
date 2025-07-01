using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface IStorageItemRepository
    {
        Task<IEnumerable<StorageItemDao>> GetAllAsync(CancellationToken ct = default);
        Task<StorageItemDao?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<StorageItemDao> CreateAsync(StorageItemDao item, CancellationToken ct = default);
        Task<StorageItemDao> UpdateAsync(StorageItemDao item, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}