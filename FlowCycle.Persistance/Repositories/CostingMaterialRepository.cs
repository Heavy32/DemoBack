using FlowCycle.Persistance.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingMaterialRepository : ICostingMaterialRepository
    {
        private readonly AppDbContext _context;

        public CostingMaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CostingMaterialDao>> GetAllAsync()
        {
            return await _context.CostingMaterials
                .Include(m => m.Costing)
                .Include(m => m.Material)
                .ToListAsync();
        }

        public async Task<CostingMaterialDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.CostingMaterials
                .Include(m => m.Costing)
                .Include(m => m.Material)
                .FirstOrDefaultAsync(m => m.Id == id, ct)
                ?? throw new KeyNotFoundException($"CostingMaterial with ID {id} not found");
        }

        public async Task<IEnumerable<CostingMaterialDao>> GetByCostingIdAsync(int costingId)
        {
            return await _context.CostingMaterials
                .Include(m => m.Costing)
                .Include(m => m.Material)
                .Where(m => m.CostingId == costingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CostingMaterialDao>> GetListAsync(CostingMaterialFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _context.CostingMaterials
                .Include(m => m.Costing)
                .Include(m => m.Material)
                .AsQueryable();

            if (filter != null)
            {
                if (filter.CostingId.HasValue)
                    query = query.Where(x => x.CostingId == filter.CostingId.Value);

                if (filter.MaterialId.HasValue)
                    query = query.Where(x => x.MaterialId == filter.MaterialId.Value);

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "materialid" => filter.SortDescending
                            ? query.OrderByDescending(x => x.MaterialId)
                            : query.OrderBy(x => x.MaterialId),
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

        public async Task<CostingMaterialDao> CreateAsync(CostingMaterialDao costingMaterial, CancellationToken ct)
        {
            _context.CostingMaterials.Add(costingMaterial);
            await _context.SaveChangesAsync(ct);
            return costingMaterial;
        }

        public async Task<CostingMaterialDao> UpdateAsync(CostingMaterialDao costingMaterial, CancellationToken ct)
        {
            _context.CostingMaterials.Update(costingMaterial);
            await _context.SaveChangesAsync(ct);
            return costingMaterial;
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costingMaterial = await GetByIdAsync(id, ct);
            _context.CostingMaterials.Remove(costingMaterial);
            await _context.SaveChangesAsync(ct);
        }
    }
}