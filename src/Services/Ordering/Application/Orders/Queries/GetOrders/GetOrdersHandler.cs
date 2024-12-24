namespace Ordering.Application.Orders.Queries.GetOrders;
public class GetOrdersQueryHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.Request.PageIndex;
        var pageSize = query.Request.PageSize;

        var orders = await dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageIndex)
            .Take(pageIndex)
            .ToListAsync(cancellationToken);

        var count = await dbContext.Orders.LongCountAsync(cancellationToken);

        return new GetOrdersQueryResult(
            new PaginatedResult<OrderDto>(orders.ToOrderDtoList(), pageIndex, pageIndex, count));
    }
}
