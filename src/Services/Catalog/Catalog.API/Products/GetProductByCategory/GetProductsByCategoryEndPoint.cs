using Carter;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public class GetProductsByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
           var result = await sender.Send(new GetProductByCategoryQuery(category));
           return Results.Ok(result);

        }).WithName("GetProductsByCategory")
        .WithDescription("GetProductsByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products By Category");
    }
}