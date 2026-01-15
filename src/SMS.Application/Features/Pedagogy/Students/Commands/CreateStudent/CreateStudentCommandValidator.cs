using FluentValidation;

namespace SMS.Application.Features.Pedagogy.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Le prénom est requis.")
            .MaximumLength(100)
            .WithMessage("Le prénom ne peut pas dépasser 100 caractères.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Le nom de famille est requis.")
            .MaximumLength(100)
            .WithMessage("Le nom de famille ne peut pas dépasser 100 caractères.");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("La date de naissance est requise.")
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("La date de naissance doit être dans le passé.")
            .Must(BeValidAge)
            .WithMessage("L'étudiant doit avoir entre 3 et 100 ans.");
    }

    private bool BeValidAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - birthDate.Year;
        
        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 3 && age <= 100;
    }
}
