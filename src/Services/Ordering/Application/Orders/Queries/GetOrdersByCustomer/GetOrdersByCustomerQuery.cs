namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public record GetOrdersByCustomerQuery(Guid CustomerId) 
    : IQuery<GetOrdersByCustomerQueryResult>;

public record GetOrdersByCustomerQueryResult(IEnumerable<OrderDto> Result);