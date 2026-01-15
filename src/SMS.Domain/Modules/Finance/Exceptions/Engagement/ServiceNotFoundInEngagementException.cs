using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Engagement;

public sealed class ServiceNotFoundInEngagementException : DomainException
{
    public ServiceNotFoundInEngagementException(string message) : base(message)
    {
    }
}
