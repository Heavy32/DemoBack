using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Api.Models.Storage;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Persistance.Repositories.Models;
using FlowCycle.Persistance.Storage;

namespace DemoBack.Models.Storage.MapProfiles
{
    public class CostingProfiles : Profile
    {
        public CostingProfiles()
        {
            // CostingType
            CreateMap<CostingTypeDao, CostingType>().ReverseMap();
            CreateMap<CostingType, CostingTypeDto>().ReverseMap();
            CreateMap<CostingTypeDao, CostingTypeDto>().ReverseMap();


            // CostingMaterialType
            CreateMap<CostingMaterialTypeDao, CostingMaterialType>().ReverseMap();
            CreateMap<CostingMaterialType, CostingMaterialTypeDto>().ReverseMap();
            CreateMap<CostingMaterialTypeDao, CostingMaterialTypeDto>().ReverseMap();

            // OverheadType
            CreateMap<OverheadTypeDao, OverheadType>().ReverseMap();
            CreateMap<OverheadType, OverheadTypeDto>().ReverseMap();
            CreateMap<OverheadTypeDao, OverheadTypeDto>().ReverseMap();

            // CostingMaterial
            CreateMap<CostingMaterialDao, CostingMaterial>()
                .ForMember(dest => dest.CostingMaterialType, opt => opt.MapFrom(src => src.CostingMaterialType))
                .ReverseMap();
            CreateMap<CostingMaterial, CostingMaterialDto>()
                .ForMember(dest => dest.CostingMaterialType, opt => opt.MapFrom(src => src.CostingMaterialType))
                .ReverseMap();
            CreateMap<CostingMaterialDao, CostingMaterialDto>()
                .ForMember(dest => dest.CostingMaterialType, opt => opt.MapFrom(src => src.CostingMaterialType));

            // CostingLabor
            CreateMap<CostingLaborDao, CostingLabor>().ReverseMap();
            CreateMap<CostingLabor, CostingLaborDto>().ReverseMap();
            CreateMap<CostingLaborDao, CostingLaborDto>().ReverseMap();

            // CostingOverhead
            CreateMap<CostingOverheadDao, CostingOverhead>()
                .ForMember(dest => dest.OverheadType, opt => opt.MapFrom(src => src.OverheadType))
                .ReverseMap();
            CreateMap<CostingOverhead, CostingOverheadDto>()
                .ForMember(dest => dest.OverheadType, opt => opt.MapFrom(src => src.OverheadType))
                .ForMember(dest => dest.Uom, opt => opt.MapFrom(src => src.Uom))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.QtyPerProduct, opt => opt.MapFrom(src => src.QtyPerProduct))
                .ReverseMap();
            CreateMap<CostingOverheadDao, CostingOverheadDto>()
                .ForMember(dest => dest.OverheadType, opt => opt.MapFrom(src => src.OverheadType))
                .ForMember(dest => dest.Uom, opt => opt.MapFrom(src => src.Uom))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.QtyPerProduct, opt => opt.MapFrom(src => src.QtyPerProduct));

            // Costing
            CreateMap<CostingDao, CostingModel>()
                .ForMember(dest => dest.CostingType, opt => opt.MapFrom(src => src.CostingType))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.CostingLabors, opt => opt.MapFrom(src => src.CostingLabors))
                .ForMember(dest => dest.CostingMaterials, opt => opt.MapFrom(src => src.CostingMaterials))
                .ForMember(dest => dest.CostingOverheads, opt => opt.MapFrom(src => src.CostingOverheads))
                .ReverseMap();
            CreateMap<CostingModel, CostingDto>()
                .ForMember(dest => dest.CostingType, opt => opt.MapFrom(src => src.CostingType))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.CostingLabors, opt => opt.MapFrom(src => src.CostingLabors))
                .ForMember(dest => dest.CostingMaterials, opt => opt.MapFrom(src => src.CostingMaterials))
                .ForMember(dest => dest.CostingOverheads, opt => opt.MapFrom(src => src.CostingOverheads))
              .ReverseMap();
            CreateMap<CostingDao, CostingDto>()
                .ForMember(dest => dest.CostingType, opt => opt.MapFrom(src => src.CostingType))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.CostingLabors, opt => opt.MapFrom(src => src.CostingLabors))
                .ForMember(dest => dest.CostingMaterials, opt => opt.MapFrom(src => src.CostingMaterials))
                .ForMember(dest => dest.CostingOverheads, opt => opt.MapFrom(src => src.CostingOverheads));

            // Filter mappings
            CreateMap<CostingFilterDto, CostingFilter>().ReverseMap();
            CreateMap<CostingFilter, CostingFilterDao>().ReverseMap();
            CreateMap<CostingOverheadFilterDto, CostingOverheadFilter>().ReverseMap();
            CreateMap<CostingMaterialFilterDto, CostingMaterialFilter>().ReverseMap();
            CreateMap<CostingLaborFilterDto, CostingLaborFilter>().ReverseMap();
        }
    }
}