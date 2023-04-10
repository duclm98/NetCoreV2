using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Core.Database.Entities;

public class CoreAuditLogEntity
{
    public int AuditLogId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public AuditLogType Type { get; set; } = AuditLogType.Other;
    public string TableName { get; set; }
    public string PrimaryKey { get; set; }
    public string ColumnName { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
}

public enum AuditLogType
{
    [Display(Name = "Khác")] Other = 1,
    [Display(Name = "Xem")] Get,
    [Display(Name = "Thêm")] Create,
    [Display(Name = "Sửa")] Update,
    [Display(Name = "Xóa")] Delete
}

public abstract class CoreAuditLogEntityConfiguration<TEntityBase> : IEntityTypeConfiguration<TEntityBase>
    where TEntityBase : CoreAuditLogEntity
{
    public void Configure(EntityTypeBuilder<TEntityBase> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(x => x.AuditLogId);
        builder.Property(x => x.AuditLogId).ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.TableName).IsRequired();
        builder.Property(x => x.PrimaryKey).IsRequired();
        builder.Property(x => x.ColumnName).IsRequired(false);
        builder.Property(x => x.OldValues).IsRequired(false);
        builder.Property(x => x.NewValues).IsRequired(false);

        ConfigureOtherProperties(builder);
    }

    public abstract void ConfigureOtherProperties(EntityTypeBuilder<TEntityBase> builder);
}