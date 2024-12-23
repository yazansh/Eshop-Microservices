namespace Ordering.Application.Dtos;
public record OrderItemDto(
    Guid ProductId,
    decimal Price,
    int Quantity);