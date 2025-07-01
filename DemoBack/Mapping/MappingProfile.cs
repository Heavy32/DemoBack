using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Domain.Storage;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace DemoBack.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project
            CreateMap<ProjectDao, Project>().ReverseMap();
            CreateMap<Project, ProjectDao>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<ProjectDao, ProjectDto>().ReverseMap();

            // Domain to DTO mappings
            CreateMap<StorageItem, StorageItemDto>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.ArchivedCount, opt => opt.MapFrom(src => src.ArchivedCount))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<Supplier, SupplierDto>();

            // DTO to Domain mappings
            CreateMap<StorageItemDto, StorageItem>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.ArchivedCount, opt => opt.MapFrom(src => src.ArchivedCount))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ReverseMap();
            CreateMap<CategoryDto, Category>();
            CreateMap<SupplierDto, Supplier>();

            // Domain to DAO mappings
            CreateMap<StorageItem, StorageItemDao>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.ArchivedCount, opt => opt.MapFrom(src => src.ArchivedCount))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ReverseMap();
            CreateMap<Category, CategoryDao>();
            CreateMap<Supplier, SupplierDao>();

            // DAO to Domain mappings
            CreateMap<StorageItemDao, StorageItem>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.ArchivedCount, opt => opt.MapFrom(src => src.ArchivedCount))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ReverseMap();
            CreateMap<CategoryDao, Category>();
            CreateMap<SupplierDao, Supplier>();

            // Filter mappings
            CreateMap<StorageItemFilterDto, StorageItemFilter>();
            CreateMap<StorageItemFilter, StorageItemFilterDao>();
        }
    }
}