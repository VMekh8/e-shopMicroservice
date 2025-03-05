using BuildingBlock.CQRS.QueryHandlers;
using BuildingBlock.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.Request.PageSize;
        var pageIndex = request.Request.PageIndex;

        var totalOrdersCount = await _context.Orders.LongCountAsync(cancellationToken);

        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersQueryResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalOrdersCount,
                orders.ToOrderDtoList())
        );
    }
}