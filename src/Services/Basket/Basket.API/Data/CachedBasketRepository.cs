using System.Text.Json;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository : IBasketRepository
{
    private readonly IBasketRepository _repository;
    private readonly IDistributedCache _cache;

    public CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
    {
        var cachedBasket = await _cache.GetStringAsync(userName, cancellation);

        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }

        var basket = await _repository.GetBasket(userName, cancellation);
        await _cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellation);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation = default)
    {
        await _repository.StoreBasket(basket, cancellation);

        await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellation);
        
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation = default)
    {
        await _repository.DeleteBasket(userName, cancellation);

        await _cache.RemoveAsync(userName, cancellation);

        return true;
    }
}