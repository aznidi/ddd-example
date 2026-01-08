using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Common.Abstractions.Persistence;

public interface IBillableServiceRepository
{
    Task<BillableService?> GetByIdAsync(ServiceId id, CancellationToken ct = default);
    Task<CreateServiceDto> AddAsync(BillableService service, CancellationToken ct = default);
    Task<List<GetServicesDto>> GetServicesAsync();
}
