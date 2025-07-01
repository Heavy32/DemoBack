using FlowCycle.Persistance.Storage;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectDao>> GetAllAsync(CancellationToken ct = default);
        Task<ProjectDao?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ProjectDao> CreateAsync(ProjectDao project, CancellationToken ct = default);
        Task<ProjectDao> UpdateAsync(ProjectDao project, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
        Task<ProjectDao> GetByNameAsync(string name, CancellationToken ct = default);
    }
}