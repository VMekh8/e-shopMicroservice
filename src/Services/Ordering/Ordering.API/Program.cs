using Ordering.Application;
using Ordering.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddWebServices();

var app = builder.Build();

app.UseWebServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDataBaseAsync();
}

app.Run();
