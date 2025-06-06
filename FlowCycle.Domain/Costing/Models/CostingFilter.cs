namespace FlowCycle.Domain.Storage.Models
{
    public class CostingFilter
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? CostingType { get; set; }
        public string? ProjectName { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}