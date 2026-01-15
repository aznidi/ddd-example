using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Pedagogy.Students.Commands.DeleteStudent;

public sealed class DeleteStudentCommandHandler 
    : IRequestHandler<DeleteStudentCommand, Unit>
{
    private readonly IStudentRepository _repo;

    public DeleteStudentCommandHandler(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(
        DeleteStudentCommand request, 
        CancellationToken cancellationToken)
    {
        Student? student = await _repo.GetByIdAsync(
            new StudentId(request.StudentId), 
            cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"Student with ID {request.StudentId} not found.");

        await _repo.DeleteAsync(student, cancellationToken);

        return Unit.Value;
    }
}
