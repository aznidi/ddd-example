using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Pedagogy.Students.Commands.UpdateStudent;

public sealed class UpdateStudentCommandHandler 
    : IRequestHandler<UpdateStudentCommand, Unit>
{
    private readonly IStudentRepository _repo;

    public UpdateStudentCommandHandler(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(
        UpdateStudentCommand request, 
        CancellationToken cancellationToken)
    {
        Student? student = await _repo.GetByIdAsync(
            new StudentId(request.StudentId), 
            cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"Student with ID {request.StudentId} not found.");

        // Update name if both FirstName and LastName are provided
        if (!string.IsNullOrWhiteSpace(request.FirstName) && 
            !string.IsNullOrWhiteSpace(request.LastName))
        {
            student.Rename(request.FirstName, request.LastName);
        }
        else if (!string.IsNullOrWhiteSpace(request.FirstName) || 
                 !string.IsNullOrWhiteSpace(request.LastName))
        {
            // If only one is provided, use existing value for the other
            student.Rename(
                request.FirstName ?? student.FirstName,
                request.LastName ?? student.LastName
            );
        }

        // Update birth date if provided
        if (request.BirthDate.HasValue)
        {
            student.UpdateBirthDate(new BirthDate(request.BirthDate.Value));
        }

        await _repo.UpdateAsync(student, cancellationToken);

        return Unit.Value;
    }
}
