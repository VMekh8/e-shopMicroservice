using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductById;

public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (GetProductByIdQuery query,
            ISender sender) =>
            {
                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);

            }).WithName("GetProductById")
        .WithDescription("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id");
    }
}