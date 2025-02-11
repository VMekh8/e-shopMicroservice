using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddWebServices();

var app = builder.Build();

app.UseWebServices();

app.Run();
