using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICostingMaterialTypeRepository
    {
        Task<CostingMaterialTypeDao?> GetByNameAsync(string name, CancellationToken ct);
        Task<CostingMaterialTypeDao?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingMaterialTypeDao>> GetAllAsync(CancellationToken ct);
        Task<CostingMaterialTypeDao> CreateAsync(CostingMaterialTypeDao materialType, CancellationToken ct);
        Task<CostingMaterialTypeDao> UpdateAsync(CostingMaterialTypeDao materialType, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}