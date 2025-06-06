namespace FlowCycle.Domain.Costing.Models
{
    public class CostingLaborFilter
    {
        public int? CostingId { get; set; }
        public string? LaborName { get; set; }
        public string? LaborType { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}