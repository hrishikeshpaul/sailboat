﻿namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

using Sb.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSbServicesFromAssembly(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromCallingAssembly()
                .AddClasses(@class => @class
                    .WithAttribute<SingletonServiceAttribute>())
                        .AsSelf()
                        .WithSingletonLifetime()
                .AddClasses(@class => @class
                    .WithAttribute<TransientServiceAttribute>())
                        .AsSelfWithInterfaces()
                        .WithTransientLifetime()
                .AddClasses(@class => @class
                    .WithAttribute<ScopedServiceAttribute>())
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime()
                .AddClasses(@class => @class
                    .WithAttribute<ConfigurationAttribute>())
                        .AsSelf()
                        .WithSingletonLifetime());
    }
}