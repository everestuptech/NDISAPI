# NdisAgency API

PostgreSQL CMS backend for NDIS agency website. Reference implementation: `api/Inventory`.

## Run

```bash
dotnet run   # http://localhost:5202/swagger
```

## Config

- DB: `NdisAgencyDb` — `appsettings.json` → `ConnectionStrings:DefaultConnection`
- JWT: `appsettings.json` → `Jwt:*`
- CORS: `appsettings.json` → `Cors:AllowedOrigins` (dev: localhost:3001/3002; prod: required)
- Seed: `Data/DataSeeder.cs` — admin `admin` / `Admin@123`

## Structure

| Path | Purpose |
|------|---------|
| `Program.cs` | DI, JWT, CORS, migrate + seed on startup |
| `Models/ApplicationUser.cs` | `ApplicationDbContext` + Identity user |
| `Models/*.cs` | CMS entities |
| `Data/Repositories.cs` | All repositories |
| `Controllers/PublicController.cs` | Anonymous `/api/public/*` |
| `Controllers/*.cs` | JWT-protected admin CRUD |

## Entities

Pages, SiteSettings, NavigationItems, Services, TeamMembers, Testimonials, BlogPosts, FaqItems, ContactSubmissions

## Add entity checklist

1. Model → DbSet in `ApplicationDbContext` → repository → register in `Program.cs`
2. `[Authorize]` controller + optional public GET in `PublicController`
3. `dotnet ef migrations add <Name>` && `dotnet ef database update`
4. Update `web/ndis-admin/lib/api.ts` + admin page (+ `web/ndis-site/lib/public-api.ts` if public-facing)
5. Update `.cursor/skills/ndis-cms/reference.md` if endpoints change

## Admin endpoints (JWT)

`Auth`, `Pages`, `SiteSettings`, `Navigation`, `Services`, `TeamMembers`, `Testimonials`, `BlogPosts`, `Faqs`, `ContactSubmissions`, `Dashboard`

## Public endpoints

`/api/public/site`, `/pages`, `/pages/{slug}`, `/services`, `/team`, `/testimonials`, `/blog`, `/blog/{slug}`, `/faqs`, `POST /contact`
