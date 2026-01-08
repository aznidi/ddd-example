using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Engagement
{
    public sealed class DuplicateEngagementServiceException : DomainException
    {
        public DuplicateEngagementServiceException(string message) : base(message) { }
    }
}