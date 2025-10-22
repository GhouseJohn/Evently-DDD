using System.Reflection;
using API.Evently.Extensions;
using BuildingBlock.Common.Application;
using BuildingBlock.Common.InfraStructure;
using BuildingBlock.Common.Presentation.Endpoints;
using User.Module.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

Assembly[] moduleApplicationAssemblies = [
    User.Module.Application.AssemblyReference.Assembly
    ];
builder.Services.AddApplication(moduleApplicationAssemblies);
#region Connectionstring
string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
builder.Services.AddInfrastructure(databaseConnectionString, redisConnectionString);

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
#pragma warning disable S125 // Sections of code should not be commented out
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
#pragma warning restore S125 // Sections of code should not be commented out


app.MapControllers();
app.MapEndpoints();
app.Run();
