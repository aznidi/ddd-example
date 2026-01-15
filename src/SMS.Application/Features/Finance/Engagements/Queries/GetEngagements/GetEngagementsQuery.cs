using MediatR;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagements;

public sealed record GetEngagementsQuery() : IRequest<List<EngagementDto>>;
