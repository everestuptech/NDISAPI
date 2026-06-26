using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdisAgency.Data;
using NdisAgency.Models;

namespace NdisAgency.Controllers;

/// <summary>
/// Public read-only API for the consumer-facing NDIS website.
/// Returns only published content — no authentication required.
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/public")]
public class PublicController : ControllerBase
{
    private readonly PageRepository _pages;
    private readonly SiteSettingRepository _settings;
    private readonly NavigationRepository _navigation;
    private readonly ServiceRepository _services;
    private readonly TeamMemberRepository _team;
    private readonly TestimonialRepository _testimonials;
    private readonly BlogPostRepository _blog;
    private readonly FaqRepository _faqs;
    private readonly ContactSubmissionRepository _contacts;
    private readonly ApplicationDbContext _db;

    public PublicController(
        PageRepository pages,
        SiteSettingRepository settings,
        NavigationRepository navigation,
        ServiceRepository services,
        TeamMemberRepository team,
        TestimonialRepository testimonials,
        BlogPostRepository blog,
        FaqRepository faqs,
        ContactSubmissionRepository contacts,
        ApplicationDbContext db)
    {
        _pages = pages;
        _settings = settings;
        _navigation = navigation;
        _services = services;
        _team = team;
        _testimonials = testimonials;
        _blog = blog;
        _faqs = faqs;
        _contacts = contacts;
        _db = db;
    }

    [HttpGet("site")]
    public async Task<IActionResult> GetSiteBundle()
    {
        var settings = await _settings.GetAllAsync();
        var navigation = await _navigation.GetAllAsync(publishedOnly: true);

        return Ok(new
        {
            settings = settings.ToDictionary(s => s.Key, s => s.Value),
            navigation
        });
    }

    [HttpGet("pages")]
    public async Task<IActionResult> GetPages() =>
        Ok(await _pages.GetAllAsync(publishedOnly: true));

    [HttpGet("pages/{slug}")]
    public async Task<IActionResult> GetPageBySlug(string slug)
    {
        var page = await _pages.GetBySlugAsync(slug);
        if (page == null || !page.IsPublished) return NotFound();
        return Ok(page);
    }

    [HttpGet("services")]
    public async Task<IActionResult> GetServices() =>
        Ok(await _services.GetAllAsync(publishedOnly: true));

    [HttpGet("team")]
    public async Task<IActionResult> GetTeam() =>
        Ok(await _team.GetAllAsync(publishedOnly: true));

    [HttpGet("testimonials")]
    public async Task<IActionResult> GetTestimonials() =>
        Ok(await _testimonials.GetAllAsync(publishedOnly: true));

    [HttpGet("blog")]
    public async Task<IActionResult> GetBlogPosts() =>
        Ok(await _blog.GetAllAsync(publishedOnly: true));

    [HttpGet("blog/{slug}")]
    public async Task<IActionResult> GetBlogPostBySlug(string slug)
    {
        var post = await _blog.GetBySlugAsync(slug);
        if (post == null || !post.IsPublished) return NotFound();
        return Ok(post);
    }

    [HttpGet("faqs")]
    public async Task<IActionResult> GetFaqs() =>
        Ok(await _faqs.GetAllAsync(publishedOnly: true));

    [HttpPost("contact")]
    public async Task<IActionResult> SubmitContact([FromBody] ContactSubmission submission)
    {
        var created = await _contacts.CreateAsync(submission);
        return Ok(new { message = "Thank you for your enquiry. We will be in touch soon.", id = created.SubmissionId });
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DashboardController(ApplicationDbContext db) => _db = db;

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        return Ok(new
        {
            pages = await _db.Pages.CountAsync(),
            publishedPages = await _db.Pages.CountAsync(p => p.IsPublished),
            services = await _db.Services.CountAsync(),
            blogPosts = await _db.BlogPosts.CountAsync(),
            newContacts = await _db.ContactSubmissions.CountAsync(c => c.Status == "new"),
            teamMembers = await _db.TeamMembers.CountAsync(),
            testimonials = await _db.Testimonials.CountAsync()
        });
    }
}
