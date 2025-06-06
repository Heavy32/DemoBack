using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICostingMaterialRepository
    {
        Task<CostingMaterialDao> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingMaterialDao>> GetListAsync(CostingMaterialFilterDao? filter = null, CancellationToken ct = default);
        Task<CostingMaterialDao> CreateAsync(CostingMaterialDao costingMaterial, CancellationToken ct);
        Task<CostingMaterialDao> UpdateAsync(CostingMaterialDao costingMaterial, CancellationToken ct);
        Task DeleteAsync(CostingMaterialDao costingMaterial, CancellationToken ct);
    }
} 