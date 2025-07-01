using FlowCycle.Domain.Storage.Models;
using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Storage
{
    public interface IStorageItemService
    {
        Task<IEnumerable<StorageItem>> GetAllAsync(CancellationToken ct = default);
        Task<StorageItem?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<StorageItem> CreateAsync(StorageItem item, CancellationToken ct = default);
        Task<StorageItem> UpdateAsync(StorageItem item, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
