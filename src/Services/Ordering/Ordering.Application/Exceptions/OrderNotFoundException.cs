using BuildingBlock.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base($"Order with id: {id} not found", id)
    {
    }
}