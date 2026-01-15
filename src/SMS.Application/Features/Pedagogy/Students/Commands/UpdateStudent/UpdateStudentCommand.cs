using MediatR;

namespace SMS.Application.Features.Pedagogy.Students.Commands.UpdateStudent;

public sealed record UpdateStudentCommand(
    Guid StudentId,
    string? FirstName,
    string? LastName,
    DateOnly? BirthDate
) : IRequest<Unit>;
