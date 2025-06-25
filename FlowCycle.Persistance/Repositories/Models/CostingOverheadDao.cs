using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Persistance.Repositories.Models
{
    [Table("CostingOverheads")]
    public class CostingOverheadDao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [Required]
        public int OverheadTypeId { get; set; }

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

        [MaxLength(255)]
        public string? Note { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(CostingId))]
        public CostingDao Costing { get; set; } = null!;

        [ForeignKey(nameof(OverheadTypeId))]
        public OverheadTypeDao OverheadType { get; set; } = null!;
    }
}