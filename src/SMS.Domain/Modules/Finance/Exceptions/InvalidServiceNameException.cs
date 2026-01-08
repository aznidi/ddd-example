using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions;

public sealed class InvalidServiceNameException : DomainException
{
    public InvalidServiceNameException(string message) : base(message) { }
}
