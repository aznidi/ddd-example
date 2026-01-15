using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Students;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudentById;

public sealed class GetStudentByIdQueryHandler 
    : IRequestHandler<GetStudentByIdQuery, StudentDto>
{
    private readonly IStudentRepository _repo;

    public GetStudentByIdQueryHandler(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<StudentDto> Handle(
        GetStudentByIdQuery request, 
        CancellationToken cancellationToken)
    {
        Student? student = await _repo.GetByIdAsync(
            new StudentId(request.StudentId), 
            cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"Student with ID {request.StudentId} not found.");

        return new StudentDto(
            id: student.Id.Value,
            firstName: student.FirstName,
            lastName: student.LastName,
            birthDate: student.BirthDate.Value,
            fullName: student.GetFullName()
        );
    }
}
