using BuildingBlock.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersRequest(PaginatedRequest GetOrdersPaginatedRequest);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrdersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] GetOrdersRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request.GetOrdersPaginatedRequest);

                var result = await sender.Send(query);

                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);

            }).WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all orders");
    }
}