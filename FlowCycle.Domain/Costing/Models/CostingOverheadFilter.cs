namespace FlowCycle.Domain.Costing.Models
{
    public class CostingOverheadFilter
    {
        public int? CostingId { get; set; }
        public int? OverheadTypeId { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}