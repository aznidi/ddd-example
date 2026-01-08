using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;


public sealed class Period : ValueObject
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public Period(
        DateOnly startDate,
        DateOnly endDate
    )
    {
        if ( !IsPeriodDatesValid(startDate, endDate) )
        {
            throw new ArgumentException("Invalid period dates: StartDate must be less than or equal to EndDate.");
        }
        StartDate = startDate;
        EndDate = endDate;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }

    public static bool IsPeriodDatesValid(
        DateOnly startDate,
        DateOnly endDate
    )
    {
        return startDate <= endDate;
    }
}