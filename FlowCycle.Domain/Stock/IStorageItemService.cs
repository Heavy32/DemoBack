using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Stock
{
    public interface IStorageItemService
    {
        Task<StockItem> GetById(int id, CancellationToken ct);
        Task<IEnumerable<StockItem>> GetList(CancellationToken ct);
        Task<StockItem> Create(StockItem stockItem, CancellationToken ct);
        Task<StockItem> Update(StockItem stockItem, int id, CancellationToken ct);
        Task Delete(StockItem stockItem, CancellationToken ct);
    }
}
