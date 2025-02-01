using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsDeleted);

public class DeleteBusketCommandEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string userName, ISender sender) =>
        {
            var result = sender.Send(new DeleteBasketCommand(userName));

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);

        }).WithName("DeleteBasket")
        .WithDescription("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete basket by username");
    }
}