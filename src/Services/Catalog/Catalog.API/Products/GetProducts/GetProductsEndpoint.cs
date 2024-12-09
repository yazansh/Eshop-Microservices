namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var productsQuery = new GetProductsQuery();

            var result = await sender.Send(productsQuery);

            var productsResponse = result.Adapt<GetProductsResponse>();

            return Results.Ok(productsResponse);
        }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Products")
        .WithSummary("Get Products");
    }
}
