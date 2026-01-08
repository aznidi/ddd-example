using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class EngagementLineId : ValueObject
{
    public Guid Value { get; }

    public EngagementLineId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("EngagementLineId cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
