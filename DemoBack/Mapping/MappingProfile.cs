using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Stock.Models;
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
            CreateMap<StockItem, StockItemDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Supplier, SupplierDto>();

            // DTO to Domain mappings
            CreateMap<StockItemDto, StockItem>();
            CreateMap<CategoryDto, Category>();
            CreateMap<SupplierDto, Supplier>();

            // Domain to DAO mappings
            CreateMap<StockItem, StockItemDao>();
            CreateMap<Category, CategoryDao>();
            CreateMap<Supplier, SupplierDao>();

            // DAO to Domain mappings
            CreateMap<StockItemDao, StockItem>();
            CreateMap<CategoryDao, Category>();
            CreateMap<SupplierDao, Supplier>();

            // Filter mappings
            CreateMap<StockItemFilterDto, StockItemFilter>();
            CreateMap<StockItemFilter, StockItemFilterDao>();
        }
    }
}