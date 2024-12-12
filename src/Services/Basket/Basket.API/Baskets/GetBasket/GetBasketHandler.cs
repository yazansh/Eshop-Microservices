namespace Basket.API.Baskets.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketQueryResult>;
public record GetBasketQueryResult(ShoppingCart Cart);

internal class GetBasketHandler
    (IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasketAsync(query.Username, cancellationToken);

        return new GetBasketQueryResult(result);
    }
}
