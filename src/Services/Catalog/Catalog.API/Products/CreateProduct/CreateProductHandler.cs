namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price,List<string> Categories, string ImagePath) : ICommand<CreateProductCommandResult>;
public record CreateProductCommandResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(CreateProductCommandHandler)}.Handle is executing with Command: {{@Command}}", command);

        var productEntity = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Categories = command.Categories,
            ImagePath = command.ImagePath
        };

        session.Store(productEntity);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResult(productEntity.Id);
    }
}
