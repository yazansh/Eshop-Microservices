namespace Ordering.Application.Orders.Commands.UpdateOrder;
public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderCommandResult>;
public record UpdateOrderCommandResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {   
        RuleFor(x => x.OrderDto.Id).NotNull().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id is required");
        RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.OrderDto.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.OrderDto.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}