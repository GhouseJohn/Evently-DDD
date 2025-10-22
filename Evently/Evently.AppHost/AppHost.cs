var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.API_Evently>("api-evently");

builder.Build().Run();
