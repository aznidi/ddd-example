using MediatR;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Queries.Engagements;

public sealed record GetEngagementsQuery() : IRequest<List<EngagementDto>>;
