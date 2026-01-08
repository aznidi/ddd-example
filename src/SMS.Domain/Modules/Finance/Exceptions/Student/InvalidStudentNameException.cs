using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.Exceptions.Student;


public sealed class InvalidStudentNameException : DomainException
{
    public InvalidStudentNameException (
        string message
    ) : base(message)
    {
        
    }
}