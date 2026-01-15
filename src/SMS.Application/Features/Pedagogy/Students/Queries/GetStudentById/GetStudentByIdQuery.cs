using MediatR;
using SMS.Application.DTOs.Students;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudentById;

public sealed record GetStudentByIdQuery(Guid StudentId) : IRequest<StudentDto>;
