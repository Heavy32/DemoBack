using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Domain.Costing
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = null!;
    }
}