
using FlowCycle.Domain.Stock;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowCycle.Api.Controllers
{
    /// <summary>
    /// API for exporting stock items to Excel files
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Storage")]
    public class StockItemExportController : ControllerBase
    {
        private readonly IStockItemExportService _exportService;

        public StockItemExportController(IStockItemExportService exportService)
        {
            _exportService = exportService;
        }

        /// <summary>
        /// Export all stock items to an Excel file
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Excel file containing stock items data</returns>
        [HttpGet("excel")]
        [SwaggerOperation(OperationId = "ExportStockItemsToExcel")]
        [SwaggerResponse(200, "Excel file generated successfully")]
        [SwaggerResponse(500, "Error generating Excel file")]
        public async Task<IActionResult> ExportToExcel(CancellationToken ct = default)
        {
            try
            {
                var stream = await _exportService.ExportToExcelAsync(ct);
                var fileName = $"StockItems_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating Excel file: {ex.Message}");
            }
        }
    }
}