using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class TeamMember
{
    [Key]
    public int TeamMemberId { get; set; }

    [Required, StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Role { get; set; } = string.Empty;

    public string? Bio { get; set; }

    [StringLength(500)]
    public string? PhotoUrl { get; set; }

    [StringLength(200)]
    public string? Email { get; set; }

    public bool IsPublished { get; set; }
    public int SortOrder { get; set; }
}
