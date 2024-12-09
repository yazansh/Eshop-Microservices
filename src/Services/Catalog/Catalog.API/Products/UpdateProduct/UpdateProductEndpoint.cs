namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(string Name, string Description, decimal Price, List<string> Categories, string ImagePath);
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (Guid id, UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var updateCommand = command with { Id = id };

            var result = await sender.Send(updateCommand);

            var updateProductResponse = result.Adapt<UpdateProductResponse>();

            return Results.Ok(updateProductResponse);
        }).WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Update Product")
        .WithSummary("Update Product");
    }
}
