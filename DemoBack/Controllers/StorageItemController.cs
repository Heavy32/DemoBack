using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Storage;
using FlowCycle.Domain.Storage.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowCycle.Api.Controllers
{
    /// <summary>
    /// API for managing storage items
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Storage")]
    public class StorageItemController : ControllerBase
    {
        private readonly IStorageItemService _storageItemService;
        private readonly IMapper _mapper;

        public StorageItemController(IStorageItemService storageItemService, IMapper mapper)
        {
            _storageItemService = storageItemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a storage item by ID
        /// </summary>
        /// <param name="id">The ID of the storage item</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The requested storage item</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = "GetStorageItemById")]
        [SwaggerResponse(200, "Returns the storage item", typeof(StorageItemDto))]
        [SwaggerResponse(404, "Storage item not found")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _storageItemService.GetByIdAsync(id, ct);
            if (result == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<StorageItemDto>(result);
            return Ok(dto);
        }

        /// <summary>
        /// Get a list of storage items with optional filtering and sorting
        /// </summary>
        /// <param name="filter">Optional filter and sort parameters</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of storage items</returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "GetStorageItems")]
        [SwaggerResponse(200, "Returns the list of storage items", typeof(IEnumerable<StorageItemDto>))]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var result = await _storageItemService.GetAllAsync(ct);
            var dto = result.Select(_mapper.Map<StorageItemDto>);
            return Ok(dto);
        }

        /// <summary>
        /// Create a new storage item
        /// </summary>
        /// <param name="StorageItemDto">Storage item data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The created storage item</returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "CreateStorageItem")]
        [SwaggerResponse(201, "Storage item created", typeof(StorageItemDto))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<IActionResult> Create(
            [FromBody, SwaggerRequestBody("Storage item payload", Required = true)] StorageItemDto StorageItemDto,
            CancellationToken ct)
        {
            var StorageItem = _mapper.Map<StorageItem>(StorageItemDto);
            var result = await _storageItemService.CreateAsync(StorageItem, ct);
            var resultDto = _mapper.Map<StorageItemDto>(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, resultDto);
        }

        /// <summary>
        /// Update an existing storage item
        /// </summary>
        /// <param name="id">ID of the storage item to update</param>
        /// <param name="StorageItemDto">Updated storage item data</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The updated storage item</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = "UpdateStorageItem")]
        [SwaggerResponse(200, "Storage item updated", typeof(StorageItemDto))]
        [SwaggerResponse(400, "Invalid input data")]
        [SwaggerResponse(404, "Storage item not found")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody, SwaggerRequestBody("Updated storage item payload", Required = true)] StorageItemDto StorageItemDto,
            CancellationToken ct)
        {
            var StorageItem = _mapper.Map<StorageItem>(StorageItemDto);
            StorageItem.Id = id;
            var result = await _storageItemService.UpdateAsync(StorageItem, ct);

            if (result == null)
            {
                return NotFound();
            }

            var resultDto = _mapper.Map<StorageItemDto>(result);
            return Ok(resultDto);
        }

        /// <summary>
        /// Delete a storage item
        /// </summary>
        /// <param name="id">ID of the storage item to delete</param>
        /// <param name="ct">Cancellation token</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(OperationId = "DeleteStorageItem")]
        [SwaggerResponse(204, "Storage item deleted")]
        [SwaggerResponse(404, "Storage item not found")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var StorageItem = await _storageItemService.GetByIdAsync(id, ct);
            if (StorageItem == null)
            {
                return NotFound();
            }

            await _storageItemService.DeleteAsync(id, ct);
            return NoContent();
        }

        /// <summary>
        /// Archive or unarchive a storage item
        /// </summary>
        /// <param name="id">ID of the storage item</param>
        /// <param name="isArchived">Whether to archive (true) or unarchive (false) the item</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The updated storage item</returns>
        [HttpPatch("{id}/archive")]
        [SwaggerOperation(OperationId = "ArchiveStorageItem")]
        [SwaggerResponse(200, "Storage item archive status updated", typeof(StorageItemDto))]
        [SwaggerResponse(404, "Storage item not found")]
        public async Task<IActionResult> Archive(
            int id,
            [FromQuery] bool isArchived = true,
            CancellationToken ct = default)
        {
            var existingItem = await _storageItemService.GetByIdAsync(id, ct);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.IsArchived = isArchived;
            var result = await _storageItemService.UpdateAsync(existingItem, ct);

            var dto = _mapper.Map<StorageItemDto>(result);
            return Ok(dto);
        }
    }
}