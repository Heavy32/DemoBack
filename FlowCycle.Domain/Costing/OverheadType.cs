using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Domain.Costing
{
    public class OverheadType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}