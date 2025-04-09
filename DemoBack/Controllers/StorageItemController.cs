using AutoMapper;
using FlowCycle.Api.Storage;
using FlowCycle.Domain.Stock;
using Microsoft.AspNetCore.Mvc;

namespace FlowCycle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageItemController : ControllerBase
    {
        private readonly IStorageItemService storageItemService;
        private readonly IMapper mapper;

        public StorageItemController(IStorageItemService storageItemService, IMapper mapper)
        {
            this.storageItemService = storageItemService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> GetStockItemById(int id, CancellationToken ct)
        {
            var result = await storageItemService.GetById(id, ct);
            return Ok(result);
        }
    }
}
