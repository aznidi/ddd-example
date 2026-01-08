using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.DTOs.BillableServices;

public sealed class CreateServiceDto
{
    public ServiceId ServiceId { get; }

    public CreateServiceDto(ServiceId serviceId)
    {
        ServiceId = serviceId;
    }
}