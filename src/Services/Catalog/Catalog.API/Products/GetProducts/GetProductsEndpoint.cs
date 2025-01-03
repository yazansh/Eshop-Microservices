﻿namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber, int? PageSize);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var productsQuery = request.Adapt<GetProductsQuery>();

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
