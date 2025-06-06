using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingOverheadDao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CostingId { get; set; }

        [ForeignKey(nameof(CostingId))]
        public CostingDao Costing { get; set; } = null!;

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