using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Api.Models.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Api.Models.Storage.MapProfiles
{
    public class CostingOverheadProfiles : Profile
    {
        public CostingOverheadProfiles()
        {
            // Domain to DTO mappings
            CreateMap<CostingOverhead, CostingOverheadDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CostingOverheadFilter, CostingOverheadFilterDto>();

            // DTO to Domain mappings
            CreateMap<CostingOverheadDto, CostingOverhead>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CostingOverheadFilterDto, CostingOverheadFilter>();

            // Domain to DAO mappings
            CreateMap<CostingOverhead, CostingOverheadDao>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CostingOverheadFilter, CostingOverheadFilterDao>();

            // DAO to Domain mappings
            CreateMap<CostingOverheadDao, CostingOverhead>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CostingOverheadFilterDao, CostingOverheadFilter>();
        }
    }
}