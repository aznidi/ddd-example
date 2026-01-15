using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.DTOs.Students;

public sealed class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirthDate { get; set; }
    public string FullName { get; set; } = default!;

    public StudentDto() { }

    public StudentDto(Guid id, string firstName, string lastName, DateOnly birthDate, string fullName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        FullName = fullName;
    }
}
