namespace BuildingBlock.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {}

    public NotFoundException(string name, object key) :
        base($"Entity {name}, with key {key} was not found")
    {}
        
}