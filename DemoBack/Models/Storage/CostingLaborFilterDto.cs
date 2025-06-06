using System.ComponentModel.DataAnnotations;

namespace DemoBack.Models.Storage
{
    public class CostingLaborFilterDto
    {
        public int? CostingId { get; set; }

        public string? LaborName { get; set; }

        [RegularExpression(@"^(laborName|hours|hourRate|totalValue|note)$",
            ErrorMessage = "Invalid sort column. Valid values are: laborName, hours, hourRate, totalValue, note")]
        public string? SortColumn { get; set; }

        public bool SortDescending { get; set; }
    }
}