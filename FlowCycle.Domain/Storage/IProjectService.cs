using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Storage
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct = default);
        Task<Project?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Project> CreateAsync(Project project, CancellationToken ct = default);
        Task<Project> UpdateAsync(Project project, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}