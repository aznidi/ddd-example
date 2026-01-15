using FluentValidation;

namespace SMS.Application.Features.Finance.Engagements.Commands.CreateEngagement;

public sealed class CreateEngagementCommandValidator : AbstractValidator<CreateEngagementCommand>
{
    public CreateEngagementCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("L'identifiant de l'étudiant est requis.");

        RuleFor(x => x.Services)
            .NotEmpty()
            .WithMessage("Au moins un service doit être ajouté à l'engagement.");

        RuleForEach(x => x.Services).ChildRules(service =>
        {
            service.RuleFor(s => s.ServiceId)
                .NotEmpty()
                .WithMessage("L'identifiant du service est requis.");

            service.RuleFor(s => s.Quantity)
                .InclusiveBetween(1, 12)
                .WithMessage("La quantité doit être entre 1 et 12 mois.");
        });

        RuleFor(x => x.PaymentPlan)
            .NotEmpty()
            .WithMessage("Le plan de paiement est requis.")
            .Must(BeValidPaymentPlan)
            .WithMessage("Le plan de paiement doit être: MONTHLY, QUARTERLY, ou YEARLY.");

        RuleFor(x => x.FirstDueDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("La première échéance doit être dans le futur.");
    }

    private bool BeValidPaymentPlan(string paymentPlan)
    {
        return paymentPlan?.ToUpper() is "MONTHLY" or "QUARTERLY" or "YEARLY";
    }
}
