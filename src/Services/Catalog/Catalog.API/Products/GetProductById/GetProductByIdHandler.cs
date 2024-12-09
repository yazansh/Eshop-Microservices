namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResponse>;
public record GetProductByIdQueryResponse(Product Product);

internal class GetProductByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductByIdQueryHandler)}.Handle is being executed with {{@Query}}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product == null ? throw new ProductNotFoundException() : new GetProductByIdQueryResponse(product);
    }
}
