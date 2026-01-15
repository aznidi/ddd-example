using MediatR;
using SMS.Application.DTOs.Students;

namespace SMS.Application.Features.Pedagogy.Students.Queries.GetStudents;

public sealed record GetStudentsQuery() : IRequest<List<StudentDto>>;
