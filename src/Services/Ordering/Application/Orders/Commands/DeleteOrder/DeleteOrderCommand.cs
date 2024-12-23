namespace Ordering.Application.Orders.Commands.DeleteOrder;
public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderCommandResult>;
public record DeleteOrderCommandResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required")
            .NotEqual(Guid.Empty).WithMessage("Id is required");
    }
}