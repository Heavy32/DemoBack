namespace FlowCycle.Persistance.Repositories.Models
{
    public class CostingMaterialFilterDao
    {
        public int? CostingId { get; set; }
        public int? MaterialId { get; set; }
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}