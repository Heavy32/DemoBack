using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Domain.Costing
{
    public class CostingMaterial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [ForeignKey(nameof(CostingId))]
        public CostingModel Costing { get; set; } = null!;

        [Required]
        public int MaterialTypeId { get; set; }

        [ForeignKey(nameof(MaterialTypeId))]
        public CostingMaterialType MaterialType { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Uom { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal QtyPerProduct { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalValue { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}