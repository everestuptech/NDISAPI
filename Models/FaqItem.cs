using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class FaqItem
{
    [Key]
    public int FaqId { get; set; }

    [Required, StringLength(500)]
    public string Question { get; set; } = string.Empty;

    [Required]
    public string Answer { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Category { get; set; }

    public bool IsPublished { get; set; }
    public int SortOrder { get; set; }
}
