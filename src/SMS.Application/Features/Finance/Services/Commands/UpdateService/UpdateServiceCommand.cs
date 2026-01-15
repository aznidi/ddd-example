using MediatR;
using SMS.Application.DTOs.BillableServices;

namespace SMS.Application.Features.Finance.Services.Commands.UpdateService;

public sealed record UpdateServiceCommand(
    Guid ServiceId,
    string? Name,
    decimal? Price,
    string? Currency,
    string? Description
) : IRequest<UpdateServiceDto>;
