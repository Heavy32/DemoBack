using AutoMapper;
using DemoBack.Models.Storage;
using FlowCycle.Api.Models.Storage;
using FlowCycle.Domain.Costing;
using FlowCycle.Domain.Costing.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowCycle.Api.Controllers.Storage
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostingOverheadController : ControllerBase
    {
        private readonly ICostingOverheadService _service;
        private readonly IMapper _mapper;

        public CostingOverheadController(ICostingOverheadService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostingOverheadDto>> GetById(int id, CancellationToken ct)
        {
            var overhead = await _service.GetByIdAsync(id, ct);
            return Ok(_mapper.Map<CostingOverheadDto>(overhead));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostingOverheadDto>>> GetList([FromQuery] CostingOverheadFilterDto? filter, CancellationToken ct)
        {
            var domainFilter = filter != null ? _mapper.Map<CostingOverheadFilter>(filter) : null;
            var overheads = await _service.GetListAsync(domainFilter, ct);
            return Ok(_mapper.Map<IEnumerable<CostingOverheadDto>>(overheads));
        }

        [HttpPost]
        public async Task<ActionResult<CostingOverheadDto>> Create([FromBody] CostingOverheadDto overheadDto, CancellationToken ct)
        {
            var overhead = _mapper.Map<CostingOverhead>(overheadDto);
            var createdOverhead = await _service.CreateAsync(overhead, ct);
            return Ok(_mapper.Map<CostingOverheadDto>(createdOverhead));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CostingOverheadDto>> Update(int id, [FromBody] CostingOverheadDto overheadDto, CancellationToken ct)
        {
            var overhead = _mapper.Map<CostingOverhead>(overheadDto);
            overhead.Id = id;
            var updatedOverhead = await _service.UpdateAsync(overhead, ct);
            return Ok(_mapper.Map<CostingOverheadDto>(updatedOverhead));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}