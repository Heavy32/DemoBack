using FlowCycle.Persistance.Storage;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDao>> GetAllAsync(CancellationToken ct = default);
        Task<CategoryDao?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<CategoryDao> CreateAsync(CategoryDao category, CancellationToken ct = default);
        Task<CategoryDao> UpdateAsync(CategoryDao category, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<CategoryDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
}