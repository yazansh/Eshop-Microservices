using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price,List<string> Categories, string ImagePath) : ICommand<CreateProductCommandResponse>;
public record CreateProductCommandResponse(Guid Id);

public class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle is executing with Command: {@Command}", command);

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

        return new CreateProductCommandResponse(productEntity.Id);
    }
}
