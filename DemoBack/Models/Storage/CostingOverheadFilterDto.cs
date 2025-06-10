using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingOverheadFilterDto
    {
        public int? CostingId { get; set; }
        public int? OverheadTypeId { get; set; }

        [RegularExpression(@"^(overheadTypeId|uom|unitPrice|qtyPerProduct|totalValue|note)$",
            ErrorMessage = "Invalid sort column. Valid values are: overheadTypeId, uom, unitPrice, qtyPerProduct, totalValue, note")]
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}