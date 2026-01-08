using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Engagement;

public sealed class FailedToGenerateTranches : DomainException
{
    public FailedToGenerateTranches ( string message ) : base( message ) { }
}