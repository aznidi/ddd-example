using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.Exceptions;


namespace SMS.Domain.Modules.Finance.ValueObjects;


public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }


    private static readonly HashSet<string> AllowedCurrs = new ( StringComparer.OrdinalIgnoreCase )
    {
        "MAD"
    };

    public Money (decimal amount , string currency)
    {
        if ( amount < 0 ) throw new InvalidMoneyException("Amount cannot be negative.");
        if( string.IsNullOrWhiteSpace(currency) ) throw new InvalidCurrencyException("Currency is required");
        if ( ! AllowedCurrs.Contains(currency)) throw new InvalidMoneyException("Currency not allowed");

        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public Money Multiply ( decimal factor )
    {
        return new Money ( Amount * factor , Currency );
    }

    public Money Add ( decimal value )
    {
        return new Money ( Amount + value, Currency );
    }

    public Money Add ( Money value )
    {
        if( ! Currency.Equals( value.Currency ) ) throw new InvalidCurrencyException("Currency mismatch");
        return new Money ( Amount + value.Amount, Currency );
    }

    public Money Subtract(Money value)
    {
        if (!Currency.Equals(value.Currency)) throw new InvalidCurrencyException("Currency mismatch");
        return new Money(Amount - value.Amount, Currency);
    }
}

