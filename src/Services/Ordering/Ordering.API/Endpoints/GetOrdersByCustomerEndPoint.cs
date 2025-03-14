using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerRequest(Guid CustomerId);

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomerEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (GetOrdersByCustomerRequest request, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(request.CustomerId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);

            }).WithName("Get Orders By Customer")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customers Id");
    }
}