using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface ISupplierRepository
    {
        Task<SupplierDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
} 