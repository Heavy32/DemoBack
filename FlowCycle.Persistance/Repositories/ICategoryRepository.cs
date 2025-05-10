using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface ICategoryRepository
    {
        Task<CategoryDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
} 