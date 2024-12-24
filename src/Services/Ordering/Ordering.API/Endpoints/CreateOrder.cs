namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto OrderDto);
public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async ([FromBody] CreateOrderRequest request, ISender sender, HttpContext httpContext) =>
        {
            var createOrderCommand = new CreateOrderCommand(request.OrderDto);

            var result = await sender.Send(createOrderCommand);

            var createOrderResponse = new CreateOrderResponse(result.Id);

            return Results.Created($"{httpContext.Request.Scheme}://{httpContext.Request.Host}/orders/{result.Id}", createOrderResponse);
        }).WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
