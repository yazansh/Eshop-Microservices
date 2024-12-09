namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);

            var result = await sender.Send(query);

            var product = result.Adapt<GetProductByIdResult>();

            return Results.Ok(product);

        }).WithName("GetProductById")
        .Produces<GetProductByIdResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Product By Id")
        .WithSummary("Get Product By Id");
    }
}
