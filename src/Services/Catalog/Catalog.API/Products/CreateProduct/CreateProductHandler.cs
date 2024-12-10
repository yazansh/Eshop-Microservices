namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price,List<string> Categories, string ImagePath) : ICommand<CreateProductCommandResult>;
public record CreateProductCommandResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
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

internal class CreateProductCommandHandler
    (IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
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
