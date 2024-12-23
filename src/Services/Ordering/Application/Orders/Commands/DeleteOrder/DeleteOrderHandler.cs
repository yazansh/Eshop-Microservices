namespace Ordering.Application.Orders.Commands.DeleteOrder;
public class DeleteOrderCommandHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
{
    public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.Id)], cancellationToken)
            ?? throw new OrderNotFoundException(command.Id);

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderCommandResult(true);
    }
}
