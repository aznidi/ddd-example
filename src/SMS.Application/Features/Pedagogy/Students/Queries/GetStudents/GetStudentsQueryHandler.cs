using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Students;
using SMS.Domain.Modules.Finance.Entities;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudents;

public sealed class GetStudentsQueryHandler 
    : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly IStudentRepository _repo;

    public GetStudentsQueryHandler(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<StudentDto>> Handle(
        GetStudentsQuery request, 
        CancellationToken cancellationToken)
    {
        List<Student> students = await _repo.GetAllAsync(cancellationToken);

        return students.Select(s => new StudentDto(
            id: s.Id.Value,
            firstName: s.FirstName,
            lastName: s.LastName,
            birthDate: s.BirthDate.Value,
            fullName: s.GetFullName()
        )).ToList();
    }
}
