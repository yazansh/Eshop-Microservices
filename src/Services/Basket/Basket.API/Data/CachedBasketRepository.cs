using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository basketRepository, IDistributedCache distributedCache)
    : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken)
    {
        var basket = await distributedCache.GetStringAsync(username, cancellationToken);
        if (basket is not null)
        {
            await distributedCache.RemoveAsync(username, cancellationToken);
            await basketRepository.DeleteBasketAsync(username, cancellationToken);

            return true;
        }

        await basketRepository.DeleteBasketAsync(username, cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken)
    {
        var basketJson = await distributedCache.GetStringAsync(username, cancellationToken);

        if (basketJson is not null)
            return JsonConvert.DeserializeObject<ShoppingCart>(basketJson)!;

        var basket = await basketRepository.GetBasketAsync(username, cancellationToken);
        await distributedCache.SetStringAsync(username, JsonConvert.SerializeObject(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        await distributedCache.SetStringAsync(cart.Username, JsonConvert.SerializeObject(cart),cancellationToken);

        await basketRepository.StoreBasketAsync(cart, cancellationToken);

        return cart;
    }
}
