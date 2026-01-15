using SMS.Application.DTOs.Engagements;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Common.Abstractions.Persistence;

public interface IEngagementRepository
{
    Task<Engagement?> GetByIdAsync(EngagementId id, CancellationToken ct = default);
    Task<List<EngagementDto>> GetAllAsync(CancellationToken ct = default);
    Task<List<EngagementDto>> GetByStudentIdAsync(StudentId studentId, CancellationToken ct = default);

    Task<Engagement?> GetByIdWithDetailsAsync(EngagementId id, CancellationToken ct = default);

    Task AddAsync(Engagement engagement, CancellationToken ct = default);
    Task UpdateAsync(Engagement engagement, CancellationToken ct = default);
    Task DeleteAsync(Engagement engagement, CancellationToken ct = default);
}

