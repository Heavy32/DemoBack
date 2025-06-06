using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingMaterialDao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [ForeignKey(nameof(CostingId))]
        public CostingDao Costing { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string MaterialName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string MaterialCode { get; set; } = null!;

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