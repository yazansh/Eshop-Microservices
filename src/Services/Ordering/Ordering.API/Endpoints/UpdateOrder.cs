namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto OrderDto);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{orderId}", async (Guid orderId, [FromBody] UpdateOrderRequest request, ISender sender) =>
        {
            var updateOrderCommand = request.Adapt<UpdateOrderCommand>();
            //var orderDto = updateOrderCommand.OrderDto with { Id = orderId };
            var command = updateOrderCommand with { OrderDto = updateOrderCommand.OrderDto with { Id = orderId } };

            var result = await sender.Send(command);

            return Results.Ok(new UpdateOrderResponse(result.IsSuccess));

        }).WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Update Order")
        .WithSummary("Update Order");
    }
}
