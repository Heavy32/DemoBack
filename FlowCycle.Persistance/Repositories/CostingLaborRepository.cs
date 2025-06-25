using FlowCycle.Persistance.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingLaborRepository : ICostingLaborRepository
    {
        private readonly AppDbContext _dbContext;

        public CostingLaborRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CostingLaborDao> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _dbContext.CostingLabors
                .FirstOrDefaultAsync(x => x.Id == id, ct)
                ?? throw new KeyNotFoundException($"CostingLabor with ID {id} not found");
        }

        public async Task<IEnumerable<CostingLaborDao>> GetListAsync(CostingLaborFilterDao? filter = null, CancellationToken ct = default)
        {
            var query = _dbContext.CostingLabors.AsQueryable();

            if (filter != null)
            {
                if (filter.CostingId.HasValue)
                    query = query.Where(x => x.CostingId == filter.CostingId.Value);

                if (!string.IsNullOrWhiteSpace(filter.LaborName))
                    query = query.Where(x => x.LaborName.Contains(filter.LaborName));

                if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    query = filter.SortColumn.ToLower() switch
                    {
                        "laborname" => filter.SortDescending
                            ? query.OrderByDescending(x => x.LaborName)
                            : query.OrderBy(x => x.LaborName),
                        "hours" => filter.SortDescending
                            ? query.OrderByDescending(x => x.Hours)
                            : query.OrderBy(x => x.Hours),
                        "hourrate" => filter.SortDescending
                            ? query.OrderByDescending(x => x.HourRate)
                            : query.OrderBy(x => x.HourRate),
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

        public async Task<CostingLaborDao> CreateAsync(CostingLaborDao costingLabor, CancellationToken ct)
        {
            _dbContext.CostingLabors.Add(costingLabor);
            return costingLabor;
        }

        public async Task<CostingLaborDao> UpdateAsync(CostingLaborDao costingLabor, CancellationToken ct)
        {
            _dbContext.CostingLabors.Update(costingLabor);
            return costingLabor;
        }

        public async Task DeleteAsync(CostingLaborDao costingLabor, CancellationToken ct)
        {
            _dbContext.CostingLabors.Remove(costingLabor);
        }
    }
}