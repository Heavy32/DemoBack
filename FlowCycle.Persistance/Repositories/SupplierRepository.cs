using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Persistance.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierDao>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Suppliers.ToListAsync(ct);
        }

        public async Task<SupplierDao?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Suppliers.FindAsync(new object[] { id }, ct);
        }

        public async Task<SupplierDao> CreateAsync(SupplierDao supplier, CancellationToken ct = default)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(ct);
            return supplier;
        }

        public async Task<SupplierDao> UpdateAsync(SupplierDao supplier, CancellationToken ct = default)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(ct);
            return supplier;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.Suppliers.FindAsync(new object[] { id }, ct);
            if (entity == null) return false;
            _context.Suppliers.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<SupplierDao> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(c => c.Name == name, ct);
        }
    }
}