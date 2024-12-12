namespace Basket.API.Baskets.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketCommandResult>;
public record DeleteBasketCommandResult(bool IsSuccess);

// TODO: add command validation 

internal class DeleteBasketHandler 
    (IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public async Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasketAsync(command.Username, cancellationToken);

        return new DeleteBasketCommandResult(result);
    }
}
