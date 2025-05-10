using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Stock;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowCycle.Api.Controllers
{
    /// <summary>
    /// API for importing stock items from Excel files
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Storage")]
    public class StockItemImportController : ControllerBase
    {
        private readonly IStockItemImportService _importService;
        private readonly IMapper _mapper;

        public StockItemImportController(IStockItemImportService importService, IMapper mapper)
        {
            _importService = importService;
            _mapper = mapper;
        }

        /// <summary>
        /// Import stock items from an Excel file
        /// </summary>
        /// <param name="file">The Excel file to import</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The list of imported stock items</returns>
        [HttpPost("excel")]
        [SwaggerOperation(OperationId = "ImportStockItemsFromExcel")]
        [SwaggerResponse(200, "Stock items imported successfully", typeof(IEnumerable<StockItemDto>))]
        [SwaggerResponse(400, "Invalid file format or content")]
        public async Task<IActionResult> ImportFromExcel(
            [FromForm] IFormFile file,
            CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only .xlsx files are supported");
            }

            using var stream = file.OpenReadStream();
            var importedItems = await _importService.ImportFromExcelAsync(stream, ct);
            var dtos = _mapper.Map<IEnumerable<StockItemDto>>(importedItems);

            return Ok(dtos);
        }
    }
} 