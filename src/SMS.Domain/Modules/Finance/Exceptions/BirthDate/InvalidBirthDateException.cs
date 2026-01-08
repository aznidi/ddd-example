using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.BirthDate;

public sealed class InvalidBirthDateException : DomainException
{
    public InvalidBirthDateException ( string message ) : base ( message ) {}
}