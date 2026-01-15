using MediatR;
using SMS.Application.DTOs.BillableServices;

namespace SMS.Application.Features.Finance.Services.Queries.GetServices;

public sealed record GetServicesQuery() : IRequest<List<GetServicesDto>>;
