using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Commands.RemoveService;

public sealed class RemoveServiceCommandHandler 
    : IRequestHandler<RemoveServiceCommand, Unit>
{
    private readonly IEngagementRepository _repo;

    public RemoveServiceCommandHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(
        RemoveServiceCommand request, 
        CancellationToken cancellationToken)
    {
        Engagement? engagement = await _repo.GetByIdWithDetailsAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        engagement.RemoveService(new ServiceId(request.ServiceId));

        await _repo.UpdateAsync(engagement, cancellationToken);

        return Unit.Value;
    }
}
