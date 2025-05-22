using System.ComponentModel.DataAnnotations.Schema;

namespace FluentQuery.Tests.Mock;

public class UserRole
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}