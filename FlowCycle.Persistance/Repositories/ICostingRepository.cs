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
        Task DeleteAsync(int id, CancellationToken ct);
        Task<ProjectDao> GetProjectByNameAsync(string name, CancellationToken ct);
        Task<CostingTypeDao> GetCostingTypeByNameAsync(string name, CancellationToken ct);
        Task<CostingDao?> GetByNameAsync(string productName, CancellationToken ct);
    }
}