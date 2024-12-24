namespace Ordering.Application.Orders.Queries.GetOrders;
public record GetOrdersQuery(PaginationRequest Request) : IQuery<GetOrdersQueryResult>;
public record GetOrdersQueryResult(PaginatedResult<OrderDto> OrdersDto);