using Basket.API.Exceptions;
using Basket.API.Models;
using BuildingBlock.Exceptions;
using Marten;

namespace Basket.API.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDocumentSession _session;

    public BasketRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
    {
        var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellation);
        return basket ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default)
    {
        _session.Store(basket);
        await _session.SaveChangesAsync(cancellation);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
    {
        _session.Delete<ShoppingCart>(userName);
        await _session.SaveChangesAsync(cancellation);
        return true;
    }
}