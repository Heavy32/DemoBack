using FlowCycle.Persistance.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingMaterialRepository : ICostingMaterialRepository
    {
        private readonly AppDbContext _dbContext;

        public CostingMaterialRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CostingMaterialDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _dbContext.CostingMaterials
                .FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new KeyNotFoundException($"CostingMaterial with ID {id} not found");
        }

        public async Task<IEnumerable<CostingMaterialDao>> GetListAsync(CostingMaterialFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _dbContext.CostingMaterials.AsQueryable();

            if (filter != null)
            {
                if (filter.CostingId.HasValue)
                    query = query.Where(x => x.CostingId == filter.CostingId.Value);

                if (!string.IsNullOrWhiteSpace(filter.MaterialName))
                    query = query.Where(x => x.MaterialName.Contains(filter.MaterialName));

                if (!string.IsNullOrWhiteSpace(filter.MaterialCode))
                    query = query.Where(x => x.MaterialCode.Contains(filter.MaterialCode));

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "materialname" => filter.SortDescending
                            ? query.OrderByDescending(x => x.MaterialName)
                            : query.OrderBy(x => x.MaterialName),
                        "materialcode" => filter.SortDescending
                            ? query.OrderByDescending(x => x.MaterialCode)
                            : query.OrderBy(x => x.MaterialCode),
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
            _dbContext.CostingMaterials.Add(costingMaterial);
            await _dbContext.SaveChangesAsync(ct);
            return costingMaterial;
        }

        public async Task<CostingMaterialDao> UpdateAsync(CostingMaterialDao costingMaterial, CancellationToken ct)
        {
            _dbContext.CostingMaterials.Update(costingMaterial);
            await _dbContext.SaveChangesAsync(ct);
            return costingMaterial;
        }

        public async Task DeleteAsync(CostingMaterialDao costingMaterial, CancellationToken ct)
        {
            _dbContext.CostingMaterials.Remove(costingMaterial);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}