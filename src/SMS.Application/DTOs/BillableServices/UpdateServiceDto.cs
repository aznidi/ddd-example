using SMS.Domain.Modules.Finance.Entities;

namespace SMS.Application.DTOs.BillableServices;

public sealed class UpdateServiceDto
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public string Currency { get; }
    public string? Description { get; }

    public UpdateServiceDto(
        BillableService billableService
    )
    {
        Id = billableService.Id.Value;
        Name = billableService.Name;
        Price = billableService.Price.Amount;
        Currency = billableService.Price.Currency;
        Description = billableService.Description;
    }
}