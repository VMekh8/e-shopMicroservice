using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public class ProductId
{
    public Guid Value { get; } = Guid.Empty;
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Product id can`t be null");
        }

        return new ProductId(id);
    }
}