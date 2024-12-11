namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsQueryResult>;
public record GetProductsQueryResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductsQueryHandler)}.Handle being executed with {{@Query}}", query);

        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsQueryResult(products);
    }
}
