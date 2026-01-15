using MediatR;
using SMS.Application.DTOs.Engagements;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudentEngagements;

public sealed record GetStudentEngagementsQuery(Guid StudentId) 
    : IRequest<List<EngagementDto>>;
