using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects.ServiceQuantity;

// <summary>
// Value Object representing a service quantity.
// The quantity is represented as an integer value (representing the number of months when the service is provided and used). 
// the quantity cannot be negative , need to be between 0, 12 assuming the maximum service period is 12 months.
// </summary>
public sealed class Quantity : ValueObject
{
    public int Value { get; private set; }

    public Quantity ( int value )
    {
        if ( ! IsQuantityValid(value)) throw new ArgumentException("Quantity is invalid need to be between 0 and 12.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsQuantityValid(int value) => value > 0 && value <= 12;
}