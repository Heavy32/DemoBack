using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICostingRepository
    {
        Task<CostingDao> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingDao>> GetListAsync(CostingFilterDao? filter = null, CancellationToken ct = default);
        Task<CostingDao> CreateAsync(CostingDao costing, CancellationToken ct);
        Task<CostingDao> UpdateAsync(CostingDao costing, CancellationToken ct);
        Task DeleteAsync(CostingDao costing, CancellationToken ct);
    }
}