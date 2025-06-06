namespace FlowCycle.Domain.Costing.Models
{
    public class CostingMaterialFilter
    {
        public int? CostingId { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}