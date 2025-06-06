using AutoMapper;
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
            CreateMap<CostingOverhead, CostingOverheadDto>();
            CreateMap<CostingOverheadFilter, CostingOverheadFilterDto>();

            // DTO to Domain mappings
            CreateMap<CostingOverheadDto, CostingOverhead>();
            CreateMap<CostingOverheadFilterDto, CostingOverheadFilter>();

            // Domain to DAO mappings
            CreateMap<CostingOverhead, CostingOverheadDao>();
            CreateMap<CostingOverheadFilter, CostingOverheadFilterDao>();

            // DAO to Domain mappings
            CreateMap<CostingOverheadDao, CostingOverhead>();
            CreateMap<CostingOverheadFilterDao, CostingOverheadFilter>();
        }
    }
}