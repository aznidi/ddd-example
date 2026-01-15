using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Students;
using SMS.Application.Features.Pedagogy.Students.Commands.CreateStudent;
using SMS.Application.Features.Pedagogy.Students.Commands.UpdateStudent;
using SMS.Application.Features.Pedagogy.Students.Commands.DeleteStudent;
using SMS.Application.Features.Pedagogy.Students.Queries.GetStudents;
using SMS.Application.Features.Pedagogy.Students.Queries.GetStudentById;

namespace SMS.Api.Controllers.Finance;

[ApiController]
[Route("api/students")]
public sealed class StudentController : BaseApiController
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var students = await _mediator.Send(new GetStudentsQuery(), ct);
        
        return ApiOk(students, "Students fetched successfully");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery(id), ct);
        
        return ApiOk(student, "Student fetched successfully");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest req, CancellationToken ct)
    {
        var command = new CreateStudentCommand(
            FirstName: req.FirstName,
            LastName: req.LastName,
            BirthDate: req.BirthDate
        );

        var studentId = await _mediator.Send(command, ct);

        return ApiOk(studentId, "Student created successfully");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateStudentRequest req,
        CancellationToken ct)
    {
        var command = new UpdateStudentCommand(
            StudentId: id,
            FirstName: req.FirstName,
            LastName: req.LastName,
            BirthDate: req.BirthDate
        );

        await _mediator.Send(command, ct);

        return ApiOk(Unit.Value, "Student updated successfully");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new DeleteStudentCommand(id);

        await _mediator.Send(command, ct);

        return ApiOk(Unit.Value, "Student deleted successfully");
    }
}