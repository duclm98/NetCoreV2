using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAllDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x =>
                (x.FullName ?? "").Contains("Data")
                || (x.FullName ?? "").Contains("Core")
                || (x.FullName ?? "").Contains("Api")
            ).ToList();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(x =>
                    x.Name.EndsWith("Repository")
                    || x.Name.EndsWith("SubRepository")
                    || x.Name.EndsWith("Service")
                    || x.Name.EndsWith("SubService")
                    || x.Name.EndsWith("Helper")
                )
                .ToList();

            foreach (var type in types)
            {
                if (type.BaseType != null)
                {
                    var interfaceType = type.GetInterfaces().Except(type.BaseType.GetInterfaces()).FirstOrDefault();

                    if (interfaceType != null)
                    {
                        services.AddScoped(interfaceType, type);
                    }
                }
            }
        }

        return services;
    }
}