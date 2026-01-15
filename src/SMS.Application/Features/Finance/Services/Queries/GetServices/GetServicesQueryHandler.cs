using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.BillableServices;

namespace SMS.Application.Features.Finance.Services.Queries.GetServices;

public sealed class GetServicesQueryHandler 
    : IRequestHandler<GetServicesQuery, List<GetServicesDto>>
{
    private readonly IBillableServiceRepository _repo;

    public GetServicesQueryHandler(IBillableServiceRepository repo)
    {
        _repo = repo;
    }

    public Task<List<GetServicesDto>> Handle(
        GetServicesQuery request, 
        CancellationToken cancellationToken)
    {
        return _repo.GetServicesAsync();
    }
}
