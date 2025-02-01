using Basket.API.Data;
using Basket.API.Models;
using BuildingBlock.CQRS.Queries;
using BuildingBlock.CQRS.QueryHandlers;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    private readonly IBasketRepository _repository;

    public StoreBasketCommandHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await _repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}