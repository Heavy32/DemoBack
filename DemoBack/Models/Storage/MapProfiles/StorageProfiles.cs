using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Storage.Models;

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

            CreateMap<StorageItemFilterDto, StorageItemFilter>()
                .ForMember(dest => dest.ArrivalDateFrom, opt => opt.MapFrom(src => src.ArrivalDateFrom))
                .ForMember(dest => dest.ArrivalDateTo, opt => opt.MapFrom(src => src.ArrivalDateTo))
                .ReverseMap();

            CreateMap<StorageItemFilter, FlowCycle.Persistance.Repositories.Models.StorageItemFilterDao>()
                .ForMember(dest => dest.ArrivalDateFrom, opt => opt.MapFrom(src => src.ArrivalDateFrom))
                .ForMember(dest => dest.ArrivalDateTo, opt => opt.MapFrom(src => src.ArrivalDateTo))
                .ReverseMap();

            CreateMap<StorageItemDto, StorageItem>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Supplier, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category != null ? src.Category.Id : 0))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Id : 0))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Project != null ? src.Project.Id : 0));
        }
    }
}
