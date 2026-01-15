using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Engagements;
using SMS.Application.Features.Finance.Engagements.Commands.CreateEngagement;
using SMS.Application.Features.Finance.Engagements.Commands.DeleteEngagement;
using SMS.Application.Features.Finance.Engagements.Commands.ChangePaymentPlan;
using SMS.Application.Features.Finance.Engagements.Commands.RemoveService;
using SMS.Application.Features.Finance.Engagements.Queries.GetEngagements;
using SMS.Application.Features.Finance.Engagements.Queries.GetEngagementById;
using SMS.Application.Features.Finance.Engagements.Commands.GenerateEngagementFile;

namespace SMS.Api.Controllers.Finance;

[ApiController]
[Route("api/engagements")]
public sealed class EngagementController : BaseApiController
{
    private readonly IMediator _mediator;

    public EngagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var engagements = await _mediator.Send(new GetEngagementsQuery(), ct);

        return ApiOk(
            engagements,
            "Engagements fetched successfully"
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var engagement = await _mediator.Send(new GetEngagementByIdQuery(id), ct);

        return ApiOk(
            engagement,
            "Engagement fetched successfully"
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEngagementRequest req,
        CancellationToken ct)
    {
        var command = new CreateEngagementCommand(
            StudentId: req.StudentId,
            Services: req.Services.Select(s => 
                new SMS.Application.Features.Finance.Engagements.Commands.CreateEngagement.EngagementServiceRequest(
                    ServiceId: s.ServiceId,
                    Quantity: s.Quantity
                )).ToList(),
            PaymentPlan: req.PaymentPlan,
            FirstDueDate: req.FirstDueDate
        );

        var engagementId = await _mediator.Send(command, ct);

        return ApiOk(
            engagementId,
            "Engagement created successfully"
        );
    }

    [HttpPut("{id:guid}/payment-plan")]
    public async Task<IActionResult> ChangePaymentPlan(
        [FromRoute] Guid id,
        [FromBody] ChangePaymentPlanRequest req,
        CancellationToken ct)
    {
        var command = new ChangePaymentPlanCommand(
            EngagementId: id,
            NewPaymentPlan: req.PaymentPlan,
            NewFirstDueDate: req.FirstDueDate
        );

        await _mediator.Send(command, ct);

        return ApiOk(
            Unit.Value,
            "Payment plan changed successfully"
        );
    }

    [HttpDelete("{engagementId:guid}/services/{serviceId:guid}")]
    public async Task<IActionResult> RemoveService(
        [FromRoute] Guid engagementId,
        [FromRoute] Guid serviceId,
        CancellationToken ct)
    {
        var command = new RemoveServiceCommand(
            EngagementId: engagementId,
            ServiceId: serviceId
        );

        await _mediator.Send(command, ct);

        return ApiOk(
            Unit.Value,
            "Service removed successfully"
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new DeleteEngagementCommand(id);

        await _mediator.Send(command, ct);

        return ApiOk(
            Unit.Value,
            "Engagement deleted successfully"
        );
    }

    [HttpPost("{id:guid}/generate")]
    public async Task<IActionResult> GenerateEngagementFile([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new GenerateEngagementFileCommand(id);

        var pdfBytes = await _mediator.Send(command, ct);

        return File(
            pdfBytes,
            "application/pdf",
            $"Engagement_{id}_{DateTime.Now:yyyyMMdd}.pdf"
        );
    }
}