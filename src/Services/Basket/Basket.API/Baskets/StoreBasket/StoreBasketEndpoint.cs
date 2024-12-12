namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(ShoppingCart Cart);


public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (StoreBasketRequest request, ISender sender, HttpContext httpContext) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            return Results.Created($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/baskets/{request.Cart.Username}", result.Adapt<StoreBasketResponse>());
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Store Basket")
        .WithSummary("Store Basket (Create Basket, Add Item(s), Remove Item(s), Update Quantity");
    }
}
