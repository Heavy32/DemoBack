using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Persistance.Repositories.Models
{
    [Table("CostingMaterials")]
    public class CostingMaterialDao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [ForeignKey(nameof(CostingId))]
        public CostingDao Costing { get; set; } = null!;

        [Required]
        public int CostingMaterialTypeId { get; set; }

        [ForeignKey(nameof(CostingMaterialTypeId))]
        public CostingMaterialTypeDao CostingMaterialType { get; set; } = null!;

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

        [MaxLength(50)]
        public string? Code { get; set; }
    }
}