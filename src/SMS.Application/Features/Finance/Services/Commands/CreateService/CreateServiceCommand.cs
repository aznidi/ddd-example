using MediatR;
using SMS.Application.DTOs.BillableServices;

namespace SMS.Application.Features.Finance.Services.Commands.CreateService;

public sealed record CreateServiceCommand(
    string Name,
    decimal Price,
    string Currency,
    string? Description
) : IRequest<CreateServiceDto>;
