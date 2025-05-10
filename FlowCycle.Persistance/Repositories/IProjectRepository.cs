using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories
{
    public interface IProjectRepository
    {
        Task<ProjectDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
} 