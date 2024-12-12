namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>;
public record StoreBasketCommandResult(ShoppingCart Cart);

// TODO: Add command validations

internal class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;
        // TODO: call the repository
        return Task.FromResult(new StoreBasketCommandResult(cart));
    }
}
