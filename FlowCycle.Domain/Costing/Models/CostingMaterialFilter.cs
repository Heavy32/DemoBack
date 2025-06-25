namespace FlowCycle.Domain.Costing.Models
{
    public class CostingMaterialFilter
    {
        public int? CostingId { get; set; }
        public int? MaterialTypeId { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}