using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            var response = result.Adapt<GetProductResult>();
            return Results.Ok(response);
        }).WithName("GetProduct")
        .WithDescription("GetProduct")
        .Produces<GetProductQuery>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}