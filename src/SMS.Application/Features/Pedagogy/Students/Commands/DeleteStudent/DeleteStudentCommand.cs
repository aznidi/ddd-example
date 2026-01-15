using MediatR;

namespace SMS.Application.Features.Pedagogy.Students.Commands.DeleteStudent;

public sealed record DeleteStudentCommand(Guid StudentId) : IRequest<Unit>;
