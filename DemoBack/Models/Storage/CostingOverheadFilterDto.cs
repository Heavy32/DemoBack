using System.ComponentModel.DataAnnotations;

namespace FlowCycle.Api.Models.Storage
{
    public class CostingOverheadFilterDto
    {
        public int? CostingId { get; set; }

        public string? OverheadName { get; set; }

        public string? OverheadType { get; set; }

        [RegularExpression(@"^(overheadName|overheadType|costValue|note)$",
            ErrorMessage = "Invalid sort column. Valid values are: overheadName, overheadType, costValue, note")]
        public string? SortColumn { get; set; }

        public bool SortDescending { get; set; }
    }
}