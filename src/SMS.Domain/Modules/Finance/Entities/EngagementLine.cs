using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Domain.Modules.Finance.ValueObjects.ServiceQuantity;

namespace SMS.Domain.Modules.Finance.Entities;


public sealed class EngagementLine : Entity<EngagementLineId>
{
    public ServiceId ServiceId { get; private set; } = default!;
    public string ServiceNameSnapshot { get; private set; } = default!;
    public Money PriceSnapshot { get; private set; } = default!;

    public Quantity Quantity { get; private set; } = default!;

    public DateOnly CreatedAt { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    

    private EngagementLine () {}
    public EngagementLine (
        ServiceId serviceId,
        string serviceNameSnapshot,
        Money priceSnapshot,
        Quantity quantity,
        EngagementLineId id,
        DateOnly? createdAt = null
    ) : base (id)
    {
        ServiceId = serviceId;
        ServiceNameSnapshot = serviceNameSnapshot;
        PriceSnapshot = priceSnapshot;
        Quantity = quantity;
        if ( createdAt.HasValue ) CreatedAt = createdAt.Value;
    }

    public Money GetLineTotal()
    {
        return PriceSnapshot.Multiply(Quantity.Value);
    }

}