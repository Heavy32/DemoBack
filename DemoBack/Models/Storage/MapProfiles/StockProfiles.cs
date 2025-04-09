using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage;

namespace FlowCycle.Api.Models.Storage.MapProfiles
{
    public class StockProfiles : Profile
    {
        public StockProfiles()
        {
            CreateMap<StockItem, StockItemDto>().ReverseMap();
        }
    }
}
