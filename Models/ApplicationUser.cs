using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NdisAgency.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string Role { get; set; } = "Editor";
}

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CmsPage> Pages { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<NavigationItem> NavigationItems { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<FaqItem> FaqItems { get; set; }
    public DbSet<ContactSubmission> ContactSubmissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CmsPage>()
            .HasIndex(p => p.Slug)
            .IsUnique();

        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();

        modelBuilder.Entity<SiteSetting>()
            .HasIndex(s => s.Key)
            .IsUnique();

        modelBuilder.Entity<NavigationItem>()
            .HasOne(n => n.Parent)
            .WithMany(n => n.Children)
            .HasForeignKey(n => n.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
