using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class StudentId : ValueObject
{
    public Guid Value { get; }

    public StudentId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("StudentId cannot be empty.");
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
