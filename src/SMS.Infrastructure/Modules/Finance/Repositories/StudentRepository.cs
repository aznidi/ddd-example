using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Infrastructure.Persistence;

namespace SMS.Infrastructure.Modules.Finance.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly SmsDbContext _db;

    public StudentRepository (SmsDbContext smsDbContext) =>  _db = smsDbContext;

    public Task<Student?> GetByIdAsync (StudentId studentId, CancellationToken ct = default)
            => _db.Students.FirstOrDefaultAsync( s => s.Id.Equals(studentId), ct);

    public async Task AddAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Add(student);
        await _db.SaveChangesAsync(ct);
    }
}