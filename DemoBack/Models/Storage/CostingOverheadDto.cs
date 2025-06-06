using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Api.Models.Storage
{
    public class CostingOverheadDto
    {
        public int Id { get; set; }

        public int CostingId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OverheadName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string OverheadType { get; set; } = null!;

        [Required]
        public decimal CostValue { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}