using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Domain.Costing
{
    public class CostingOverhead
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [ForeignKey(nameof(CostingId))]
        public CostingModel Costing { get; set; } = null!;

        [Required]
        public int OverheadTypeId { get; set; }

        [ForeignKey(nameof(OverheadTypeId))]
        public OverheadType OverheadType { get; set; } = null!;

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
    }
}