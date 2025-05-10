using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface IStockItemRepository
    {
        Task<StockItemDao> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<StockItemDao>> GetListAsync(StockItemFilterDao? filter = null, CancellationToken ct = default);
        Task<StockItemDao> CreateAsync(StockItemDao stockItem, CancellationToken ct);
        Task<StockItemDao> UpdateAsync(StockItemDao stockItem, CancellationToken ct);
        Task DeleteAsync(StockItemDao stockItem, CancellationToken ct);
    }
} 