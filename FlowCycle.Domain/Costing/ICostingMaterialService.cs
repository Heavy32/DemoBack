using FlowCycle.Domain.Costing.Models;

namespace FlowCycle.Domain.Costing
{
    public interface ICostingMaterialService
    {
        Task<CostingMaterial> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingMaterial>> GetListAsync(CostingMaterialFilter? filter = null, CancellationToken ct = default);
        Task<CostingMaterial> CreateAsync(CostingMaterial costingMaterial, CancellationToken ct);
        Task<CostingMaterial> UpdateAsync(CostingMaterial costingMaterial, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}