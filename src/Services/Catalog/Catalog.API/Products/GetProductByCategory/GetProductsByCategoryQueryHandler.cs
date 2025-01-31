using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : 
    IQuery<GetProductByCategoryResponse>;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

internal sealed class GetProductsByCategoryQueryHandler : 
    IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{
    private readonly IQuerySession _session;

    public GetProductsByCategoryQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResponse(products);
    }
}