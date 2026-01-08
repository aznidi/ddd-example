using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Commands.BillableServices;

public sealed class CreateServiceCommand
{
    private IBillableServiceRepository _repo;   
    public CreateServiceCommand (
        IBillableServiceRepository repo
    )
    {
        _repo = repo;
    }

    public async Task<CreateServiceDto> CreateService(string name, decimal price, string? currency, string? description, CancellationToken ct)
    {
        BillableService service = new BillableService (
            id: new ServiceId(Guid.NewGuid()),
            name: name,
            price: new Money(price, currency ?? "MAD"),
            description: description
        );

        await _repo.AddAsync(service, ct);

        return new CreateServiceDto(service.Id);

    }
}