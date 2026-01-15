using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Commands.DeleteEngagement;

public sealed class DeleteEngagementCommandHandler 
    : IRequestHandler<DeleteEngagementCommand, Unit>
{
    private readonly IEngagementRepository _repo;

    public DeleteEngagementCommandHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(
        DeleteEngagementCommand request, 
        CancellationToken cancellationToken)
    {
        Engagement? engagement = await _repo.GetByIdAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        await _repo.DeleteAsync(engagement, cancellationToken);

        return Unit.Value;
    }
}
