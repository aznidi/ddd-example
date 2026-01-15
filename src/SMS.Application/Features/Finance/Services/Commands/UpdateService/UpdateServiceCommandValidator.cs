using FluentValidation;

namespace SMS.Application.Features.Finance.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .WithMessage("L'identifiant du service est requis.");

        RuleFor(x => x.Name)
            .MaximumLength(200)
            .WithMessage("Le nom du service ne peut pas dépasser 200 caractères.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Le prix doit être supérieur à zéro.")
            .When(x => x.Price.HasValue);

        RuleFor(x => x.Currency)
            .Length(3)
            .WithMessage("La devise doit être un code ISO de 3 caractères (ex: MAD, USD, EUR).")
            .When(x => !string.IsNullOrWhiteSpace(x.Currency));

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("La description ne peut pas dépasser 500 caractères.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
