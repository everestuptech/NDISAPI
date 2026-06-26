using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class ContactSubmission
{
    [Key]
    public int SubmissionId { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Subject { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    [StringLength(30)]
    public string Status { get; set; } = "new";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
