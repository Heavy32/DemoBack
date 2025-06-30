using DemoBack.Models.Storage;
using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Api.Models.Storage
{
    public class CostingOverheadDto
    {
        public int Id { get; set; }

        public int CostingId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        public OverheadTypeDto OverheadType { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Uom { get; set; } = null!;

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal QtyPerProduct { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}