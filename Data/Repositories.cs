using Microsoft.EntityFrameworkCore;
using NdisAgency.Models;

namespace NdisAgency.Data;

public class PageRepository
{
    private readonly ApplicationDbContext _context;
    public PageRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<CmsPage>> GetAllAsync(bool publishedOnly = false) =>
        await _context.Pages
            .Where(p => !publishedOnly || p.IsPublished)
            .OrderBy(p => p.SortOrder).ThenByDescending(p => p.CreatedAt)
            .ToListAsync();

    public async Task<CmsPage?> GetByIdAsync(int id) => await _context.Pages.FindAsync(id);

    public async Task<CmsPage?> GetBySlugAsync(string slug) =>
        await _context.Pages.FirstOrDefaultAsync(p => p.Slug == slug);

    public async Task<CmsPage> CreateAsync(CmsPage page)
    {
        page.CreatedAt = DateTime.UtcNow;
        _context.Pages.Add(page);
        await _context.SaveChangesAsync();
        return page;
    }

    public async Task UpdateAsync(CmsPage page)
    {
        page.UpdatedAt = DateTime.UtcNow;
        _context.Entry(page).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Pages.FindAsync(id);
        if (item != null) { _context.Pages.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class SiteSettingRepository
{
    private readonly ApplicationDbContext _context;
    public SiteSettingRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<SiteSetting>> GetAllAsync() =>
        await _context.SiteSettings.OrderBy(s => s.Group).ThenBy(s => s.Key).ToListAsync();

    public async Task<SiteSetting?> GetByKeyAsync(string key) =>
        await _context.SiteSettings.FirstOrDefaultAsync(s => s.Key == key);

    public async Task UpsertAsync(SiteSetting setting)
    {
        var existing = await _context.SiteSettings.FirstOrDefaultAsync(s => s.Key == setting.Key);
        if (existing == null)
            _context.SiteSettings.Add(setting);
        else
        {
            existing.Value = setting.Value;
            existing.Label = setting.Label;
            existing.Group = setting.Group;
        }
        await _context.SaveChangesAsync();
    }

    public async Task BulkUpsertAsync(IEnumerable<SiteSetting> settings)
    {
        foreach (var setting in settings)
            await UpsertAsync(setting);
    }
}

public class NavigationRepository
{
    private readonly ApplicationDbContext _context;
    public NavigationRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<NavigationItem>> GetAllAsync(bool publishedOnly = false) =>
        await _context.NavigationItems
            .Include(n => n.Children)
            .Where(n => n.ParentId == null && (!publishedOnly || n.IsPublished))
            .OrderBy(n => n.SortOrder)
            .ToListAsync();

    public async Task<NavigationItem?> GetByIdAsync(int id) => await _context.NavigationItems.FindAsync(id);

    public async Task<NavigationItem> CreateAsync(NavigationItem item)
    {
        _context.NavigationItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(NavigationItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.NavigationItems.FindAsync(id);
        if (item != null) { _context.NavigationItems.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class ServiceRepository
{
    private readonly ApplicationDbContext _context;
    public ServiceRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Service>> GetAllAsync(bool publishedOnly = false) =>
        await _context.Services
            .Where(s => !publishedOnly || s.IsPublished)
            .OrderBy(s => s.SortOrder)
            .ToListAsync();

    public async Task<Service?> GetByIdAsync(int id) => await _context.Services.FindAsync(id);

    public async Task<Service> CreateAsync(Service service)
    {
        service.CreatedAt = DateTime.UtcNow;
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task UpdateAsync(Service service)
    {
        service.UpdatedAt = DateTime.UtcNow;
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Services.FindAsync(id);
        if (item != null) { _context.Services.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class TeamMemberRepository
{
    private readonly ApplicationDbContext _context;
    public TeamMemberRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<TeamMember>> GetAllAsync(bool publishedOnly = false) =>
        await _context.TeamMembers
            .Where(t => !publishedOnly || t.IsPublished)
            .OrderBy(t => t.SortOrder)
            .ToListAsync();

    public async Task<TeamMember?> GetByIdAsync(int id) => await _context.TeamMembers.FindAsync(id);

    public async Task<TeamMember> CreateAsync(TeamMember member)
    {
        _context.TeamMembers.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task UpdateAsync(TeamMember member)
    {
        _context.Entry(member).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.TeamMembers.FindAsync(id);
        if (item != null) { _context.TeamMembers.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class TestimonialRepository
{
    private readonly ApplicationDbContext _context;
    public TestimonialRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Testimonial>> GetAllAsync(bool publishedOnly = false) =>
        await _context.Testimonials
            .Where(t => !publishedOnly || t.IsPublished)
            .OrderBy(t => t.SortOrder)
            .ToListAsync();

    public async Task<Testimonial?> GetByIdAsync(int id) => await _context.Testimonials.FindAsync(id);

    public async Task<Testimonial> CreateAsync(Testimonial testimonial)
    {
        testimonial.CreatedAt = DateTime.UtcNow;
        _context.Testimonials.Add(testimonial);
        await _context.SaveChangesAsync();
        return testimonial;
    }

    public async Task UpdateAsync(Testimonial testimonial)
    {
        _context.Entry(testimonial).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Testimonials.FindAsync(id);
        if (item != null) { _context.Testimonials.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class BlogPostRepository
{
    private readonly ApplicationDbContext _context;
    public BlogPostRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<BlogPost>> GetAllAsync(bool publishedOnly = false) =>
        await _context.BlogPosts
            .Where(b => !publishedOnly || b.IsPublished)
            .OrderByDescending(b => b.PublishedAt ?? b.CreatedAt)
            .ToListAsync();

    public async Task<BlogPost?> GetByIdAsync(int id) => await _context.BlogPosts.FindAsync(id);

    public async Task<BlogPost?> GetBySlugAsync(string slug) =>
        await _context.BlogPosts.FirstOrDefaultAsync(b => b.Slug == slug);

    public async Task<BlogPost> CreateAsync(BlogPost post)
    {
        post.CreatedAt = DateTime.UtcNow;
        _context.BlogPosts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(BlogPost post)
    {
        post.UpdatedAt = DateTime.UtcNow;
        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.BlogPosts.FindAsync(id);
        if (item != null) { _context.BlogPosts.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class FaqRepository
{
    private readonly ApplicationDbContext _context;
    public FaqRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<FaqItem>> GetAllAsync(bool publishedOnly = false) =>
        await _context.FaqItems
            .Where(f => !publishedOnly || f.IsPublished)
            .OrderBy(f => f.SortOrder)
            .ToListAsync();

    public async Task<FaqItem?> GetByIdAsync(int id) => await _context.FaqItems.FindAsync(id);

    public async Task<FaqItem> CreateAsync(FaqItem faq)
    {
        _context.FaqItems.Add(faq);
        await _context.SaveChangesAsync();
        return faq;
    }

    public async Task UpdateAsync(FaqItem faq)
    {
        _context.Entry(faq).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.FaqItems.FindAsync(id);
        if (item != null) { _context.FaqItems.Remove(item); await _context.SaveChangesAsync(); }
    }
}

public class ContactSubmissionRepository
{
    private readonly ApplicationDbContext _context;
    public ContactSubmissionRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<ContactSubmission>> GetAllAsync() =>
        await _context.ContactSubmissions.OrderByDescending(c => c.CreatedAt).ToListAsync();

    public async Task<ContactSubmission?> GetByIdAsync(int id) =>
        await _context.ContactSubmissions.FindAsync(id);

    public async Task<ContactSubmission> CreateAsync(ContactSubmission submission)
    {
        submission.CreatedAt = DateTime.UtcNow;
        _context.ContactSubmissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        var item = await _context.ContactSubmissions.FindAsync(id);
        if (item != null)
        {
            item.Status = status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.ContactSubmissions.FindAsync(id);
        if (item != null) { _context.ContactSubmissions.Remove(item); await _context.SaveChangesAsync(); }
    }
}
