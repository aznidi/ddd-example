using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Students;
using SMS.Application.Commands.Students;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Api.Controllers.Finance;

[ApiController]
[Route( "api/students" )]
public sealed class StudentController : BaseApiController
{
    public readonly CreateStudentCommand _handler;

    public StudentController ( CreateStudentCommand handler ) => _handler = handler;

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateStudentRequest req, CancellationToken ct )
    {
        StudentId studentId = await _handler.CreateStudent(req.FirstName, req.LastName, req.BirthDate, ct);

        return ApiOk(studentId, "Student created successfully");
    }
}