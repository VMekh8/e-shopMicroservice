using BuildingBlock.CQRS.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameQueryHandler : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameQueryResult>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersByNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetOrdersByNameQueryResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(x => x.OrderItems)
            .Where(o => o.OrderName.Value.Contains(request.OrderName))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameQueryResult(orders.ToOrderDtoList());
    }
}