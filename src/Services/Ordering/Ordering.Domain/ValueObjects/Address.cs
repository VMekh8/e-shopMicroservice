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

    private Address(
        string firstName, 
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string state,
        string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string state,
        string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
    }
}