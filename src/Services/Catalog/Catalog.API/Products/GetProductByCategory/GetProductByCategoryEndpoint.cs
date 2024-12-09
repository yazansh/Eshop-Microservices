namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/categories/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category);

            var result = await sender.Send(query);

            var productResult = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(productResult);

        }).WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Product By Category")
        .WithSummary("Get Product By Category");
    }
}
