namespace SMS.Api.Contracts.Finance.Engagements;

public sealed record ChangePaymentPlanRequest(
    string PaymentPlan,  // "MONTHLY", "QUARTERLY", "YEARLY"
    DateOnly FirstDueDate
);
