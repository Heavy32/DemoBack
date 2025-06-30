using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingMaterialDto
    {
        public int Id { get; set; }

        public int CostingId { get; set; }

        public CostingMaterialTypeDto CostingMaterialType { get; set; }

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

        [MaxLength(50)]
        public string? Code { get; set; }
    }
}