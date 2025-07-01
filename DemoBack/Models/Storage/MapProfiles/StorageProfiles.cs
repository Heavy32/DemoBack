using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage;

namespace FlowCycle.Api.Models.Storage.MapProfiles
{
    public class StorageProfiles : Profile
    {
        public StorageProfiles()
        {
            CreateMap<StorageItem, StorageItemDto>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.ArchivedCount, opt => opt.MapFrom(src => src.ArchivedCount))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ReverseMap();
        }
    }
}
