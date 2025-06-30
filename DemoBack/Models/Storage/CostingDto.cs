using FlowCycle.Api.Models.Storage;
using FlowCycle.Api.Storage;

namespace DemoBack.Models.Storage
{
    public class CostingDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public CostingTypeDto CostingType { get; set; }
        public string Uom { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public ProjectDto Project { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<CostingLaborDto> CostingLabors { get; set; } = new List<CostingLaborDto>();
        public ICollection<CostingMaterialDto> CostingMaterials { get; set; } = new List<CostingMaterialDto>();
        public ICollection<CostingOverheadDto> CostingOverheads { get; set; } = new List<CostingOverheadDto>();
    }
}