using BuildingBlock.Common.Application.Clock;
using BuildingBlock.Common.Application.Data;
using BuildingBlock.Common.InfraStructure.Clock;
using BuildingBlock.Common.InfraStructure.Date;
using BuildingBlock.Common.InfraStructure.outbox;
using Evently.Common.Application.EventBus;
using Evently.Common.Infrastructure.EventBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Quartz;
using StackExchange.Redis;


namespace BuildingBlock.Common.InfraStructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
       string serviceName, Action<IRegistrationConfigurator,
            string>[] moduleConfigureConsumers,
         RabbitMqSettings rabbitMqSettings,
                           string databaseConnectionString, string redisConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddMassTransit(configure =>
        {
            string instanceId = serviceName.ToLowerInvariant().Replace('.', '-');
            foreach (Action<IRegistrationConfigurator, string> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure, instanceId);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqSettings.Host), h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });



        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
              options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));

        }
        catch (Exception ex)
        {
            Console.WriteLine("Redis connection failed: " + ex.Message);
            services.AddDistributedMemoryCache();
        }

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();
        services.TryAddSingleton<IEventBus, EventBus>();

        return services;
    }
}

