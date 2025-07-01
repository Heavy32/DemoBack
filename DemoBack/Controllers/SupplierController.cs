using FlowCycle.Domain.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DemoBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;
        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var items = await _service.GetAllAsync(ct);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var item = await _service.GetByIdAsync(id, ct);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier, CancellationToken ct)
        {
            var created = await _service.CreateAsync(supplier, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Supplier supplier, CancellationToken ct)
        {
            if (id != supplier.Id) return BadRequest();
            var updated = await _service.UpdateAsync(supplier, ct);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await _service.DeleteAsync(id, ct);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}