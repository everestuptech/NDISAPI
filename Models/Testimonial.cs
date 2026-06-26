using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class Testimonial
{
    [Key]
    public int TestimonialId { get; set; }

    [Required, StringLength(150)]
    public string ClientName { get; set; } = string.Empty;

    [StringLength(150)]
    public string? ClientRole { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    public int Rating { get; set; } = 5;
    public bool IsPublished { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
