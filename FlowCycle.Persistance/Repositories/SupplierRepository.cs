
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SupplierDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Name == name, ct);
        }
    }
} 