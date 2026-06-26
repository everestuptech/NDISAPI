using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NdisAgency.Data;
using NdisAgency.Models;

namespace NdisAgency.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NavigationController : ControllerBase
{
    private readonly NavigationRepository _repo;
    public NavigationController(NavigationRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NavigationItem>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<NavigationItem>> Create(NavigationItem item)
    {
        var created = await _repo.CreateAsync(item);
        return CreatedAtAction(nameof(GetAll), new { id = created.NavItemId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, NavigationItem item)
    {
        if (id != item.NavItemId) return BadRequest();
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return NotFound();
        await _repo.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
