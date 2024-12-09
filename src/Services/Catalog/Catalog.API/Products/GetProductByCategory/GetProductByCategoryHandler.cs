namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryQueryResult>;
public record GetProductByCategoryQueryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryQueryResult>
{
    public async Task<GetProductByCategoryQueryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
            .Where(p => p.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryQueryResult(products);
    }
}
