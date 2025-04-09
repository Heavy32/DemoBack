

namespace FlowCycle.Api.Storage
{
    public class StockItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDto Category { get; set; }
        public string Code { get; set; }
        public string Measure { get; set; }
        public int Amount { get; set; }
        public double SinglePrice { get; set; }
        public double VAT { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ReceiptDate { get; set; }
        public SupplierDto Supplier { get; set; }
        public ProjectDto Project { get; set; }
    }
}
