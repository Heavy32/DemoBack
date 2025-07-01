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

        public async Task<IEnumerable<CategoryDao>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Categories.ToListAsync(ct);
        }

        public async Task<CategoryDao?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Categories.FindAsync(new object[] { id }, ct);
        }

        public async Task<CategoryDao> CreateAsync(CategoryDao category, CancellationToken ct = default)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(ct);
            return category;
        }

        public async Task<CategoryDao> UpdateAsync(CategoryDao category, CancellationToken ct = default)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(ct);
            return category;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.Categories.FindAsync(new object[] { id }, ct);
            if (entity == null) return false;
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<CategoryDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name, ct);
        }
    }
}