using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Domain.Costing
{
    public class CostingType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}