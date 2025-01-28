using Carter;
using MediatR;

namespace Catalog.API.Products.GetProductById;

public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id,
            ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                return Results.Ok(result);

            }).WithName("GetProductById")
        .WithDescription("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id");
    }
}