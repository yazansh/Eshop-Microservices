namespace Ordering.API.Endpoints;

//public record DeleteOrderRequest(Guid Id);
public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
        {
            var deleteOrderCommand = new DeleteOrderCommand(orderId);

            var result = await sender.Send(deleteOrderCommand);

            return Results.Ok(result.Adapt<DeleteOrderResponse>());

        }).WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Delete Order")
        .WithSummary("Delete Order");
    }
}
