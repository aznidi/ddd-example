using FluentValidation;

namespace SMS.Application.Features.Pedagogy.Students.Commands.UpdateStudent;

public sealed class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("L'identifiant de l'étudiant est requis.");

        RuleFor(x => x.FirstName)
            .MaximumLength(100)
            .WithMessage("Le prénom ne peut pas dépasser 100 caractères.")
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .WithMessage("Le nom de famille ne peut pas dépasser 100 caractères.")
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("La date de naissance doit être dans le passé.")
            .Must(BeValidAge)
            .WithMessage("L'étudiant doit avoir entre 3 et 100 ans.")
            .When(x => x.BirthDate.HasValue);
    }

    private bool BeValidAge(DateOnly? birthDate)
    {
        if (!birthDate.HasValue) return true;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - birthDate.Value.Year;
        
        if (birthDate.Value > today.AddYears(-age))
            age--;

        return age >= 3 && age <= 100;
    }
}
