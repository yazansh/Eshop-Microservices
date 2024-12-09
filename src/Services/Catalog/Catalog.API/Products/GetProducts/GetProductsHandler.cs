using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsQueryResponse>;

public record GetProductsQueryResponse(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductsQueryHandler)}.Handle being executed with {{@Query}}", query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsQueryResponse(products);
    }
}
