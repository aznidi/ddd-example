using MediatR;

namespace SMS.Application.Features.Finance.Engagements.Commands.DeleteEngagement;

public sealed record DeleteEngagementCommand(Guid EngagementId) : IRequest<Unit>;
