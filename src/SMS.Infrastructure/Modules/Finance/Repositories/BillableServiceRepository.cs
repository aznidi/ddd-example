using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Infrastructure.Persistence;

namespace SMS.Infrastructure.Modules.Finance.Repositories;

public sealed class BillableServiceRepository : IBillableServiceRepository
{
    private readonly SmsDbContext _db;

    public BillableServiceRepository(SmsDbContext db) => _db = db;

    public Task<BillableService?> GetByIdAsync(ServiceId id, CancellationToken ct = default)
        => _db.BillableServices.FirstOrDefaultAsync(x => x.Id.Equals(id), ct);

    public async Task<CreateServiceDto> AddAsync(BillableService service, CancellationToken ct = default)
    {
        _db.BillableServices.Add(service);
        await _db.SaveChangesAsync(ct);
        return new CreateServiceDto(service.Id);
    }

    public async Task<List<GetServicesDto>> GetServicesAsync ()
    {
        return await _db.
        BillableServices.
        AsNoTracking()
        .Select( bs => new GetServicesDto
        {
            ServiceId = bs.Id.Value.ToString(),
            ServiceName = bs.Name,
            Description = bs.Description,
            Price = bs.Price.Amount,
            Currency = bs.Price.Currency
        }).ToListAsync();
    }
    

    
}
