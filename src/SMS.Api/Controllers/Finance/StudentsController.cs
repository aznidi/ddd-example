using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Students;
using SMS.Application.Features.Pedagogy.Students.Commands.CreateStudent;
using SMS.Application.Features.Pedagogy.Students.Commands.UpdateStudent;
using SMS.Application.Features.Pedagogy.Students.Commands.DeleteStudent;
using SMS.Application.Features.Pedagogy.Students.Queries.GetStudents;
using SMS.Application.Features.Pedagogy.Students.Queries.GetStudentById;
using SMS.Application.Features.Pedagogy.Students.Queries.GetStudentEngagements;
using SMS.Application.Features.Finance.Engagements.Queries.GetEngagementServices;

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

    [HttpGet("{id:guid}/engagements")]
    public async Task<IActionResult> GetStudentEngagements([FromRoute] Guid id, CancellationToken ct)
    {
        var engagements = await _mediator.Send(new GetStudentEngagementsQuery(id), ct);
        
        return ApiOk(engagements, "Student engagements fetched successfully");
    }

    [HttpGet("{studentId:guid}/engagements/{engagementId:guid}/services")]
    public async Task<IActionResult> GetEngagementServices(
        [FromRoute] Guid studentId, 
        [FromRoute] Guid engagementId,
        CancellationToken ct)
    {
        var services = await _mediator.Send(new GetEngagementServicesQuery(engagementId), ct);
        
        return ApiOk(services, "Engagement services fetched successfully");
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