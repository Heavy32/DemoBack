using FlowCycle.Domain.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DemoBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
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
        public async Task<IActionResult> Create(Category category, CancellationToken ct)
        {
            var created = await _service.CreateAsync(category, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category, CancellationToken ct)
        {
            if (id != category.Id) return BadRequest();
            var updated = await _service.UpdateAsync(category, ct);
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