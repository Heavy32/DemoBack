using AutoMapper;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Domain.Costing
{
    public class CostingLaborService : ICostingLaborService
    {
        private readonly ICostingLaborRepository _costingLaborRepository;
        private readonly IMapper _mapper;

        public CostingLaborService(ICostingLaborRepository costingLaborRepository, IMapper mapper)
        {
            _costingLaborRepository = costingLaborRepository;
            _mapper = mapper;
        }

        public async Task<CostingLabor> GetByIdAsync(int id, CancellationToken ct)
        {
            var dao = await _costingLaborRepository.GetByIdAsync(id, ct);
            return _mapper.Map<CostingLabor>(dao);
        }

        public async Task<IEnumerable<CostingLabor>> GetListAsync(CostingLaborFilter? filter = null, CancellationToken ct = default)
        {
            var filterDao = filter != null ? _mapper.Map<CostingLaborFilterDao>(filter) : null;
            var daos = await _costingLaborRepository.GetListAsync(filterDao, ct);
            return _mapper.Map<IEnumerable<CostingLabor>>(daos);
        }

        public async Task<CostingLabor> CreateAsync(CostingLabor costingLabor, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingLaborDao>(costingLabor);
            var createdDao = await _costingLaborRepository.CreateAsync(dao, ct);
            return _mapper.Map<CostingLabor>(createdDao);
        }

        public async Task<CostingLabor> UpdateAsync(CostingLabor costingLabor, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingLaborDao>(costingLabor);
            var updatedDao = await _costingLaborRepository.UpdateAsync(dao, ct);
            return _mapper.Map<CostingLabor>(updatedDao);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costingLabor = await _costingLaborRepository.GetByIdAsync(id, ct);
            await _costingLaborRepository.DeleteAsync(costingLabor, ct);
        }
    }
}