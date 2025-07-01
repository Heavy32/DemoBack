using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Api.Storage
{
    /// <summary>
    /// Filter and sort parameters for Storage items
    /// </summary>
    public class StorageItemFilterDto
    {
        /// <summary>
        /// Filter by item name (case-insensitive contains)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Filter by supplier name (case-insensitive contains)
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// Filter by item code (case-insensitive contains)
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Column to sort by. Available values: name, code, supplier, category, project, price, quantity, receiptDate, isArchived
        /// </summary>
        [RegularExpression("^(name|code|supplier|category|project|price|quantity|receiptDate|isArchived)$", 
            ErrorMessage = "Invalid sort column. Available values: name, code, supplier, category, project, price, quantity, receiptDate, isArchived")]
        public string? SortColumn { get; set; }

        /// <summary>
        /// Sort direction. True for descending, false for ascending
        /// </summary>
        public bool SortDescending { get; set; }
    }
} 