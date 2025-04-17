using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Stock;
using FlowCycle.Domain.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlowCycle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageItemController : ControllerBase
    {
        private readonly IStorageItemService _storageItemService;
        private readonly IMapper _mapper;

        public StorageItemController(IStorageItemService storageItemService, IMapper mapper)
        {
            _storageItemService = storageItemService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _storageItemService.GetById(id, ct);
            var dto = _mapper.Map<StockItemDto>(result);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(CancellationToken ct)
        {
            var result = await _storageItemService.GetList(ct);
            var dto = result.Select(_mapper.Map<StockItemDto>);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockItemDto stockItemDto, CancellationToken ct)
        {
            var stockItem = _mapper.Map<StockItem>(stockItemDto);
            var result = await _storageItemService.Create(stockItem, ct);
            var resultDto = _mapper.Map<StockItemDto>(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] StockItemDto stockItemDto, int id, CancellationToken ct)
        {
            var stockItem = _mapper.Map<StockItem>(stockItemDto);
            var result = await _storageItemService.Update(stockItem, id, ct);
            var resultDto = _mapper.Map<StockItemDto>(result);

            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var stockItem = await _storageItemService.GetById(id, ct);
            await _storageItemService.Delete(stockItem, ct);

            return NoContent();
        }
    }
}