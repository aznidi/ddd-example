using MediatR;

namespace SMS.Application.Features.Pedagogy.Students.Commands.CreateStudent;

public sealed record CreateStudentCommand(
    string FirstName,
    string LastName,
    DateOnly BirthDate
) : IRequest<Guid>;
