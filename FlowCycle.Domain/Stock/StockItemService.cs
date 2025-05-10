using AutoMapper;
using FlowCycle.Domain.Stock.Models;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Domain.Stock
{
    public class StorageItemService : IStorageItemService
    {
        private readonly IStockItemRepository _repository;
        private readonly IMapper _mapper;

        public StorageItemService(IStockItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StockItem> GetById(int id, CancellationToken ct)
        {
            var dao = await _repository.GetByIdAsync(id, ct);
            return _mapper.Map<StockItem>(dao);
        }

        public async Task<IEnumerable<StockItem>> GetList(StockItemFilter? filter = null, CancellationToken ct = default)
        {
            var repositoryFilter = filter != null ? _mapper.Map<StockItemFilterDao>(filter) : null;
            var daos = await _repository.GetListAsync(repositoryFilter, ct);
            return daos.Select(_mapper.Map<StockItem>);
        }

        public async Task<StockItem> Create(StockItem stockItem, CancellationToken ct)
        {
            var dao = _mapper.Map<StockItemDao>(stockItem);
            var createdDao = await _repository.CreateAsync(dao, ct);
            return _mapper.Map<StockItem>(createdDao);
        }

        public async Task<StockItem> Update(StockItem stockItem, int id, CancellationToken ct)
        {
            var dao = _mapper.Map<StockItemDao>(stockItem);
            var updatedDao = await _repository.UpdateAsync(dao, ct);
            return _mapper.Map<StockItem>(updatedDao);
        }

        public async Task Delete(StockItem stockItem, CancellationToken ct)
        {
            var dao = _mapper.Map<StockItemDao>(stockItem);
            await _repository.DeleteAsync(dao, ct);
        }
    }
}
