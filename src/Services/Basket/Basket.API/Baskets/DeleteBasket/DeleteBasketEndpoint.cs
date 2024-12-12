namespace Basket.API.Baskets.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{username}", async (string username, ISender sender) =>
        {
            var command = new DeleteBasketCommand(username);

            var result = await sender.Send(command);

            return Results.Ok(result.Adapt<DeleteBasketResponse>());
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Delete Basket")
        .WithSummary("Delete Basket");
    }
}
