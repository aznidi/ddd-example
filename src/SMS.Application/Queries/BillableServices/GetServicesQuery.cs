using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.BillableServices;

namespace SMS.Application.Queries.BillableServices;

public sealed class GetServicesQuery
{
    private IBillableServiceRepository _repo;

    public GetServicesQuery ( IBillableServiceRepository repo ) => _repo = repo;

    public Task<List<GetServicesDto>> GetServicesAsync (  )
    {
        return _repo.GetServicesAsync();
    }
}