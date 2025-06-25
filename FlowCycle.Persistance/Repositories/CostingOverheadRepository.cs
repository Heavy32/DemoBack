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
                .Include(o => o.OverheadType)
                .ToListAsync();
        }

        public async Task<CostingOverheadDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.CostingOverheads
                .Include(o => o.Costing)
                .Include(o => o.OverheadType)
                .FirstOrDefaultAsync(o => o.Id == id, ct)
                ?? throw new KeyNotFoundException($"CostingOverhead with ID {id} not found");
        }

        public async Task<IEnumerable<CostingOverheadDao>> GetByCostingIdAsync(int costingId)
        {
            return await _context.CostingOverheads
                .Include(o => o.Costing)
                .Include(o => o.OverheadType)
                .Where(o => o.CostingId == costingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CostingOverheadDao>> GetListAsync(CostingOverheadFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _context.CostingOverheads
                .Include(o => o.Costing)
                .Include(o => o.OverheadType)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.CostingId.HasValue)
                    query = query.Where(x => x.CostingId == filter.CostingId.Value);

                if (filter.OverheadTypeId.HasValue)
                    query = query.Where(x => x.OverheadTypeId == filter.OverheadTypeId.Value);

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "overheadtypeid" => filter.SortDescending
                            ? query.OrderByDescending(x => x.OverheadTypeId)
                            : query.OrderBy(x => x.OverheadTypeId),
                        "uom" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Uom)
                            : query.OrderBy(x => x.Uom),
                        "unitprice" => filter.SortDescending
                            ? query.OrderByDescending(x => x.UnitPrice)
                            : query.OrderBy(x => x.UnitPrice),
                        "qtyperproduct" => filter.SortDescending
                            ? query.OrderByDescending(x => x.QtyPerProduct)
                            : query.OrderBy(x => x.QtyPerProduct),
                        "totalvalue" => filter.SortDescending
                            ? query.OrderByDescending(x => x.TotalValue)
                            : query.OrderBy(x => x.TotalValue),
                        "note" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Note)
                            : query.OrderBy(x => x.Note),
                        _ => query
                    };
                }
            }

            return await query.ToListAsync(ct);
        }

        public async Task<CostingOverheadDao> CreateAsync(CostingOverheadDao costingOverhead, CancellationToken ct)
        {
            _context.CostingOverheads.Add(costingOverhead);
            return costingOverhead;
        }

        public async Task<CostingOverheadDao> UpdateAsync(CostingOverheadDao costingOverhead, CancellationToken ct)
        {
            _context.CostingOverheads.Update(costingOverhead);
            return costingOverhead;
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costingOverhead = await GetByIdAsync(id, ct);
            _context.CostingOverheads.Remove(costingOverhead);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CostingOverheads.AnyAsync(o => o.Id == id);
        }

        public async Task<OverheadTypeDao?> GetByNameAsync(string name, CancellationToken ct)
        {
            return await _context.OverheadTypes
                .FirstOrDefaultAsync(o => o.Name == name, ct);
        }
    }
}