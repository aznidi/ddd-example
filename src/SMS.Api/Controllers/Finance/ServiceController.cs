using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Services;
using SMS.Application.Commands.BillableServices;
using SMS.Application.DTOs.BillableServices;
using SMS.Application.Queries.BillableServices;

namespace SMS.Api.Controllers.Finance;


[ApiController]
[Route( "api/services" )]
public sealed class ServiceController : BaseApiController
{

    private readonly GetServicesQuery _query;
    private readonly CreateServiceCommand _createCommand;
    private readonly UpdateServiceCommand _updateCommand;

    public ServiceController(
        GetServicesQuery getServicesQuery,
        CreateServiceCommand createServiceCommand,
        UpdateServiceCommand updateServiceCommand
    )
    {
        _query = getServicesQuery;
        _createCommand = createServiceCommand;
        _updateCommand = updateServiceCommand;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        List<GetServicesDto> services = await _query.GetServicesAsync();
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
        CreateServiceDto response = await _createCommand.CreateService(
            name: req.Name,
            price: req.Price,
            currency: req.Currency,
            description: req.Description,
            ct: CancellationToken.None
        );

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

        UpdateServiceDto response = await _updateCommand.UpdateService(
            serviceId:  serviceId.ToString(),
            name: req.Name,
            price: req.Price,
            currency: req.Currency,
            description: req.Description,
            ct: CancellationToken.None
        );
        return ApiOk(
            response,
            "Service updated successfully"
        );
    }
}