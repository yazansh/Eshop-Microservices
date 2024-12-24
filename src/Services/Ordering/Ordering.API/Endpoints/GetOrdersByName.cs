namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> OrderDtos);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var query = new GetOrdersByNameQuery(orderName);

            var result = await sender.Send(query);

            return Results.Ok(result.Adapt<GetOrdersByNameResponse>());

        }).WithName("GetOrderByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Order By Name")
        .WithSummary("Get Order By Name");
    }
}
