using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Api.Models.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using FlowCycle.Persistance.Repositories.Models;

namespace FlowCycle.Api.Models.Storage.MapProfiles
{
    public class CostingMaterialProfiles : Profile
    {
        public CostingMaterialProfiles()
        {
            // Removed generic CostingMaterial mappings to avoid duplication. All CostingMaterial mappings are now in CostingProfiles.cs.
            // Domain to DTO mappings
            CreateMap<CostingMaterial, CostingMaterialDto>();
            CreateMap<CostingMaterialFilter, CostingMaterialFilterDto>();

            // DTO to Domain mappings
            CreateMap<CostingMaterialDto, CostingMaterial>();
            CreateMap<CostingMaterialFilterDto, CostingMaterialFilter>();

            // Domain to DAO mappings
            CreateMap<CostingMaterial, CostingMaterialDao>();
            CreateMap<CostingMaterialFilter, CostingMaterialFilterDao>();

            // DAO to Domain mappings
            CreateMap<CostingMaterialDao, CostingMaterial>();
            CreateMap<CostingMaterialFilterDao, CostingMaterialFilter>();
        }
    }
}