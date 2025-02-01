using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketQueryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));

            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);

        }).WithName("GetBasket")
        .WithDescription("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket by username");
    }
}