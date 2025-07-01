using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Domain.Storage
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken ct = default)
        {
            var daos = await _repository.GetAllAsync(ct);
            return daos.Select(_mapper.Map<Supplier>);
        }

        public async Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return dao == null ? null : _mapper.Map<Supplier>(dao);
        }

        public async Task<Supplier> CreateAsync(Supplier supplier, CancellationToken ct = default)
        {
            var dao = _mapper.Map<SupplierDao>(supplier);
            var created = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<Supplier>(created);
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier, CancellationToken ct = default)
        {
            var dao = _mapper.Map<SupplierDao>(supplier);
            var updated = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<Supplier>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }
    }
}