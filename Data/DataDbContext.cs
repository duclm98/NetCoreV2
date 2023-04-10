using Core.Database;
using Data.Entities;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataDbContext : CoreDbContext<AuditLogEntity>
{
    public DataDbContext(DbContextOptions<CoreDbContext<AuditLogEntity>> options) : base(options)
    {
    }

    public override void ConfigureModelCreating(ModelBuilder modelBuilder)
    {
        // Configure using Fluent API
        modelBuilder.ApplyConfiguration(new AuditLogEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        // Data seeding
        modelBuilder.Seed();
    }

    public DbSet<UserEntity> Users { set; get; }
}