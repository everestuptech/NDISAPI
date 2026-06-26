using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NdisAgency.Data;
using NdisAgency.Models;
using System.Security.Claims;

namespace NdisAgency.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PagesController : ControllerBase
{
    private readonly PageRepository _repo;
    public PagesController(PageRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CmsPage>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<CmsPage>> GetById(int id)
    {
        var page = await _repo.GetByIdAsync(id);
        return page == null ? NotFound() : Ok(page);
    }

    [HttpPost]
    public async Task<ActionResult<CmsPage>> Create(CmsPage page)
    {
        page.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var created = await _repo.CreateAsync(page);
        return CreatedAtAction(nameof(GetById), new { id = created.PageId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CmsPage page)
    {
        if (id != page.PageId) return BadRequest();
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = page.Title;
        existing.Slug = page.Slug;
        existing.Content = page.Content;
        existing.MetaTitle = page.MetaTitle;
        existing.MetaDescription = page.MetaDescription;
        existing.PageType = page.PageType;
        existing.IsPublished = page.IsPublished;
        existing.SortOrder = page.SortOrder;
        existing.UpdatedById = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _repo.UpdateAsync(existing);
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
