using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price,List<string> Categories, string ImagePath) : ICommand<CreateProductCommandResponse>;
public record CreateProductCommandResponse(Guid Id);

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Categories = request.Categories,
            ImagePath = request.ImagePath
        };

        return new CreateProductCommandResponse(Guid.NewGuid());
    }
}
