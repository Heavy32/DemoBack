using FlowCycle.Domain.Storage.Models;

namespace FlowCycle.Domain.Costing
{
    public interface ICostingService
    {
        Task<CostingModel> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<CostingModel>> GetListAsync(CostingFilter? filter = null, CancellationToken ct = default);
        Task<CostingModel> CreateAsync(CostingModel costing, CancellationToken ct);
        Task<CostingModel> UpdateAsync(CostingModel costing, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}