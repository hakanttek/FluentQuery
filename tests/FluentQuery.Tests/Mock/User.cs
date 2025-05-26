using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentQuery.Tests.Mock;

public class User
{
    [Column("id")]
    public long Id { get; set; }

#if NET7_0_OR_GREATER
    [Required]
    [Column("full_name")]
    public required string FullName { get; set; }
#else
    [Required]
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;
#endif
}
