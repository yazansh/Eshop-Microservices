namespace Basket.API.Baskets.CheckoutBasket;
public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
    : ICommand<CheckoutBasketCommandResult>;
public record CheckoutBasketCommandResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(c => c.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto Dto is required");
        RuleFor(c => c.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username is required");
    }
}

public class CheckoutBasketCommandHandler
    (IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketCommandResult>
{
    public async Task<CheckoutBasketCommandResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        //1 get basket 
        var basket = await basketRepository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken)
            ?? throw new BasketNotFoundException(command.BasketCheckoutDto.UserName);

        //2 create BasketCheckoutEvent
        var basketCheckoutEvent = GenerateBasketCheckoutEvent(command.BasketCheckoutDto);
        basketCheckoutEvent.TotalPrice = basket.TotalPrice;

        //3 publish event
        await publishEndpoint.Publish(basketCheckoutEvent, cancellationToken);

        //4 delete basket
        await basketRepository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketCommandResult(true);
    }

    private static BasketCheckoutEvent GenerateBasketCheckoutEvent(BasketCheckoutDto basket)
    {
        var billingAddress = basket.BillingAddress;
        var billingAddressDto = new AddressDto(billingAddress.FirstName, billingAddress.LastName, billingAddress.EmailAddress,
            billingAddress.AddressLine, billingAddress.Country, billingAddress.State, billingAddress.ZipCode);

        var shippingAddress = basket.ShippingAddress;
        var shippingAddressDto = new AddressDto(shippingAddress.FirstName, shippingAddress.LastName, shippingAddress.EmailAddress,
            shippingAddress.AddressLine, shippingAddress.Country, shippingAddress.State, shippingAddress.ZipCode);

        var payment = basket.Payment;
        var paymentDto = new PaymentDto(payment.CardName, payment.CardNumber, payment.Expiration, payment.CVV, payment.PaymentMethod);

        return new BasketCheckoutEvent()
        {
            OrderName = basket.UserName,
            CustomerId = basket.CustomerId,
            BillingAddress = billingAddressDto,
            ShippingAddress = shippingAddressDto,
            Payment = paymentDto,
            OrderItems = basket.OrderItems
            .Select(i => new OrderItemDto(Guid.NewGuid(), i.ProductId, i.Price, i.Quantity)).ToList()
        };
    }
}
