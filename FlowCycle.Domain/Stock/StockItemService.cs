using AutoMapper;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Domain.Stock
{
    public class StorageItemService : IStorageItemService
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public StorageItemService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<StockItem> GetById(int id, CancellationToken ct)
        {
            var dbResult = await context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            var result = mapper.Map<StockItem>(dbResult);

            return result;
        }

        public async Task<IEnumerable<StockItem>> GetList(CancellationToken ct)
        {
            var dbResult = await context.Stocks.ToListAsync(ct);

            var result = dbResult.Select(mapper.Map<StockItem>);

            return result;
        }
    }
}
