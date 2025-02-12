using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; } = Guid.Empty;
    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Order id can`t be bull");
        }

        return new OrderId(value);
    } 
}