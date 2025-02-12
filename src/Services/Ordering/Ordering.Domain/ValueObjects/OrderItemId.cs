using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public class OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Order item Id can`t be null");
        }

        return new OrderItemId(value);
    }
}