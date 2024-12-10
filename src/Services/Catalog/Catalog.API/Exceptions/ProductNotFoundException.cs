namespace Catalog.API.Exceptions;

public class ProductNotFoundException(Guid id) : NotFoundException("product", id)
{
}
