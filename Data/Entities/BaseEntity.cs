using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public class BaseEntity : CoreBaseEntity
{
    public virtual UserEntity CreatedByUser { get; set; }
    public virtual UserEntity UpdatedByUser { get; set; }
    public virtual UserEntity DeletedByUser { get; set; }
}

public class BaseConfiguration : CoreBaseEntityConfiguration<BaseEntity>
{
    public override void ConfigureOtherProperties(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasOne(x => x.CreatedByUser)
            .WithMany(y => y.CreatedItems)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasOne(x => x.UpdatedByUser)
            .WithMany(y => y.UpdatedItems)
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder.HasOne(x => x.DeletedByUser)
            .WithMany(y => y.DeletedItems)
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}