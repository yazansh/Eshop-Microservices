namespace Basket.API.Baskets.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketQueryResult>;
public record GetBasketQueryResult(ShoppingCart Cart);

internal class GetBasketHandler
    (IBasketRepository repository, Discount.Grpc.Discount.DiscountClient discountClient)
    : IQueryHandler<GetBasketQuery, GetBasketQueryResult>
{
    public async Task<GetBasketQueryResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasketAsync(query.Username, cancellationToken);

        foreach (var item in result.Items)
        {
            var discount = await discountClient.GetDiscountAsync(new Discount.Grpc.GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= discount.Amount;
        }

        return new GetBasketQueryResult(result);
    }
}
