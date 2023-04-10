using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserEntity : BaseEntity
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public UserRole Role { get; set; }

    public virtual ICollection<AuditLogEntity> AuditLogs { get; set; }
    public virtual ICollection<UserEntity> CreatedItems { get; set; }
    public virtual ICollection<UserEntity> UpdatedItems { get; set; }
    public virtual ICollection<UserEntity> DeletedItems { get; set; }
}

public enum UserRole
{
    [Display(Name = "Quản lý")] Manager = 1,
    [Display(Name = "Nhân viên")] Employee
}

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.RefreshToken).IsRequired(false);
        builder.Property(x => x.Role).IsRequired().HasDefaultValue(UserRole.Employee);
    }
}