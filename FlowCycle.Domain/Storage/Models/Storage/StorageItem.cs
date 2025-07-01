

namespace FlowCycle.Domain.Storage
{
    public class StorageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Code { get; set; }
        public string Measure { get; set; }
        public int Amount { get; set; }
        public double SinglePrice { get; set; }
        public double VAT { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Supplier Supplier { get; set; }
        public Project Project { get; set; }
        public bool IsArchived { get; set; }
        /// <summary>
        /// Только для архивных записей
        /// </summary>
        public int ArchivedCount { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
