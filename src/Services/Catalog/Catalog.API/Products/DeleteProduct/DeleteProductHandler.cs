
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;
public record DeleteProductCommandResult(bool IsSuccess);

internal class DeleteProductCommandHandler
    (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DeleteProductCommandHandler)}.Handle is called with {{@Command}}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException();

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductCommandResult(true);
    }
}
