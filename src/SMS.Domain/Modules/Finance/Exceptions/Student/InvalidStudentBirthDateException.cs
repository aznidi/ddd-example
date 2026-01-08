using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Student;

public sealed class InvalidStudentBirthDateException : DomainException
{
    public InvalidStudentBirthDateException ( 
        string message
    ) : base(message)
    { }
}