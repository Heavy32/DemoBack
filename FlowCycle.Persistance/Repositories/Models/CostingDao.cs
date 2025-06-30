using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlowCycle.Persistance.Storage;

namespace FlowCycle.Persistance.Repositories.Models
{
    [Table("Costings")]
    public class CostingDao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        public int CostingTypeId { get; set; }

        [ForeignKey(nameof(CostingTypeId))]
        public CostingTypeDao CostingType { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Uom { get; set; } = string.Empty;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitCost { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectDao Project { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public ICollection<CostingLaborDao> CostingLabors { get; set; } = new List<CostingLaborDao>();
        public ICollection<CostingMaterialDao> CostingMaterials { get; set; } = new List<CostingMaterialDao>();
        public ICollection<CostingOverheadDao> CostingOverheads { get; set; } = new List<CostingOverheadDao>();
    }
}