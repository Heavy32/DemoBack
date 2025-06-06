using FlowCycle.Domain.Costing.Models;

namespace FlowCycle.Domain.Costing
{
    public interface ICostingOverheadService
    {
        Task<CostingOverhead> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingOverhead>> GetListAsync(CostingOverheadFilter? filter = null, CancellationToken ct = default);
        Task<CostingOverhead> CreateAsync(CostingOverhead overhead, CancellationToken ct);
        Task<CostingOverhead> UpdateAsync(CostingOverhead overhead, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}