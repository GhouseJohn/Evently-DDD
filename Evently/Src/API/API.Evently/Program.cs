using System.Reflection;
using API.Evently.Extensions;
using API.Evently.MiddleWare;
using API.Evently.OpenTelemetry;
using BuildingBlock.Common.Application;
using BuildingBlock.Common.InfraStructure;
using BuildingBlock.Common.Presentation.Endpoints;
using Evently.Common.Infrastructure.Configuration;
using Evently.Common.Infrastructure.EventBus;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using User.Module.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Host.UseSerilog((context, logfile) =>
                logfile.ReadFrom.Configuration(context.Configuration));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


Assembly[] moduleApplicationAssemblies = [
    User.Module.Application.AssemblyReference.Assembly
    ];
builder.Services.AddApplication(moduleApplicationAssemblies);

#region Connectionstring
string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionStringOrThrow("Queue"));
builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [
    ],
    rabbitMqSettings,
    databaseConnectionString,
    redisConnectionString);

builder.Services.AddHealthChecks().AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);
#endregion
builder.Configuration.AddModuleConfiguration(["users", "events", "ticketing", "attendance"]);
builder.Services.AddEventsModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

WebApplication app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapEndpoints();
app.Run();

internal sealed partial class Program;
