using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BpmnEngine.Migrations;
using Microsoft.AspNetCore.Identity;

namespace BpmnEngine.Storage.Entities;

[Table(DatabaseConstants.UsersTable)]
public class UserEntity : IdentityUser<Guid>
{
    [Key] [Column("id")] public override Guid Id { get; set; }
    [Required] [Column("user_name")] public override string UserName { get; set; }
    [Required] [Column("password")] public override string PasswordHash { get; set; }
    [Required] [Column("position")] public string JobPosition { get; set; }

    [NotMapped] public override string Email { get; set; }
    [NotMapped] public override bool EmailConfirmed { get; set; }
    
    [NotMapped]
    public override string NormalizedUserName
    {
        get => UserName.ToLowerInvariant();
        set { }
    }

    [NotMapped] public override string NormalizedEmail { get; set; }
    [NotMapped] public override bool LockoutEnabled { get; set; }
    [NotMapped] public override int AccessFailedCount { get; set; }
    [NotMapped] public override string? PhoneNumber { get; set; }
    [NotMapped] public override string? ConcurrencyStamp { get; set; }
    [NotMapped] public override string? SecurityStamp { get; set; }
    [NotMapped] public override DateTimeOffset? LockoutEnd { get; set; }
    [NotMapped] public override bool TwoFactorEnabled { get; set; }
    [NotMapped] public override bool PhoneNumberConfirmed { get; set; }
}