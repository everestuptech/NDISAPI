using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class BlogPost
{
    [Key]
    public int BlogPostId { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    public string? Excerpt { get; set; }
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? FeaturedImageUrl { get; set; }

    [StringLength(100)]
    public string? Category { get; set; }

    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? AuthorId { get; set; }
}
