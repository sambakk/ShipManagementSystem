namespace ShipManagement.Infrastructure.Exceptions;

public class KeyNotFoundException : Exception
{
    public KeyNotFoundException(string message) : base(message) { }
}
