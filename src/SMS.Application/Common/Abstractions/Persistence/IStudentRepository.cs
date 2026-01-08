using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Common.Abstractions.Persistence;

public interface IStudentRepository
{
    public Task<Student?> GetByIdAsync ( StudentId studentId, CancellationToken ct);    
    public Task AddAsync ( Student student, CancellationToken ct);    
}