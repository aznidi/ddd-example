using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Queries.Engagements;

public sealed class GetEngagementsQueryHandler
    : IRequestHandler<GetEngagementsQuery, List<EngagementDto>>
{
    private readonly IEngagementRepository _repo;

    public GetEngagementsQueryHandler(IEngagementRepository repo)
        => _repo = repo;

    public Task<List<EngagementDto>> Handle(GetEngagementsQuery request, CancellationToken ct)
        => _repo.GetAllAsync(ct);
}
