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
            // Domain to DTO mappings
            CreateMap<StockItem, StockItemDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<Supplier, SupplierDto>();

            // DTO to Domain mappings
            CreateMap<StockItemDto, StockItem>();
            CreateMap<CategoryDto, Category>();
            CreateMap<ProjectDto, Project>();
            CreateMap<SupplierDto, Supplier>();

            // Domain to DAO mappings
            CreateMap<StockItem, StockItemDao>();
            CreateMap<Category, CategoryDao>();
            CreateMap<Project, ProjectDao>();
            CreateMap<Supplier, SupplierDao>();

            // DAO to Domain mappings
            CreateMap<StockItemDao, StockItem>();
            CreateMap<CategoryDao, Category>();
            CreateMap<ProjectDao, Project>();
            CreateMap<SupplierDao, Supplier>();

            // Filter mappings
            CreateMap<StockItemFilterDto, StockItemFilter>();
            CreateMap<StockItemFilter, StockItemFilterDao>();
        }
    }
} 