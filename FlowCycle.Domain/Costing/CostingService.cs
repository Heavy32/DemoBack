using AutoMapper;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Domain.Costing
{
    public class CostingService : ICostingService
    {
        private readonly ICostingRepository _costingRepository;
        private readonly IMapper _mapper;

        public CostingService(ICostingRepository costingRepository, IMapper mapper)
        {
            _costingRepository = costingRepository;
            _mapper = mapper;
        }

        public async Task<CostingModel> GetByIdAsync(int id, CancellationToken ct)
        {
            var dao = await _costingRepository.GetByIdAsync(id, ct);
            return _mapper.Map<CostingModel>(dao);
        }

        public async Task<IEnumerable<CostingModel>> GetListAsync(CostingFilter? filter = null, CancellationToken ct = default)
        {
            var filterDao = filter != null ? _mapper.Map<CostingFilterDao>(filter) : null;
            var daos = await _costingRepository.GetListAsync(filterDao, ct);
            return _mapper.Map<IEnumerable<CostingModel>>(daos);
        }

        public async Task<CostingModel> CreateAsync(CostingModel costing, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingDao>(costing);
            dao.CreatedAt = DateTime.UtcNow;
            dao.UpdatedAt = DateTime.UtcNow;
            var created = await _costingRepository.CreateAsync(dao, ct);
            return _mapper.Map<CostingModel>(created);
        }

        public async Task<CostingModel> UpdateAsync(CostingModel costing, CancellationToken ct)
        {
            var existing = await _costingRepository.GetByIdAsync(costing.Id, ct);
            var dao = _mapper.Map<CostingDao>(costing);
            dao.CreatedAt = existing.CreatedAt;
            dao.UpdatedAt = DateTime.UtcNow;
            var updated = await _costingRepository.UpdateAsync(dao, ct);
            return _mapper.Map<CostingModel>(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var costing = await _costingRepository.GetByIdAsync(id, ct);
            await _costingRepository.DeleteAsync(id, ct);
        }
    }
}