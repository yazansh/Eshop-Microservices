using static Discount.Grpc.Discount;

namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>;
public record StoreBasketCommandResult(ShoppingCart Cart);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(c => c.Cart).NotNull()
            .WithMessage("Cart must not be null!");

        RuleFor(c => c.Cart.Username).NotNull()
            .NotEmpty().WithMessage("Cart username must not be empty!")
            .MaximumLength(20).WithMessage("Cart username must not exceed 20 characters");

        RuleFor(c => c.Cart.TotalPrice)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.Cart.Items)
            .NotNull().WithMessage("Items must not be null")
            .NotEmpty().WithMessage("Items must not be empty");

        RuleForEach(c => c.Cart.Items).SetValidator(new CartShoppingItemValidator());
    }
}

public class CartShoppingItemValidator : AbstractValidator<ShoppingCartItem>
{
    public CartShoppingItemValidator()
    {
        RuleFor(i => i.Color)
            .MaximumLength(10).WithMessage("Color must not exceed 10 characters");

        RuleFor(i => i.Price)
            .NotNull()
            .GreaterThan(0);
        RuleFor(i => i.Quantity)
            .GreaterThan(0);

        RuleFor(i => i.ProductId)
            .NotEqual(Guid.Empty);

        RuleFor(i => i.ProductName)
            .NotNull()
            .NotEmpty();
    }
}

internal class StoreBasketHandler
    (IBasketRepository repository, Discount.Grpc.Discount.DiscountClient discountClient)
    : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.StoreBasketAsync(command.Cart, cancellationToken);

        foreach (var item in result.Items)
        {
            var discount = await discountClient.GetDiscountAsync(new Discount.Grpc.GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= discount.Amount;
        }

        return new StoreBasketCommandResult(result);
    }
}
