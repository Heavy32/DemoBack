using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Stock
{
    public interface IStorageItemService
    {
        Task<StockItem> GetById(int id, CancellationToken ct);
    }
}
