using FluentValidation;

namespace SMS.Application.Features.Finance.Engagements.Commands.ChangePaymentPlan;

public sealed class ChangePaymentPlanCommandValidator : AbstractValidator<ChangePaymentPlanCommand>
{
    public ChangePaymentPlanCommandValidator()
    {
        RuleFor(x => x.EngagementId)
            .NotEmpty()
            .WithMessage("L'identifiant de l'engagement est requis.");

        RuleFor(x => x.NewPaymentPlan)
            .NotEmpty()
            .WithMessage("Le nouveau plan de paiement est requis.")
            .Must(BeValidPaymentPlan)
            .WithMessage("Le plan de paiement doit être: MONTHLY, QUARTERLY, ou YEARLY.");

        RuleFor(x => x.NewFirstDueDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("La nouvelle première échéance doit être dans le futur.");
    }

    private bool BeValidPaymentPlan(string paymentPlan)
    {
        return paymentPlan?.ToUpper() is "MONTHLY" or "QUARTERLY" or "YEARLY";
    }
}
