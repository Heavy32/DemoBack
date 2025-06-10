using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlowCycle.Domain.Storage;

namespace FlowCycle.Domain.Costing
{
    public class CostingModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }

        [Required]
        public int CostingTypeId { get; set; }

        [ForeignKey(nameof(CostingTypeId))]
        public CostingType CostingType { get; set; } = null!;

        public string Uom { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}