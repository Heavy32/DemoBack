using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingLaborDto
    {
        public int Id { get; set; }

        public int CostingId { get; set; }

        [Required]
        [MaxLength(255)]
        public string LaborName { get; set; } = null!;

        [Required]
        public decimal Hours { get; set; }

        [Required]
        public decimal HourRate { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}