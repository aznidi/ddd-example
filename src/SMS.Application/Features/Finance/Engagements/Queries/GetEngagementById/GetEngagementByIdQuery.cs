using MediatR;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagementById;

public sealed record GetEngagementByIdQuery(Guid EngagementId) : IRequest<EngagementDto>;
