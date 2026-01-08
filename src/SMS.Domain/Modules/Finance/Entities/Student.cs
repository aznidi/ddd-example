using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.Exceptions.Student;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Domain.Modules.Finance.Entities;


public sealed class Student : Entity<StudentId>
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public BirthDate BirthDate { get; private set; } = default!;


    private Student () {}
    public Student (
        StudentId id,
        string firstname,
        string lastname,
        BirthDate birthdate
    ) : base(id)
    {
        if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) ) 
            throw new InvalidStudentNameException("name cannot be empty");

        FirstName = firstname.Trim();
        LastName = lastname.Trim();
        BirthDate = birthdate;
    }

    public string GetFullName()
    {
        return string.Concat(FirstName, " ", LastName);
    }

    public void Rename(string firstname, string lastname)
    {
        if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) ) 
            throw new InvalidStudentNameException("name cannot be empty");

        FirstName = firstname;
        LastName = lastname;
    }

    public void UpdateBirthDate(BirthDate birthdate)
    {
        BirthDate = birthdate;
    }


}