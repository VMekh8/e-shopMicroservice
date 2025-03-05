using BuildingBlock.CQRS.Queries;
using BuildingBlock.Pagination;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginatedRequest Request) : IQuery<GetOrdersQueryResult>;

public record GetOrdersQueryResult(PaginatedResult<OrderDto> Orders);