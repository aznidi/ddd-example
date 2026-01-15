using MediatR;

namespace SMS.Application.Features.Finance.Engagements.Commands.ChangePaymentPlan;

public sealed record ChangePaymentPlanCommand(
    Guid EngagementId,
    string NewPaymentPlan,  // "MONTHLY", "QUARTERLY", "YEARLY"
    DateOnly NewFirstDueDate
) : IRequest<Unit>;
