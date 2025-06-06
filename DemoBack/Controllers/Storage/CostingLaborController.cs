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
    public class CostingLaborController : ControllerBase
    {
        private readonly ICostingLaborService _service;
        private readonly IMapper _mapper;

        public CostingLaborController(ICostingLaborService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostingLaborDto>> GetById(int id, CancellationToken ct)
        {
            var labor = await _service.GetByIdAsync(id, ct);
            return Ok(_mapper.Map<CostingLaborDto>(labor));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostingLaborDto>>> GetList([FromQuery] CostingLaborFilterDto? filter, CancellationToken ct)
        {
            var domainFilter = filter != null ? _mapper.Map<CostingLaborFilter>(filter) : null;
            var labors = await _service.GetListAsync(domainFilter, ct);
            return Ok(_mapper.Map<IEnumerable<CostingLaborDto>>(labors));
        }

        [HttpPost]
        public async Task<ActionResult<CostingLaborDto>> Create([FromBody] CostingLaborDto laborDto, CancellationToken ct)
        {
            var labor = _mapper.Map<CostingLabor>(laborDto);
            var createdLabor = await _service.CreateAsync(labor, ct);
            return Ok(_mapper.Map<CostingLaborDto>(createdLabor));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CostingLaborDto>> Update(int id, [FromBody] CostingLaborDto laborDto, CancellationToken ct)
        {
            var labor = _mapper.Map<CostingLabor>(laborDto);
            labor.Id = id;
            var updatedLabor = await _service.UpdateAsync(labor, ct);
            return Ok(_mapper.Map<CostingLaborDto>(updatedLabor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}