using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions;

public sealed class InvalidCurrencyException : DomainException
{
    public InvalidCurrencyException(string message) : base(message) { }
}
