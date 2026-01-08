using SMS.Domain.BuildingBlocks;

namespace SMS.Application.Common.Exceptions.Services;

public sealed class ServiceNotFoundException : DomainException
{
    public ServiceNotFoundException(string message) : base(message)
    {
    }
}