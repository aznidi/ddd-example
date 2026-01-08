using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Infrastructure.Persistence;

namespace SMS.Infrastructure.Modules.Finance.Repositories;

public sealed class EngagementRepository : IEngagementRepository
{
    private readonly SmsDbContext _db;

    public EngagementRepository(SmsDbContext db) => _db = db;

    public Task<Engagement?> GetByIdAsync(EngagementId id, CancellationToken ct = default)
        => _db.Engagements.FirstOrDefaultAsync(x => x.Id.Equals(id), ct);

    public Task<Engagement?> GetByIdWithDetailsAsync(EngagementId id, CancellationToken ct = default)
        => _db.Engagements
            .Include(e => e.Lines)
            .Include(e => e.Tranches)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), ct);

    public async Task AddAsync(Engagement engagement, CancellationToken ct = default)
    {
        _db.Engagements.Add(engagement);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Engagement engagement, CancellationToken ct = default)
    {
        _db.Engagements.Update(engagement);
        await _db.SaveChangesAsync(ct);
    }
}