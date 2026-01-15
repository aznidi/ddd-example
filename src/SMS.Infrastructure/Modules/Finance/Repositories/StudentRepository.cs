using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Infrastructure.Persistence;

namespace SMS.Infrastructure.Modules.Finance.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly SmsDbContext _db;

    public StudentRepository(SmsDbContext smsDbContext) => _db = smsDbContext;

    public Task<Student?> GetByIdAsync(StudentId studentId, CancellationToken ct = default)
        => _db.Students.FirstOrDefaultAsync(s => s.Id.Equals(studentId), ct);

    public async Task<List<Student>> GetAllAsync(CancellationToken ct = default)
        => await _db.Students.ToListAsync(ct);

    public async Task AddAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Add(student);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Update(student);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Student student, CancellationToken ct = default)
    {
        _db.Students.Remove(student);
        await _db.SaveChangesAsync(ct);
    }
}