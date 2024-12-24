namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public record GetOrdersByNameQuery(string OrderName) 
    : IQuery<GetOrdersByNameQueryResult>;

public record GetOrdersByNameQueryResult(IEnumerable<OrderDto> OrderDtos);