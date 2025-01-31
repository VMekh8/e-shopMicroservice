using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>
{
    private readonly IQuerySession _session;

    public GetProductsQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}