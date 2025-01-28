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
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IQuerySession session, ILogger<GetProductsQueryHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductQuery.Handler called with query: {query}", query);

        var products = await _session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}