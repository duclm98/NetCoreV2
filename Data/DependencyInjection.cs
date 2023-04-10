using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDataDbContext(
        this IServiceCollection services,
        string connectionString,
        string migrationsAssembly = null)
    {
        services.AddDbContext<DataDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                b =>
                {
                    b.CommandTimeout(1200);
                    if (migrationsAssembly != null)
                        b.MigrationsAssembly(migrationsAssembly);
                }
            );
            options.ConfigureWarnings(config =>
            {
                config.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning);
                config.Ignore(RelationalEventId.BoolWithDefaultWarning);
            });
        }, ServiceLifetime.Transient);

        services.AddTransient<UnitOfWork>();

        return services;
    }
}