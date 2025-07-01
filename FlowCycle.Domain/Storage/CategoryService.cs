using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Domain.Storage
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
        {
            var daos = await _repository.GetAllAsync(ct);
            return daos.Select(_mapper.Map<Category>);
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return dao == null ? null : _mapper.Map<Category>(dao);
        }

        public async Task<Category> CreateAsync(Category category, CancellationToken ct = default)
        {
            var dao = _mapper.Map<CategoryDao>(category);
            var created = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<Category>(created);
        }

        public async Task<Category> UpdateAsync(Category category, CancellationToken ct = default)
        {
            var dao = _mapper.Map<CategoryDao>(category);
            var updated = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<Category>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }
    }
}