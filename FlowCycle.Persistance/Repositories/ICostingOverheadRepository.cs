using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICostingOverheadRepository
    {
        Task<CostingOverheadDao> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingOverheadDao>> GetListAsync(CostingOverheadFilterDao? filter = null, CancellationToken ct = default);
        Task<CostingOverheadDao> CreateAsync(CostingOverheadDao overhead, CancellationToken ct);
        Task<CostingOverheadDao> UpdateAsync(CostingOverheadDao overhead, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}