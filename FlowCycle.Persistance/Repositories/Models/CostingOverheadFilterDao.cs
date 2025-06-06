namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingOverheadFilterDao
    {
        public int? CostingId { get; set; }
        public string? OverheadName { get; set; }
        public string? OverheadType { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}