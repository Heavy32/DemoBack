using AutoMapper;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Domain.Costing
{
    public class CostingMaterialService : ICostingMaterialService
    {
        private readonly ICostingMaterialRepository _costingMaterialRepository;
        private readonly IMapper _mapper;

        public CostingMaterialService(ICostingMaterialRepository costingMaterialRepository, IMapper mapper)
        {
            _costingMaterialRepository = costingMaterialRepository;
            _mapper = mapper;
        }

        public async Task<CostingMaterial> GetByIdAsync(int id, CancellationToken ct)
        {
            var dao = await _costingMaterialRepository.GetByIdAsync(id, ct);
            return _mapper.Map<CostingMaterial>(dao);
        }

        public async Task<IEnumerable<CostingMaterial>> GetListAsync(CostingMaterialFilter? filter = null, CancellationToken ct = default)
        {
            var filterDao = filter != null ? _mapper.Map<CostingMaterialFilterDao>(filter) : null;
            var daos = await _costingMaterialRepository.GetListAsync(filterDao, ct);
            return _mapper.Map<IEnumerable<CostingMaterial>>(daos);
        }

        public async Task<CostingMaterial> CreateAsync(CostingMaterial costingMaterial, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingMaterialDao>(costingMaterial);
            var createdDao = await _costingMaterialRepository.CreateAsync(dao, ct);
            return _mapper.Map<CostingMaterial>(createdDao);
        }

        public async Task<CostingMaterial> UpdateAsync(CostingMaterial costingMaterial, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingMaterialDao>(costingMaterial);
            var updatedDao = await _costingMaterialRepository.UpdateAsync(dao, ct);
            return _mapper.Map<CostingMaterial>(updatedDao);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costingMaterial = await _costingMaterialRepository.GetByIdAsync(id, ct);
            await _costingMaterialRepository.DeleteAsync(costingMaterial, ct);
        }
    }
}