using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class TrancheId : ValueObject
{
    public Guid Value { get; }

    public TrancheId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("TrancheId cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
