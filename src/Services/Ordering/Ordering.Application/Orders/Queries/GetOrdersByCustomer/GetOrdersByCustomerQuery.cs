using BuildingBlock.CQRS.Queries;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomersQueryResult>;

public record GetOrdersByCustomersQueryResult(IEnumerable<OrderDto> Orders);