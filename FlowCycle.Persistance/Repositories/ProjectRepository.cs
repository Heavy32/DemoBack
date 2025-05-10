
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Name == name, ct);
        }
    }
} 