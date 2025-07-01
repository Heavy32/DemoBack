using FlowCycle.Persistance.Storage;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierDao>> GetAllAsync(CancellationToken ct = default);
        Task<SupplierDao?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<SupplierDao> CreateAsync(SupplierDao supplier, CancellationToken ct = default);
        Task<SupplierDao> UpdateAsync(SupplierDao supplier, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<SupplierDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
}