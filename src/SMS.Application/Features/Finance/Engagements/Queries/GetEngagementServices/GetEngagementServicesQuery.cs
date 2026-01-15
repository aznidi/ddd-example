using MediatR;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagementServices;

public sealed record GetEngagementServicesQuery(Guid EngagementId) 
    : IRequest<List<EngagementLineDto>>;
