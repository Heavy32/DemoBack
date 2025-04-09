using Microsoft.EntityFrameworkCore;

namespace FlowCycle.Persistance.Storage
{
    public class StockItemDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDao Category { get; set; }
        public string Code { get; set; }
        public string Measure { get; set; }
        public int Amount { get; set; }
        public double SinglePrice { get; set; }
        [Comment("НДС")]
        public double VAT { get; set; }
        public double TotalPrice { get; set; }
        [Comment("Дата поступления")]
        public DateTime ReceiptDate { get; set; }
        [Comment("Поставщик")]
        public SupplierDao Supplier { get; set; }
        public ProjectDao Project { get; set; }
        public bool IsArchived { get; set; }
    }
}
