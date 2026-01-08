using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.Exceptions.BirthDate;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public sealed class BirthDate : ValueObject
{
    public DateOnly Value { get;}

    public BirthDate () {}
    public BirthDate ( DateOnly birthDate )
    {
        if(birthDate > DateOnly.FromDateTime(DateTime.Now)) throw new InvalidBirthDateException("BirthDate cannot be in future");   
        int years = DateTime.Now.Year - birthDate.Year;
        if (years > 150)
            throw new InvalidBirthDateException("Age cannot be more than 150 years");
        Value = birthDate;
    }

      protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

}