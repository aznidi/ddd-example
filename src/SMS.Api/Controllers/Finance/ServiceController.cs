using Microsoft.AspNetCore.Mvc;
using SMS.Api.Contracts.Finance.Services;
using SMS.Application.Commands.BillableServices;
using SMS.Application.DTOs.BillableServices;
using SMS.Application.Queries.BillableServices;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Api.Controllers.Finance;


[ApiController]
[Route( "api/services" )]
public sealed class ServiceController : BaseApiController
{

    private readonly GetServicesQuery _query;
    private readonly CreateServiceCommand _createCommand;

    public ServiceController(
        GetServicesQuery getServicesQuery,
        CreateServiceCommand createServiceCommand
    )
    {
        _query = getServicesQuery;
        _createCommand = createServiceCommand;
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
}