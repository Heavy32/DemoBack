using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Storage
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
        Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Category> CreateAsync(Category category, CancellationToken ct = default);
        Task<Category> UpdateAsync(Category category, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}