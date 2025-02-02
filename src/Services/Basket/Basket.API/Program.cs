using Basket.API.Data;
using Basket.API.Models;
using BuildingBlock.BehaviorPipeline;
using BuildingBlock.Exceptions.Handlers;
using Carter;
using FluentValidation;
using Marten;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.MapCarter();

app.Run();
