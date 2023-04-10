using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class AuditLogEntity : CoreAuditLogEntity
{
    public virtual UserEntity CreatedByUser { get; set; }
}

class AuditLogEntityConfiguration : CoreAuditLogEntityConfiguration<AuditLogEntity>
{
    public override void ConfigureOtherProperties(EntityTypeBuilder<AuditLogEntity> builder)
    {
        builder.HasOne(x => x.CreatedByUser)
            .WithMany(y => y.AuditLogs)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}