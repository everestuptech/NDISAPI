using System.ComponentModel.DataAnnotations;

namespace NdisAgency.Models;

public class NavigationItem
{
    [Key]
    public int NavItemId { get; set; }

    [Required, StringLength(100)]
    public string Label { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string Url { get; set; } = string.Empty;

    [StringLength(20)]
    public string Location { get; set; } = "header";

    public int? ParentId { get; set; }
    public virtual NavigationItem? Parent { get; set; }
    public virtual ICollection<NavigationItem> Children { get; set; } = new List<NavigationItem>();

    public int SortOrder { get; set; }
    public bool IsPublished { get; set; } = true;
    public bool OpenInNewTab { get; set; }
}
