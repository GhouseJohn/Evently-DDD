using BuildingBlock.Common.Application.Messaging;
using BuildingBlock.Common.InfraStructure.outbox;
using BuildingBlock.Common.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using User.Module.Application;
using User.Module.Domain;
using User.Module.Infrastructure.Database;
using User.Module.Infrastructure.outbox;
using User.Module.Infrastructure.User;
namespace User.Module.Infrastructure;
public static class UserModuleInfrastructure
{
    public static IServiceCollection AddEventsModule(this IServiceCollection services,
                                           IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);
        services.AddDomainEventHandlers();

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<UserDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
        services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));

    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }



}


