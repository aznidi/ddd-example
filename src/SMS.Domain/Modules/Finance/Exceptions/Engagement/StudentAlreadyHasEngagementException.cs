using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Engagement;

public sealed class StudentAlreadyHasEngagementException : DomainException
{
    public StudentAlreadyHasEngagementException(string message) : base(message)
    {
    }
}
