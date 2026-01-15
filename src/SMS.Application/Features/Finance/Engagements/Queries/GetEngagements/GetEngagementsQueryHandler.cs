using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagements;

public sealed class GetEngagementsQueryHandler 
    : IRequestHandler<GetEngagementsQuery, List<EngagementDto>>
{
    private readonly IEngagementRepository _repo;

    public GetEngagementsQueryHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public Task<List<EngagementDto>> Handle(
        GetEngagementsQuery request, 
        CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }
}
