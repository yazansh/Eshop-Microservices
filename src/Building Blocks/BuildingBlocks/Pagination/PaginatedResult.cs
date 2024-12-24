namespace BuildingBlocks.Pagination;
public record PaginatedResult<TEntity>(IEnumerable<TEntity> Data, int PageIndex, int PageSize, long Count)
    where TEntity : class;