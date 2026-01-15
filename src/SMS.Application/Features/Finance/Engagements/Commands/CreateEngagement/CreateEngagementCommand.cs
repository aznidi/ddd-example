using MediatR;

namespace SMS.Application.Features.Finance.Engagements.Commands.CreateEngagement;

public sealed record CreateEngagementCommand(
    Guid StudentId,
    List<EngagementServiceRequest> Services,
    string PaymentPlan,  // "MONTHLY", "QUARTERLY", "YEARLY"
    DateOnly FirstDueDate
) : IRequest<Guid>;

public sealed record EngagementServiceRequest(
    Guid ServiceId,
    int Quantity  // 1-12 months
);

