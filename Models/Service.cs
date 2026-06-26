using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class Service
{
    [Key]
    public int ServiceId { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string? ShortDescription { get; set; }

    [StringLength(100)]
    public string? Icon { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    public bool IsPublished { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
