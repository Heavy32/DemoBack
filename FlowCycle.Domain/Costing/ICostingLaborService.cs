using FlowCycle.Domain.Costing.Models;

namespace FlowCycle.Domain.Costing
{
    public interface ICostingLaborService
    {
        Task<CostingLabor> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingLabor>> GetListAsync(CostingLaborFilter? filter = null, CancellationToken ct = default);
        Task<CostingLabor> CreateAsync(CostingLabor costingLabor, CancellationToken ct);
        Task<CostingLabor> UpdateAsync(CostingLabor costingLabor, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}