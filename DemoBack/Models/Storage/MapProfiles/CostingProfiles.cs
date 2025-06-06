using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Storage.Models;
using FlowCycle.Persistance.Repositories.Models;

namespace DemoBack.Models.Storage.MapProfiles
{
    public class CostingProfiles : Profile
    {
        public CostingProfiles()
        {
            // Domain to DTO mappings
            CreateMap<CostingModel, CostingDto>();

            // DTO to Domain mappings
            CreateMap<CostingDto, CostingModel>();

            // Domain to DAO mappings
            CreateMap<CostingModel, CostingDao>();

            // DAO to Domain mappings
            CreateMap<CostingDao, CostingModel>();

            // Filter mappings
            CreateMap<CostingFilterDto, CostingFilter>();
            CreateMap<CostingFilter, CostingFilterDao>();
        }
    }
}