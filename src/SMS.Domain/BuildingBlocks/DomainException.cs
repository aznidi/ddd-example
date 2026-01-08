namespace SMS.Domain.BuildingBlocks;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}
