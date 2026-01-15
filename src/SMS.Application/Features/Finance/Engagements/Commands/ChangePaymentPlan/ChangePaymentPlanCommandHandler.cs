using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Commands.ChangePaymentPlan;

public sealed class ChangePaymentPlanCommandHandler 
    : IRequestHandler<ChangePaymentPlanCommand, Unit>
{
    private readonly IEngagementRepository _repo;

    public ChangePaymentPlanCommandHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(
        ChangePaymentPlanCommand request, 
        CancellationToken cancellationToken)
    {
        Engagement? engagement = await _repo.GetByIdWithDetailsAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        // Parse payment plan
        if (!Enum.TryParse<PaymentPlan>(request.NewPaymentPlan, true, out var paymentPlan))
            throw new ArgumentException($"Invalid payment plan: {request.NewPaymentPlan}");

        engagement.ChangePaymentPlan(paymentPlan, request.NewFirstDueDate);

        await _repo.UpdateAsync(engagement, cancellationToken);

        return Unit.Value;
    }
}
