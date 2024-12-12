namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken);
    Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken);
    Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken);
}
