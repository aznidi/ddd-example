using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Commands.Students;

public sealed class CreateStudentCommand
{
    private IStudentRepository _repo;

    public CreateStudentCommand (
        IStudentRepository repo
    )
    {
        _repo = repo;
    }

    public async Task<StudentId> CreateStudent(string firstName, string lastName, DateOnly birthDate, CancellationToken ct)
    {
        Student student = new Student (
            id: new StudentId(Guid.NewGuid()),
            firstname: firstName,
            lastname: lastName,
            birthdate: new BirthDate(birthDate)
        );        

        await _repo.AddAsync(student, ct);

        return student.Id;

    }
}