using AutoMapper;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.UnitOfWork;

namespace FlowCycle.Domain.Costing
{
    public class CostingOverheadService : ICostingOverheadService
    {
        private readonly ICostingOverheadRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CostingOverheadService(ICostingOverheadRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CostingOverhead> GetByIdAsync(int id, CancellationToken ct)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return _mapper.Map<CostingOverhead>(dao);
        }

        public async Task<IEnumerable<CostingOverhead>> GetListAsync(CostingOverheadFilter? filter = null, CancellationToken ct = default)
        {
            var filterDao = filter != null ? _mapper.Map<CostingOverheadFilterDao>(filter) : null;
            var daos = await _repository.GetListAsync(filterDao, ct);
            return _mapper.Map<IEnumerable<CostingOverhead>>(daos);
        }

        public async Task<CostingOverhead> CreateAsync(CostingOverhead overhead, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingOverheadDao>(overhead);
            var createdDao = await _repository.CreateAsync(dao, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return _mapper.Map<CostingOverhead>(createdDao);
        }

        public async Task<CostingOverhead> UpdateAsync(CostingOverhead overhead, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingOverheadDao>(overhead);
            var updatedDao = await _repository.UpdateAsync(dao, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return _mapper.Map<CostingOverhead>(updatedDao);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repository.DeleteAsync(id, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}