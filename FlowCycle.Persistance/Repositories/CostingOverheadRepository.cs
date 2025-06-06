using FlowCycle.Persistance.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingOverheadRepository : ICostingOverheadRepository
    {
        private readonly AppDbContext _context;

        public CostingOverheadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CostingOverheadDao>> GetAllAsync()
        {
            return await _context.CostingOverheads
                .Include(o => o.Costing)
                .ToListAsync();
        }

        public async Task<CostingOverheadDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.CostingOverheads
                .Include(o => o.Costing)
                .FirstOrDefaultAsync(o => o.Id == id, ct)
                ?? throw new KeyNotFoundException($"CostingOverhead with ID {id} not found");
        }

        public async Task<IEnumerable<CostingOverheadDao>> GetByCostingIdAsync(int costingId)
        {
            return await _context.CostingOverheads
                .Include(o => o.Costing)
                .Where(o => o.CostingId == costingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CostingOverheadDao>> GetListAsync(CostingOverheadFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _context.CostingOverheads
                .Include(o => o.Costing)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.CostingId.HasValue)
                    query = query.Where(x => x.CostingId == filter.CostingId.Value);

                if (!string.IsNullOrWhiteSpace(filter.OverheadName))
                    query = query.Where(x => x.OverheadName.Contains(filter.OverheadName));

                if (!string.IsNullOrWhiteSpace(filter.OverheadType))
                    query = query.Where(x => x.OverheadType == filter.OverheadType);

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "overheadname" => filter.SortDescending
                            ? query.OrderByDescending(x => x.OverheadName)
                            : query.OrderBy(x => x.OverheadName),
                        "overheadtype" => filter.SortDescending
                            ? query.OrderByDescending(x => x.OverheadType)
                            : query.OrderBy(x => x.OverheadType),
                        "costvalue" => filter.SortDescending
                            ? query.OrderByDescending(x => x.CostValue)
                            : query.OrderBy(x => x.CostValue),
                        "note" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Note)
                            : query.OrderBy(x => x.Note),
                        _ => query
                    };
                }
            }

            return await query.ToListAsync(ct);
        }

        public async Task<CostingOverheadDao> CreateAsync(CostingOverheadDao overhead, CancellationToken ct)
        {
            _context.CostingOverheads.Add(overhead);
            await _context.SaveChangesAsync(ct);
            return overhead;
        }

        public async Task<CostingOverheadDao> UpdateAsync(CostingOverheadDao overhead, CancellationToken ct)
        {
            var existing = await _context.CostingOverheads.FindAsync(new object[] { overhead.Id }, ct);
            if (existing == null)
                throw new KeyNotFoundException($"CostingOverhead with ID {overhead.Id} not found");

            _context.Entry(existing).CurrentValues.SetValues(overhead);
            await _context.SaveChangesAsync(ct);
            return existing;
        }

        public async Task DeleteAsync(CostingOverheadDao overhead, CancellationToken ct)
        {
            _context.CostingOverheads.Remove(overhead);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CostingOverheads.AnyAsync(o => o.Id == id);
        }
    }
}