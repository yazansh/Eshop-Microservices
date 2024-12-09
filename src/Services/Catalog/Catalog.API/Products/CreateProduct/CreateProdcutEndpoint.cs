namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, string Description, decimal Price, List<string> Categories, string ImagePath);
public record CreateProductResponse(Guid Id);

public class CreateProdcutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var createProductCommand = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(createProductCommand);

            var createProductResponse = result.Adapt<CreateProductResponse>();

            return Results.Created($"getProduct/{result.Id}", createProductResponse);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Create Product")
            .WithSummary("Create Product");
    }
}
