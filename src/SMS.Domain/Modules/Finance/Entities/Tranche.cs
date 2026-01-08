using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Domain.Modules.Finance.Entities;

public sealed class Tranche : Entity<TrancheId>
{
    public DateOnly DueDate { get; private set; } = default!;
    public Money AmountDue { get; private set; } = default!;
    public TRANCHE_STATUS Status { get; private set; } = default!;

    private Tranche () {}
    public Tranche (
        TrancheId id,
        DateOnly duedate,
        Money amountdue,
        TRANCHE_STATUS status
    ) : base(id)
    {
        DueDate = duedate;
        AmountDue = amountdue;
        Status = status;
    }



}