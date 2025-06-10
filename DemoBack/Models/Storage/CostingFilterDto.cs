using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    /// <summary>
    /// Filter and sort parameters for costing items
    /// </summary>
    public class CostingFilterDto
    {
        /// <summary>
        /// Filter by product name (case-insensitive contains)
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Filter by product code (case-insensitive contains)
        /// </summary>
        public string? ProductCode { get; set; }

        /// <summary>
        /// Filter by costing type ID
        /// </summary>
        public int? CostingTypeId { get; set; }

        /// <summary>
        /// Filter by project ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// Column to sort by. Available values: productName, productCode, costingTypeId, uom, quantity, unitCost, totalCost, projectId, createdAt, updatedAt
        /// </summary>
        [RegularExpression("^(productName|productCode|costingTypeId|uom|quantity|unitCost|totalCost|projectId|createdAt|updatedAt)$",
            ErrorMessage = "Invalid sort column. Available values: productName, productCode, costingTypeId, uom, quantity, unitCost, totalCost, projectId, createdAt, updatedAt")]
        public string? SortColumn { get; set; }

        /// <summary>
        /// Sort direction. True for descending, false for ascending
        /// </summary>
        public bool SortDescending { get; set; }
    }
}