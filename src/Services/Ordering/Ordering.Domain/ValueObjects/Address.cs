namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = string.Empty;

    public string LastName { get; } = string.Empty;

    public string? EmailAddress { get; } = null;

    public string AddressLine { get; } = string.Empty;

    public string Country { get; } = string.Empty;

    public string State { get; } = string.Empty;
    
    public string ZipCode { get; } = string.Empty;
}