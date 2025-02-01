using Basket.API.Data;
using Basket.API.Models;
using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketQueryResult>;

public record GetBasketQueryResult(ShoppingCart Cart);

internal sealed class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    private readonly IBasketRepository _repository;

    public GetBasketQueryHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetBasketQueryResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetBasket(request.UserName, cancellationToken);

        return new GetBasketQueryResult(result);
    }
}