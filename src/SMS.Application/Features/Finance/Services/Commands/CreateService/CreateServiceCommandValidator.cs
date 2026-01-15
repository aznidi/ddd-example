using FluentValidation;

namespace SMS.Application.Features.Finance.Services.Commands.CreateService;

public sealed class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Le nom du service est requis.")
            .MaximumLength(200)
            .WithMessage("Le nom du service ne peut pas dépasser 200 caractères.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Le prix doit être supérieur à zéro.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("La devise est requise.")
            .Length(3)
            .WithMessage("La devise doit être un code ISO de 3 caractères (ex: MAD, USD, EUR).");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("La description ne peut pas dépasser 500 caractères.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
