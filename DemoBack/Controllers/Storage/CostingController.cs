using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Domain.Costing;
using Microsoft.AspNetCore.Mvc;

namespace FlowCycle.Api.Controllers.Storage
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostingController : ControllerBase
    {
        private readonly ICostingService _costingService;
        private readonly IMapper _mapper;

        public CostingController(ICostingService costingService, IMapper mapper)
        {
            _costingService = costingService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostingDto>> GetById(int id, CancellationToken ct)
        {
            var costing = await _costingService.GetByIdAsync(id, ct);
            return Ok(_mapper.Map<CostingDto>(costing));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostingDto>>> GetList([FromQuery] CostingFilterDto? filter, CancellationToken ct)
        {
            var costings = await _costingService.GetListAsync(_mapper.Map<Domain.Storage.Models.CostingFilter>(filter), ct);
            return Ok(costings.Select(_mapper.Map<CostingDto>));
        }

        [HttpPost]
        public async Task<ActionResult<CostingDto>> Create([FromBody] CostingDto costing, CancellationToken ct)
        {
            var created = await _costingService.CreateAsync(_mapper.Map<CostingModel>(costing), ct);
            return Ok(_mapper.Map<CostingDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CostingDto>> Update(int id, [FromBody] CostingDto costing, CancellationToken ct)
        {
            var domain = _mapper.Map<CostingModel>(costing);
            domain.Id = id;
            var updated = await _costingService.UpdateAsync(domain, ct);
            return Ok(_mapper.Map<CostingDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _costingService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}