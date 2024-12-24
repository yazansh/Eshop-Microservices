namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public class GetOrdersByCustomerQueryHandler 
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerQueryResult>
{
    public async Task<GetOrdersByCustomerQueryResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId.Value.Equals(request.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerQueryResult(orders.ToOrderDtoList());
    }
}
