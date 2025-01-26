using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
