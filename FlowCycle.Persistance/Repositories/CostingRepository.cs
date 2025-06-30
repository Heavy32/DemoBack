using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingRepository : ICostingRepository
    {
        private readonly AppDbContext _dbContext;

        public CostingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CostingDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _dbContext.Costings
                .Include(x => x.Project)
                .Include(x => x.CostingType)
                .Include(x => x.CostingLabors)
                .Include(x => x.CostingMaterials)
                    .ThenInclude(m => m.CostingMaterialType)
                .Include(x => x.CostingOverheads)
                .FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new KeyNotFoundException($"Costing with ID {id} not found");
        }

        public async Task<IEnumerable<CostingDao>> GetListAsync(CostingFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _dbContext.Costings
                .Include(x => x.Project)
                .Include(x => x.CostingType)
                .Include(x => x.CostingLabors)
                .Include(x => x.CostingMaterials)
                    .ThenInclude(m => m.CostingMaterialType)
                .Include(x => x.CostingOverheads)
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.ProductName))
                    query = query.Where(x => x.ProductName.Contains(filter.ProductName));

                if (!string.IsNullOrWhiteSpace(filter.ProductCode))
                    query = query.Where(x => x.ProductCode.Contains(filter.ProductCode));

                if (filter.CostingTypeId.HasValue)
                    query = query.Where(x => x.CostingTypeId == filter.CostingTypeId.Value);

                if (filter.ProjectId.HasValue)
                    query = query.Where(x => x.ProjectId == filter.ProjectId.Value);

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "productname" => filter.SortDescending
                            ? query.OrderByDescending(x => x.ProductName)
                            : query.OrderBy(x => x.ProductName),
                        "productcode" => filter.SortDescending
                            ? query.OrderByDescending(x => x.ProductCode)
                            : query.OrderBy(x => x.ProductCode),
                        "costingtypeid" => filter.SortDescending
                            ? query.OrderByDescending(x => x.CostingTypeId)
                            : query.OrderBy(x => x.CostingTypeId),
                        "uom" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Uom)
                            : query.OrderBy(x => x.Uom),
                        "quantity" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Quantity)
                            : query.OrderBy(x => x.Quantity),
                        "unitcost" => filter.SortDescending
                            ? query.OrderByDescending(x => x.UnitCost)
                            : query.OrderBy(x => x.UnitCost),
                        "totalcost" => filter.SortDescending
                            ? query.OrderByDescending(x => x.TotalCost)
                            : query.OrderBy(x => x.TotalCost),
                        "projectid" => filter.SortDescending
                            ? query.OrderByDescending(x => x.ProjectId)
                            : query.OrderBy(x => x.ProjectId),
                        "createdat" => filter.SortDescending
                            ? query.OrderByDescending(x => x.CreatedAt)
                            : query.OrderBy(x => x.CreatedAt),
                        "updatedat" => filter.SortDescending
                            ? query.OrderByDescending(x => x.UpdatedAt)
                            : query.OrderBy(x => x.UpdatedAt),
                        _ => query
                    };
                }
            }

            return await query.ToListAsync(ct);
        }

        public async Task<CostingDao> CreateAsync(CostingDao costing, CancellationToken ct)
        {
            _dbContext.Costings.Add(costing);
            return costing;
        }

        public async Task<CostingDao> UpdateAsync(CostingDao costing, CancellationToken ct)
        {
            _dbContext.Costings.Update(costing);
            return costing;
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costing = await GetByIdAsync(id, ct);
            _dbContext.Costings.Remove(costing);
        }

        public async Task<ProjectDao> GetProjectByNameAsync(string name, CancellationToken ct)
        {
            var project = await _dbContext.Projects
                .FirstOrDefaultAsync(p => p.Name == name, ct);

            if (project == null)
            {
                throw new KeyNotFoundException($"Project with name '{name}' not found");
            }

            return project;
        }

        public async Task<CostingTypeDao> GetCostingTypeByNameAsync(string name, CancellationToken ct)
        {
            var costingType = await _dbContext.CostingTypes
                .FirstOrDefaultAsync(ct => ct.Name == name, ct);

            if (costingType == null)
            {
                throw new KeyNotFoundException($"Costing type with name '{name}' not found");
            }

            return costingType;
        }

        public async Task<CostingDao?> GetByNameAsync(string productName, CancellationToken ct)
        {
            return await _dbContext.Costings
                .Include(x => x.Project)
                .Include(x => x.CostingType)
                .Include(x => x.CostingLabors)
                .Include(x => x.CostingMaterials)
                    .ThenInclude(m => m.CostingMaterialType)
                .Include(x => x.CostingOverheads)
                .FirstOrDefaultAsync(x => x.ProductName == productName, ct);
        }
    }
}