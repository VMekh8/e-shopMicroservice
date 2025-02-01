using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery(int? Page = 1, int? PageSize = 10) : IQuery<GetProductResult>;

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
        var products = await _session.Query<Product>()
            .ToPagedListAsync(query.Page ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductResult(products);
    }
}