using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Entities;

public class CoreBaseEntity
{
    public int? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

public abstract class CoreBaseEntityConfiguration<TEntityBase> : IEntityTypeConfiguration<TEntityBase>
    where TEntityBase : CoreBaseEntity
{
    public void Configure(EntityTypeBuilder<TEntityBase> builder)
    {
        builder.Property(x => x.CreatedBy).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(x => x.UpdatedBy).IsRequired(false);
        builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(x => x.DeletedBy).IsRequired(false);
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        ConfigureOtherProperties(builder);
    }

    public abstract void ConfigureOtherProperties(EntityTypeBuilder<TEntityBase> builder);
}