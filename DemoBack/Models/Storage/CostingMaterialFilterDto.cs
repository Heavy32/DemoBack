using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingMaterialFilterDto
    {
        public int? CostingId { get; set; }

        public string? MaterialName { get; set; }

        public string? MaterialCode { get; set; }

        [RegularExpression(@"^(materialName|materialCode|uom|unitPrice|qtyPerProduct|totalValue|note)$",
            ErrorMessage = "Invalid sort column. Valid values are: materialName, materialCode, uom, unitPrice, qtyPerProduct, totalValue, note")]
        public string? SortColumn { get; set; }

        public bool SortDescending { get; set; }
    }
}