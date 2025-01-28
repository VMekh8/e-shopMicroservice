using Carter;
using MediatR;

namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                return new DeleteProductResponse(result.Deleted);

            }).WithName("DeleteProduct")
        .WithDescription("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product");
    }
}