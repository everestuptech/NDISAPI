using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class SiteSetting
{
    [Key]
    public int SettingId { get; set; }

    [Required, StringLength(100)]
    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    [StringLength(50)]
    public string Group { get; set; } = "general";

    [StringLength(200)]
    public string? Label { get; set; }
}
