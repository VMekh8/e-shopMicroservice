﻿namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; } = Guid.Empty;

    private CustomerId(Guid value) => Value = value;

    public static CustomerId of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("Customer Id can`t be null");
        }

        return new CustomerId(value);
    }
}