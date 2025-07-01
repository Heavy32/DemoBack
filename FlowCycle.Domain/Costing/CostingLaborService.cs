using AutoMapper;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.UnitOfWork;

namespace FlowCycle.Domain.Costing
{
    public class CostingLaborService : ICostingLaborService
    {
        private readonly ICostingLaborRepository _costingLaborRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CostingLaborService(ICostingLaborRepository costingLaborRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _costingLaborRepository = costingLaborRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.SaveChangesAsync(ct);
            return _mapper.Map<CostingLabor>(createdDao);
        }

        public async Task<CostingLabor> UpdateAsync(CostingLabor costingLabor, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingLaborDao>(costingLabor);
            var updatedDao = await _costingLaborRepository.UpdateAsync(dao, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return _mapper.Map<CostingLabor>(updatedDao);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var entity = await _costingLaborRepository.GetByIdAsync(id, ct);
            if (entity != null)
            {
                await _costingLaborRepository.DeleteAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
            }
        }
    }
}