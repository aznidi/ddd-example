using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Infrastructure.Persistence;

namespace SMS.Infrastructure.Modules.Finance.Repositories;

public sealed class EngagementRepository : IEngagementRepository
{
    private readonly SmsDbContext _db;

    public EngagementRepository(SmsDbContext db) => _db = db;


    public Task<List<EngagementDto>> GetAllAsync(CancellationToken ct = default)
    {
        return _db.Engagements
            .AsNoTracking()
            .AsSplitQuery()
            .Select(e => new EngagementDto
            {
                EngagementId = e.Id.Value.ToString(),
                StudentId = e.StudentId.Value.ToString(),

                TotalAmount = new MoneyDto
                {
                    Amount = e.TotalAmount.Amount,
                    Currency = e.TotalAmount.Currency
                },

                Lines = e.Lines.Select(l => new EngagementLineDto
                {
                    EngagementLineId = l.Id.Value.ToString(),
                    ServiceId = l.ServiceId.Value.ToString(),
                    ServiceNameSnapshot = l.ServiceNameSnapshot,

                    PriceSnapshot = new MoneyDto
                    {
                        Amount = l.PriceSnapshot.Amount,
                        Currency = l.PriceSnapshot.Currency
                    },

                    Quantity = new QuantityDto
                    {
                        Value = l.Quantity.Value,
                        Unit = null
                    },

                    LineTotal = new MoneyDto
                    {
                        Amount = l.PriceSnapshot.Amount * l.Quantity.Value,
                        Currency = l.PriceSnapshot.Currency
                    }
                }).ToList(),

                Tranches = e.Tranches.Select(t => new TrancheDto
                {
                    TrancheId = t.Id.Value.ToString(),
                    DueDate = t.DueDate,

                    AmountDue = new MoneyDto
                    {
                        Amount = t.AmountDue.Amount,
                        Currency = t.AmountDue.Currency
                    },

                    Status = t.Status.ToString()
                }).ToList()
            })
            .ToListAsync(ct);
    }

    public Task<List<EngagementDto>> GetByStudentIdAsync(StudentId studentId, CancellationToken ct = default)
    {
        return _db.Engagements
            .Where(e => e.StudentId.Equals(studentId))
            .AsNoTracking()
            .AsSplitQuery()
            .Select(e => new EngagementDto
            {
                EngagementId = e.Id.Value.ToString(),
                StudentId = e.StudentId.Value.ToString(),

                TotalAmount = new MoneyDto
                {
                    Amount = e.TotalAmount.Amount,
                    Currency = e.TotalAmount.Currency
                },

                Lines = e.Lines.Select(l => new EngagementLineDto
                {
                    EngagementLineId = l.Id.Value.ToString(),
                    ServiceId = l.ServiceId.Value.ToString(),
                    ServiceNameSnapshot = l.ServiceNameSnapshot,

                    PriceSnapshot = new MoneyDto
                    {
                        Amount = l.PriceSnapshot.Amount,
                        Currency = l.PriceSnapshot.Currency
                    },

                    Quantity = new QuantityDto
                    {
                        Value = l.Quantity.Value,
                        Unit = null
                    },

                    LineTotal = new MoneyDto
                    {
                        Amount = l.PriceSnapshot.Amount * l.Quantity.Value,
                        Currency = l.PriceSnapshot.Currency
                    }
                }).ToList(),

                Tranches = e.Tranches.Select(t => new TrancheDto
                {
                    TrancheId = t.Id.Value.ToString(),
                    DueDate = t.DueDate,

                    AmountDue = new MoneyDto
                    {
                        Amount = t.AmountDue.Amount,
                        Currency = t.AmountDue.Currency
                    },

                    Status = t.Status.ToString()
                }).ToList()
            })
            .ToListAsync(ct);
    }


       
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

    public async Task DeleteAsync(Engagement engagement, CancellationToken ct = default)
    {
        _db.Engagements.Remove(engagement);
        await _db.SaveChangesAsync(ct);
    }
}