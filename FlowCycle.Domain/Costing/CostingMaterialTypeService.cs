using AutoMapper;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Domain.Costing
{
    public class CostingMaterialTypeService : ICostingMaterialTypeService
    {
        private readonly ICostingMaterialTypeRepository _repository;
        private readonly IMapper _mapper;

        public CostingMaterialTypeService(ICostingMaterialTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CostingMaterialType?> GetByIdAsync(int id, CancellationToken ct)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return dao != null ? _mapper.Map<CostingMaterialType>(dao) : null;
        }

        public async Task<CostingMaterialType?> GetByNameAsync(string name, CancellationToken ct)
        {
            var dao = await _repository.GetByNameAsync(name, ct);
            return dao != null ? _mapper.Map<CostingMaterialType>(dao) : null;
        }

        public async Task<IEnumerable<CostingMaterialType>> GetAllAsync(CancellationToken ct)
        {
            var daos = await _repository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<CostingMaterialType>>(daos);
        }

        public async Task<CostingMaterialType> CreateAsync(CostingMaterialType materialType, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingMaterialTypeDao>(materialType);
            var createdDao = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<CostingMaterialType>(createdDao);
        }

        public async Task<CostingMaterialType> UpdateAsync(CostingMaterialType materialType, CancellationToken ct)
        {
            var dao = _mapper.Map<CostingMaterialTypeDao>(materialType);
            var updatedDao = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<CostingMaterialType>(updatedDao);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            await _repository.DeleteAsync(id, ct);
        }
    }
}