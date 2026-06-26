using Microsoft.AspNetCore.Identity;
using NdisAgency.Models;

namespace NdisAgency.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

        foreach (var role in new[] { "Admin", "Editor" })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@ndisagency.local";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                FullName = "NDIS Admin",
                Role = "Admin"
            };
            var result = await userManager.CreateAsync(admin, "Admin@123");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (!db.SiteSettings.Any())
        {
            db.SiteSettings.AddRange(
                new SiteSetting { Key = "site_name", Value = "CarePath NDIS Agency", Group = "general", Label = "Site Name" },
                new SiteSetting { Key = "tagline", Value = "Empowering lives through personalised NDIS support", Group = "general", Label = "Tagline" },
                new SiteSetting { Key = "phone", Value = "1300 000 000", Group = "contact", Label = "Phone" },
                new SiteSetting { Key = "email", Value = "hello@carepathndis.com.au", Group = "contact", Label = "Email" },
                new SiteSetting { Key = "address", Value = "123 Support Street, Melbourne VIC 3000", Group = "contact", Label = "Address" },
                new SiteSetting { Key = "facebook_url", Value = "", Group = "social", Label = "Facebook" },
                new SiteSetting { Key = "instagram_url", Value = "", Group = "social", Label = "Instagram" },
                new SiteSetting { Key = "linkedin_url", Value = "", Group = "social", Label = "LinkedIn" }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Pages.Any())
        {
            db.Pages.AddRange(
                new CmsPage
                {
                    Title = "Home",
                    Slug = "home",
                    PageType = "home",
                    IsPublished = true,
                    SortOrder = 0,
                    MetaTitle = "CarePath NDIS Agency | Support That Puts You First",
                    MetaDescription = "Registered NDIS provider offering plan management, support coordination, and community participation.",
                    Content = "<h1>Welcome to CarePath NDIS Agency</h1><p>We help participants navigate the NDIS with confidence.</p>"
                },
                new CmsPage
                {
                    Title = "About Us",
                    Slug = "about",
                    PageType = "about",
                    IsPublished = true,
                    SortOrder = 1,
                    Content = "<h1>About CarePath</h1><p>We are a registered NDIS provider committed to person-centred support.</p>"
                },
                new CmsPage
                {
                    Title = "Contact",
                    Slug = "contact",
                    PageType = "contact",
                    IsPublished = true,
                    SortOrder = 2,
                    Content = "<h1>Get in Touch</h1><p>Reach out to discuss your NDIS goals.</p>"
                }
            );
            await db.SaveChangesAsync();
        }

        if (!db.NavigationItems.Any())
        {
            db.NavigationItems.AddRange(
                new NavigationItem { Label = "Home", Url = "/", Location = "header", SortOrder = 0, IsPublished = true },
                new NavigationItem { Label = "About", Url = "/about", Location = "header", SortOrder = 1, IsPublished = true },
                new NavigationItem { Label = "Services", Url = "/services", Location = "header", SortOrder = 2, IsPublished = true },
                new NavigationItem { Label = "Blog", Url = "/blog", Location = "header", SortOrder = 3, IsPublished = true },
                new NavigationItem { Label = "FAQ", Url = "/faq", Location = "header", SortOrder = 4, IsPublished = true },
                new NavigationItem { Label = "Contact", Url = "/contact", Location = "header", SortOrder = 5, IsPublished = true }
            );
            await db.SaveChangesAsync();
        }

        if (!db.Services.Any())
        {
            db.Services.AddRange(
                new Service
                {
                    Title = "Plan Management",
                    Slug = "plan-management",
                    ShortDescription = "We manage your NDIS funds so you can focus on your goals.",
                    Description = "Our plan management service handles invoices, budgets, and provider payments on your behalf.",
                    Icon = "wallet",
                    IsPublished = true,
                    SortOrder = 0
                },
                new Service
                {
                    Title = "Support Coordination",
                    Slug = "support-coordination",
                    ShortDescription = "Connect with the right supports and build your capacity.",
                    Description = "We help you understand your plan, find providers, and coordinate your supports.",
                    Icon = "users",
                    IsPublished = true,
                    SortOrder = 1
                },
                new Service
                {
                    Title = "Community Participation",
                    Slug = "community-participation",
                    ShortDescription = "Social and community activities tailored to your interests.",
                    Description = "Join group activities, develop skills, and connect with your community.",
                    Icon = "heart",
                    IsPublished = true,
                    SortOrder = 2
                }
            );
            await db.SaveChangesAsync();
        }

        if (!db.FaqItems.Any())
        {
            db.FaqItems.AddRange(
                new FaqItem
                {
                    Question = "What is the NDIS?",
                    Answer = "The National Disability Insurance Scheme (NDIS) provides funding for supports and services for Australians with permanent and significant disability.",
                    Category = "General",
                    IsPublished = true,
                    SortOrder = 0
                },
                new FaqItem
                {
                    Question = "Am I eligible for NDIS support?",
                    Answer = "You may be eligible if you are under 65, an Australian citizen or resident, and have a permanent disability that affects your daily life.",
                    Category = "Eligibility",
                    IsPublished = true,
                    SortOrder = 1
                }
            );
            await db.SaveChangesAsync();
        }
    }
}
