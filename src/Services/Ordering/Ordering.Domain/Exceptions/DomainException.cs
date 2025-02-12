namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) :
        base($"Domain exception with message: {message} throws from Domain layer")
    {
    }
}