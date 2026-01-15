using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Pedagogy.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandHandler 
    : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _repo;

    public CreateStudentCommandHandler(IStudentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(
        CreateStudentCommand request, 
        CancellationToken cancellationToken)
    {
        Student student = new Student(
            id: new StudentId(Guid.NewGuid()),
            firstname: request.FirstName,
            lastname: request.LastName,
            birthdate: new BirthDate(request.BirthDate)
        );

        await _repo.AddAsync(student, cancellationToken);

        return student.Id.Value;
    }
}
