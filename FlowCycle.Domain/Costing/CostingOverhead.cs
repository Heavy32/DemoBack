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
        [MaxLength(255)]
        public string OverheadName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string OverheadType { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostValue { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}