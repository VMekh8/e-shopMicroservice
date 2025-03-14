using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameRequest(string OrderName);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByNameEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (GetOrdersByNameRequest request, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(request.OrderName);

                var result = await sender.Send(query);

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);

            }).WithName("Get Orders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders");
    }
}