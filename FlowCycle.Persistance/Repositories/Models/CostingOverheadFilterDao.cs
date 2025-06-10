namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingOverheadFilterDao
    {
        public int? CostingId { get; set; }
        public int? OverheadTypeId { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}