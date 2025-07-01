namespace FlowCycle.Domain.Storage.Models
{
    public class StorageItemFilter
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? Code { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
} 