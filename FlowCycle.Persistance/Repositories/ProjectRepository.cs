using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDao>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Projects.ToListAsync(ct);
        }

        public async Task<ProjectDao?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Projects.FindAsync(new object[] { id }, ct);
        }

        public async Task<ProjectDao> CreateAsync(ProjectDao project, CancellationToken ct = default)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync(ct);
            return project;
        }

        public async Task<ProjectDao> UpdateAsync(ProjectDao project, CancellationToken ct = default)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync(ct);
            return project;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.Projects.FindAsync(new object[] { id }, ct);
            if (entity == null) return false;
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<ProjectDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(c => c.Name == name, ct);
        }
    }
}