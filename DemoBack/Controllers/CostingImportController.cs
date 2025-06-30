using FlowCycle.Domain.Costing;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using DemoBack.Models.Storage;

namespace FlowCycle.Api.Controllers
{
    /// <summary>
    /// API for importing costing data from Excel files
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Costing")]
    public class CostingImportController : ControllerBase
    {
        private readonly ICostingImportService _importService;
        private readonly IMapper _mapper;

        public CostingImportController(ICostingImportService importService, IMapper mapper)
        {
            _importService = importService;
            _mapper = mapper;
        }

        /// <summary>
        /// Import costing data from an Excel file
        /// </summary>
        /// <param name="file">The Excel file to import</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The imported costing data</returns>
        [HttpPost("excel")]
        [SwaggerOperation(OperationId = "ImportCostingFromExcel")]
        [SwaggerResponse(200, "Costing data imported successfully")]
        [SwaggerResponse(400, "Invalid file format or content")]
        public async Task<IActionResult> ImportFromExcel(
            IFormFile file,
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
            var costing = await _importService.ImportFromExcelAsync(stream, ct);

            var costingDtos = _mapper.Map<CostingDto[]>(costing);
            return Ok(costingDtos);
        }
    }
}