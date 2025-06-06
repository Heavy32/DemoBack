using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICostingLaborRepository
    {
        Task<CostingLaborDao> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingLaborDao>> GetListAsync(CostingLaborFilterDao? filter = null, CancellationToken ct = default);
        Task<CostingLaborDao> CreateAsync(CostingLaborDao costingLabor, CancellationToken ct);
        Task<CostingLaborDao> UpdateAsync(CostingLaborDao costingLabor, CancellationToken ct);
        Task DeleteAsync(CostingLaborDao costingLabor, CancellationToken ct);
    }
}