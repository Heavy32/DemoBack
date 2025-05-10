using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name, ct);
        }
    }
} 