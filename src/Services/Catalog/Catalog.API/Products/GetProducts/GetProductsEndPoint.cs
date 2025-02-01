using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? Page = 1, int? PageSize = 10);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters]GetProductsRequest request, ISender sender) => 
        { 
            var query = request.Adapt<GetProductQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductResult>();
            return Results.Ok(response);
        }).WithName("GetProduct")
        .WithDescription("GetProduct")
        .Produces<GetProductQuery>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}