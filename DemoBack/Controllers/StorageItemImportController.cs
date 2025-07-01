using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowCycle.Api.Controllers
{
    /// <summary>
    /// API for importing Storage items from Excel files
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Storage")]
    public class StorageItemImportController : ControllerBase
    {
        private readonly IStorageItemImportService _importService;
        private readonly IMapper _mapper;

        public StorageItemImportController(IStorageItemImportService importService, IMapper mapper)
        {
            _importService = importService;
            _mapper = mapper;
        }

        /// <summary>
        /// Import Storage items from an Excel file
        /// </summary>
        /// <param name="file">The Excel file to import</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The list of imported Storage items</returns>
        [HttpPost("excel")]
        [SwaggerOperation(OperationId = "ImportStorageItemsFromExcel")]
        [SwaggerResponse(200, "Storage items imported successfully", typeof(IEnumerable<StorageItemDto>))]
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
            var importedItems = await _importService.ImportFromExcelAsync(stream, ct);
            var dtos = _mapper.Map<IEnumerable<StorageItemDto>>(importedItems);

            return Ok(dtos);
        }
    }
}