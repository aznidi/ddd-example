using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class EngagementId : ValueObject
{
    public Guid Value { get; }

    public EngagementId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("EngagementId cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
