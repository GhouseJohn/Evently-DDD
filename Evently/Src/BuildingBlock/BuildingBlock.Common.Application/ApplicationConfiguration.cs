using System.Reflection;
using BuildingBlock.Common.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Common.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
                                     Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));


        });
        return services;
    }
}
