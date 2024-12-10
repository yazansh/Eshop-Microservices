namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;
public record GetProductByIdQueryResult(Product Product);

internal class GetProductByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductByIdQueryHandler)}.Handle is being executed with {{@Query}}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product == null ? throw new ProductNotFoundException(query.Id) : new GetProductByIdQueryResult(product);
    }
}
