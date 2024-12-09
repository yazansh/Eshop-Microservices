namespace Catalog.API.Products;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var productsQuery = new GetProductsQuery();

            var result = await sender.Send(productsQuery);

            var products = result.Adapt<GetProductsResult>();

            return Results.Ok(products);
        }).WithName("GetProducts")
        .Produces<GetProductsResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Products")
        .WithSummary("Get Products");
    }
}
