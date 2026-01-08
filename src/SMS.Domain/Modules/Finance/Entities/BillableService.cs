using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.Exceptions;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Domain.Modules.Finance.Entities;

public sealed class BillableService : Entity<ServiceId>
{
    public string Name { get; private set; } = default!;
    public Money Price { get; private set; } = default!;
    public string? Description { get; private set; } = default!;

    private BillableService () {}
    public BillableService(ServiceId id, string name, Money price, string? description) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidServiceNameException("Service name is required.");

        Name = name.Trim();
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Description = description;
    }


    public void Rename(string name) => SetName(name);

    public void ChangePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidServiceNameException("Service name is required.");

        Name = name.Trim();
    }
}
