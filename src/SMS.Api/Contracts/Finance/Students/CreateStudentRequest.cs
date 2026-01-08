using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Api.Contracts.Finance.Students;

public sealed record CreateStudentRequest ( string FirstName, string LastName, DateOnly BirthDate );