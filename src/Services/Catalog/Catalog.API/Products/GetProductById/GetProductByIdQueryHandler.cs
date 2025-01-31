using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;

public record GetProductByIdResponse(Product Product);

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    private readonly IQuerySession _session;

    public GetProductByIdQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResponse(product);
    }
}