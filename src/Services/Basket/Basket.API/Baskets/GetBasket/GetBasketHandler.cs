namespace Basket.API.Baskets.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketQueryResult>;
public record GetBasketQueryResult(ShoppingCart Cart);

public class GetBasketHandler
    (IDocumentSession session)
    : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        //// TODO: Create an abstraction using repository pattern to have DB Repo and Redis repo
        //var basket = await session.Query<ShoppingCart>()
        //    .Where(sc => sc.Username.Equals(request.Username))
        //    .FirstOrDefaultAsync(cancellationToken);

        //return  basket is null ? throw new BasketNotFoundException(request.Username) : new GetBasketQueryResult(basket);

        return new GetBasketQueryResult(new ShoppingCart("test"));
    }
}
