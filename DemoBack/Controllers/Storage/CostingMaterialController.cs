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
    public class CostingMaterialController : ControllerBase
    {
        private readonly ICostingMaterialService _service;
        private readonly IMapper _mapper;

        public CostingMaterialController(ICostingMaterialService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostingMaterialDto>> GetById(int id, CancellationToken ct)
        {
            var material = await _service.GetByIdAsync(id, ct);
            return Ok(_mapper.Map<CostingMaterialDto>(material));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostingMaterialDto>>> GetList([FromQuery] CostingMaterialFilterDto? filter, CancellationToken ct)
        {
            var domainFilter = filter != null ? _mapper.Map<CostingMaterialFilter>(filter) : null;
            var materials = await _service.GetListAsync(domainFilter, ct);
            return Ok(_mapper.Map<IEnumerable<CostingMaterialDto>>(materials));
        }

        [HttpPost]
        public async Task<ActionResult<CostingMaterialDto>> Create([FromBody] CostingMaterialDto materialDto, CancellationToken ct)
        {
            var material = _mapper.Map<CostingMaterial>(materialDto);
            var createdMaterial = await _service.CreateAsync(material, ct);
            return Ok(_mapper.Map<CostingMaterialDto>(createdMaterial));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CostingMaterialDto>> Update(int id, [FromBody] CostingMaterialDto materialDto, CancellationToken ct)
        {
            var material = _mapper.Map<CostingMaterial>(materialDto);
            material.Id = id;
            var updatedMaterial = await _service.UpdateAsync(material, ct);
            return Ok(_mapper.Map<CostingMaterialDto>(updatedMaterial));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}