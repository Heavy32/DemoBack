namespace FlowCycle.Api.Storage
{
    public class StorageItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Measure { get; set; }
        public int Amount { get; set; }
        public double SinglePrice { get; set; }
        public double VAT { get; set; }
        public double TotalPrice { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierId { get; set; }
        public int ProjectId { get; set; }
        public bool IsArchived { get; set; }
        public int ArchivedCount { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
