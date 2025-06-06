using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Api.Models.Storage.MapProfiles
{
    public class CostingLaborProfiles : Profile
    {
        public CostingLaborProfiles()
        {
            // Domain to DTO mappings
            CreateMap<CostingLabor, CostingLaborDto>();
            CreateMap<CostingLaborFilter, CostingLaborFilterDto>();

            // DTO to Domain mappings
            CreateMap<CostingLaborDto, CostingLabor>();
            CreateMap<CostingLaborFilterDto, CostingLaborFilter>();

            // Domain to DAO mappings
            CreateMap<CostingLabor, CostingLaborDao>();
            CreateMap<CostingLaborFilter, CostingLaborFilterDao>();

            // DAO to Domain mappings
            CreateMap<CostingLaborDao, CostingLabor>();
            CreateMap<CostingLaborFilterDao, CostingLaborFilter>();
        }
    }
}