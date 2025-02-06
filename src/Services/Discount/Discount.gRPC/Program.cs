using Discount.gRPC.Data;
using Discount.gRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DiscountContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("Database")!);
});

builder.Services.AddGrpc();

var app = builder.Build();

app.UseMigration();

app.MapGrpcService<DiscountService>();  
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
