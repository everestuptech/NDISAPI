using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class CmsPage
{
    [Key]
    public int PageId { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    [StringLength(200)]
    public string? MetaTitle { get; set; }

    [StringLength(500)]
    public string? MetaDescription { get; set; }

    [StringLength(100)]
    public string PageType { get; set; } = "custom";

    public bool IsPublished { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedById { get; set; }
}
