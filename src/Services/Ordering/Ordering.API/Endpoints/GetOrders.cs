namespace Ordering.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Result);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (ISender sender, [FromQuery] int pageIndex = 0,[FromQuery] int pageSize = 10) =>
        {
            var getOrdersQuery = new GetOrdersQuery(new PaginationRequest(pageIndex, pageSize));

            var result = await sender.Send(getOrdersQuery);

            return Results.Ok(result.Adapt<GetOrdersResponse>());

        }).WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get Orders")
        .WithSummary("Get Orders");
    }
}
