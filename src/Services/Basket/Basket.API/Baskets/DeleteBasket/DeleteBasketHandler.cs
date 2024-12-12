namespace Basket.API.Baskets.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketCommandResult>;
public record DeleteBasketCommandResult(bool IsSuccess);

// TODO: add command validation 

internal class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        // TODO: call the repository delete method

        return Task.FromResult(new DeleteBasketCommandResult(true));
    }
}
