using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentQuery.Tests.Mock;

[Table("user")]
public class User
{
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("full_name")]
    public required string FullName { get; set; }
}
