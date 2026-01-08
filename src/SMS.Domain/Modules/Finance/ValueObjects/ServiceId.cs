using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class ServiceId : ValueObject
{
    public Guid Value { get; }

    public ServiceId(
        Guid value
    )
    {
        if(value == Guid.Empty) throw new ArgumentException("Service Id cannot be empty ");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}