using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingMaterialFilterDto
    {
        public int? CostingId { get; set; }
        public int? MaterialTypeId { get; set; }

        [RegularExpression(@"^(materialTypeId|uom|unitPrice|qtyPerProduct|totalValue|note)$",
            ErrorMessage = "Invalid sort column. Valid values are: materialTypeId, uom, unitPrice, qtyPerProduct, totalValue, note")]
        public string? SortColumn { get; set; }
        public bool SortDescending { get; set; }
    }
}