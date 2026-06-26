using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NdisAgency.Data;
using NdisAgency.Models;
using System.Security.Claims;

namespace NdisAgency.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly ServiceRepository _repo;
    public ServicesController(ServiceRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Service>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Service>> Create(Service service)
    {
        var created = await _repo.CreateAsync(service);
        return CreatedAtAction(nameof(GetById), new { id = created.ServiceId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Service service)
    {
        if (id != service.ServiceId) return BadRequest();
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.UpdateAsync(service);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly TeamMemberRepository _repo;
    public TeamMembersController(TeamMemberRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<TeamMember>> Create(TeamMember member)
    {
        var created = await _repo.CreateAsync(member);
        return CreatedAtAction(nameof(GetAll), new { id = created.TeamMemberId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TeamMember member)
    {
        if (id != member.TeamMemberId) return BadRequest();
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.UpdateAsync(member);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TestimonialsController : ControllerBase
{
    private readonly TestimonialRepository _repo;
    public TestimonialsController(TestimonialRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Testimonial>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<Testimonial>> Create(Testimonial testimonial)
    {
        var created = await _repo.CreateAsync(testimonial);
        return CreatedAtAction(nameof(GetAll), new { id = created.TestimonialId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Testimonial testimonial)
    {
        if (id != testimonial.TestimonialId) return BadRequest();
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.UpdateAsync(testimonial);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly BlogPostRepository _repo;
    public BlogPostsController(BlogPostRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPost>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPost>> GetById(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        return post == null ? NotFound() : Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<BlogPost>> Create(BlogPost post)
    {
        post.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (post.IsPublished && post.PublishedAt == null)
            post.PublishedAt = DateTime.UtcNow;
        var created = await _repo.CreateAsync(post);
        return CreatedAtAction(nameof(GetById), new { id = created.BlogPostId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BlogPost post)
    {
        if (id != post.BlogPostId) return BadRequest();
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return NotFound();
        if (post.IsPublished && existing.PublishedAt == null)
            post.PublishedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(post);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FaqsController : ControllerBase
{
    private readonly FaqRepository _repo;
    public FaqsController(FaqRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FaqItem>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<FaqItem>> Create(FaqItem faq)
    {
        var created = await _repo.CreateAsync(faq);
        return CreatedAtAction(nameof(GetAll), new { id = created.FaqId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FaqItem faq)
    {
        if (id != faq.FaqId) return BadRequest();
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.UpdateAsync(faq);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactSubmissionsController : ControllerBase
{
    private readonly ContactSubmissionRepository _repo;
    public ContactSubmissionsController(ContactSubmissionRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactSubmission>>> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactSubmission>> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdate model)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.UpdateStatusAsync(id, model.Status);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _repo.GetByIdAsync(id) == null) return NotFound();
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}

public class StatusUpdate
{
    public string Status { get; set; } = "read";
}
