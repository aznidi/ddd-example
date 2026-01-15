using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Common.Abstractions.Persistence;

public interface IStudentRepository
{
    public Task<Student?> GetByIdAsync(StudentId studentId, CancellationToken ct);
    public Task<List<Student>> GetAllAsync(CancellationToken ct);
    public Task AddAsync(Student student, CancellationToken ct);
    public Task UpdateAsync(Student student, CancellationToken ct);
    public Task DeleteAsync(Student student, CancellationToken ct);
}