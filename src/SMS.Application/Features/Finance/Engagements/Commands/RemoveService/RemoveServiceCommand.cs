using MediatR;

namespace SMS.Application.Features.Finance.Engagements.Commands.RemoveService;

public sealed record RemoveServiceCommand(
    Guid EngagementId,
    Guid ServiceId
) : IRequest<Unit>;
