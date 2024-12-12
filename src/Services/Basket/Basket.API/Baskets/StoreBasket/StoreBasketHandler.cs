namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>;
public record StoreBasketCommandResult(ShoppingCart Cart);

// TODO: Add command validations

internal class StoreBasketHandler
    (IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketCommandResult(result);
    }
}
