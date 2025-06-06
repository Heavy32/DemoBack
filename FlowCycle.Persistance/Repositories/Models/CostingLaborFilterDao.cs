namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingLaborFilterDao
    {
        public int? CostingId { get; set; }
        public string? LaborName { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}