using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Domain.Storage
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct = default)
        {
            var daos = await _repository.GetAllAsync(ct);
            return daos.Select(_mapper.Map<Project>);
        }

        public async Task<Project?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return dao == null ? null : _mapper.Map<Project>(dao);
        }

        public async Task<Project> CreateAsync(Project project, CancellationToken ct = default)
        {
            var dao = _mapper.Map<ProjectDao>(project);
            var created = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<Project>(created);
        }

        public async Task<Project> UpdateAsync(Project project, CancellationToken ct = default)
        {
            var dao = _mapper.Map<ProjectDao>(project);
            var updated = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<Project>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }
    }
}