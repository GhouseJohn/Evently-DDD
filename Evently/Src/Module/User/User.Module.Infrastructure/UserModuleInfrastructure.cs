using BuildingBlock.Common.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Module.Application;
using User.Module.Application.Repo;
using User.Module.Domain;
using User.Module.Infrastructure.Database;
using User.Module.Infrastructure.Event;
using User.Module.Infrastructure.User;
namespace User.Module.Infrastructure;
public static class UserModuleInfrastructure
{
    public static IServiceCollection AddEventsModule(this IServiceCollection services,
                                           IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);

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
                .UseSnakeCaseNamingConvention());


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDbContext>());
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepo, UserRepo>();
    }

}


