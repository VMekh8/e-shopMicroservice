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
    private readonly ILogger<GetProductsByCategoryQueryHandler> _logger;

    public GetProductsByCategoryQueryHandler(ILogger<GetProductsByCategoryQueryHandler> logger, IQuerySession session)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductByCategoryQuery.Handler called by query: {query}", query);

        var products = await _session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResponse(products);
    }
}