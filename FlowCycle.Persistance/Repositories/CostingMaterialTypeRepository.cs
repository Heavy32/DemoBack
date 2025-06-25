using FlowCycle.Persistance.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Repositories
{
    public class CostingMaterialTypeRepository : ICostingMaterialTypeRepository
    {
        private readonly AppDbContext _context;

        public CostingMaterialTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CostingMaterialTypeDao?> GetByNameAsync(string name, CancellationToken ct)
        {
            return await _context.CostingMaterialTypes
                .FirstOrDefaultAsync(x => x.Name == name, ct);
        }

        public async Task<CostingMaterialTypeDao?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.CostingMaterialTypes
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IEnumerable<CostingMaterialTypeDao>> GetAllAsync(CancellationToken ct)
        {
            return await _context.CostingMaterialTypes
                .ToListAsync(ct);
        }

        public async Task<CostingMaterialTypeDao> CreateAsync(CostingMaterialTypeDao materialType, CancellationToken ct)
        {
            _context.CostingMaterialTypes.Add(materialType);
            return materialType;
        }

        public async Task<CostingMaterialTypeDao> UpdateAsync(CostingMaterialTypeDao materialType, CancellationToken ct)
        {
            _context.CostingMaterialTypes.Update(materialType);
            return materialType;
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var materialType = await GetByIdAsync(id, ct);
            if (materialType != null)
            {
                _context.CostingMaterialTypes.Remove(materialType);
            }
        }
    }
}