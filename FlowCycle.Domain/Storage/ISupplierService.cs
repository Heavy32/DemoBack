using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Storage
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken ct = default);
        Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Supplier> CreateAsync(Supplier supplier, CancellationToken ct = default);
        Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}