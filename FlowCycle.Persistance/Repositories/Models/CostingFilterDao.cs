namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingFilterDao
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int? CostingTypeId { get; set; }
        public int? ProjectId { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}