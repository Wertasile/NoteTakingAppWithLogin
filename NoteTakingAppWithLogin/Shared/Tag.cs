using System.ComponentModel.DataAnnotations;

namespace NoteTakingAppWithLogin.Shared;

public class Tag
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string TagName { get; set; } = string.Empty;
}
