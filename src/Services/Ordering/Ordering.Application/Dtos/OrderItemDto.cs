namespace Ordering.Application.Dtos;

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    decimal Price,
    int Quantity);