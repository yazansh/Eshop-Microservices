namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellationToken)
    {
        var basket = await session.Query<ShoppingCart>().Where(sc => sc.Username.Equals(username))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new BasketNotFoundException(username);

        session.Delete(basket);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellationToken)
    {
        var basket = await session.Query<ShoppingCart>()
            .Where(sc => sc.Username.Equals(username))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new BasketNotFoundException(username);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);

        return cart;
    }
}
