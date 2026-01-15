namespace SMS.Api.Contracts.Finance.Engagements;

public sealed record CreateEngagementRequest(
    Guid StudentId,
    List<EngagementServiceRequest> Services,
    string PaymentPlan,  // "MONTHLY", "QUARTERLY", "YEARLY"
    DateOnly FirstDueDate
);

public sealed record EngagementServiceRequest(
    Guid ServiceId,
    int Quantity  // 1-12 months
);
