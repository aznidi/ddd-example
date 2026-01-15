namespace SMS.Api.Contracts.Finance.Students;

public sealed record UpdateStudentRequest(
    string? FirstName,
    string? LastName,
    DateOnly? BirthDate
);
