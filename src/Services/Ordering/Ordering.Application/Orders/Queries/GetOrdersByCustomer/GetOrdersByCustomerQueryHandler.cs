using BuildingBlock.CQRS.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomersQueryResult>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersByCustomerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetOrdersByCustomersQueryResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomersQueryResult(orders.ToOrderDtoList());
    }
}