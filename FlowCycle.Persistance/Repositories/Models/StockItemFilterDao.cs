namespace FlowCycle.Persistance.Repositories.Models
{
    public class StockItemFilterDao
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Code { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
} 