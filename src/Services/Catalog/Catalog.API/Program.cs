var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.Run();
