using Carter;
using MediatR;

namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id,UpdateProductCommand command, ISender sender) =>
            {
                if (id != command.Id)
                {
                    return Results.BadRequest("The product ID in the path and body do not match.");
                }
                var result = await sender.Send(command);
                return Results.Ok(result);

            }).WithName("UpdateProduct")
            .WithDescription("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status202Accepted)
            .ProducesProblem(StatusCodes.Status406NotAcceptable)
            .WithSummary("Update Product");
    }
}