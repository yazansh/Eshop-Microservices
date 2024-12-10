namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Categories, string ImagePath)
    : ICommand<UpdateProductCommandResult>;
public record UpdateProductCommandResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid Guid!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(5, 1000).WithMessage("Description must be between 5 and 1000 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.Categories)
            .NotEmpty().WithMessage("Categories are required.")
            .Must(categories => categories.All(category => !string.IsNullOrEmpty(category)))
            .WithMessage("Each category must be a non-empty string.");

        RuleFor(x => x.ImagePath)
            .NotEmpty().WithMessage("ImagePath is required.")
            .Must(imagePath => Uri.IsWellFormedUriString(imagePath, UriKind.RelativeOrAbsolute))
            .WithMessage("ImagePath must be a valid URI.");
    }
}

internal class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(UpdateProductCommandHandler)}.Handle is called with {{@Command}}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Categories = command.Categories;
        product.ImagePath = command.ImagePath;
            
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(true);
    }
}
