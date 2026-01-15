using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Services;
using SMS.Application.Features.Finance.Services.Commands.CreateService;
using SMS.Application.Features.Finance.Services.Commands.UpdateService;
using SMS.Application.Features.Finance.Services.Queries.GetServices;

namespace SMS.Api.Controllers.Finance;


[ApiController]
[Route("api/services")]
public sealed class ServiceController : BaseApiController
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _mediator.Send(new GetServicesQuery());
        
        return ApiOk(
            services,
            "Services fetched successfully"
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateService(
        [FromBody] CreateServiceRequest req
    )
    {
        var command = new CreateServiceCommand(
            Name: req.Name,
            Price: req.Price,
            Currency: req.Currency,
            Description: req.Description
        );

        var response = await _mediator.Send(command);

        return ApiOk(
            response,
            "Service created successfully"
        );
    }

    [HttpPut("{serviceId:guid}")]
    public async Task<IActionResult> UpdateService(
        [FromRoute] Guid serviceId,
        [FromBody] UpdateServiceRequest req
    )
    {
        var command = new UpdateServiceCommand(
            ServiceId: serviceId,
            Name: req.Name,
            Price: req.Price,
            Currency: req.Currency,
            Description: req.Description
        );

        var response = await _mediator.Send(command);
        
        return ApiOk(
            response,
            "Service updated successfully"
        );
    }
}