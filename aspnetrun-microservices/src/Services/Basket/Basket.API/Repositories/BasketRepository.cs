#nullable enable
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using static Basket.API.JsonHelpers;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private IDistributedCache RedisCache { get; }

    public BasketRepository(IDistributedCache redis)
    {
        RedisCache = redis ?? throw new ArgumentNullException(nameof(redis));
    }
    public async Task<ShoppingCart?> GetBasket(string username)
    {
       var basket=  await RedisCache.GetStringAsync(username);
       return basket == null ? null : Deserialze(basket);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart cart)
    {
        await RedisCache.SetStringAsync(cart.Username, Serialze(cart));
        return await GetBasket(cart.Username);
    }

    public async Task<bool> DeleteBasket(string username)
    {
        await RedisCache.RemoveAsync(username);
        var shoppingItem = await GetBasket(username);
        return shoppingItem == null;
    }
    
    
}