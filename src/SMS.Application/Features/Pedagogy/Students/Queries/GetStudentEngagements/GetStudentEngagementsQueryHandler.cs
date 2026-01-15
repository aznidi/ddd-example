using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudentEngagements;

public sealed class GetStudentEngagementsQueryHandler 
    : IRequestHandler<GetStudentEngagementsQuery, List<EngagementDto>>
{
    private readonly IEngagementRepository _repo;

    public GetStudentEngagementsQueryHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public Task<List<EngagementDto>> Handle(
        GetStudentEngagementsQuery request, 
        CancellationToken cancellationToken)
    {
        return _repo.GetByStudentIdAsync(
            new StudentId(request.StudentId), 
            cancellationToken);
    }
}
