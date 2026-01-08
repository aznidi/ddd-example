using SMS.Domain.BuildingBlocks;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.Exceptions.Engagement;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Domain.Modules.Finance.ValueObjects.ServiceQuantity;

namespace SMS.Domain.Modules.Finance.Aggregates;


public sealed class Engagement : AggregateRoot<EngagementId>
{
    public StudentId StudentId { get; private set; }
    private readonly List<EngagementLine> _lines = new();
    private readonly List<Tranche> _tranches = new();
    public Money TotalAmount { get; private set; }

    

    public Engagement (
        EngagementId id,
        StudentId studentId
    ) : base( id )
    {
        TotalAmount = new Money(0, "MAD");
        StudentId = studentId;
    }

    public IReadOnlyCollection<EngagementLine> Lines => _lines;
    public IReadOnlyCollection<Tranche> Tranches => _tranches;

    public void AddService(
        ServiceId serviceId,
        string serviceNameSnapshot,
        Money priceSnapshot,
        Quantity quantity
    )
    {
        bool exists = _lines.Any( l => l.ServiceId.Equals(serviceId) );

        if ( exists ) throw new DuplicateEngagementServiceException(" Service Already exists ");

        EngagementLine engagementLine = new EngagementLine(
            serviceId: serviceId,
            serviceNameSnapshot: serviceNameSnapshot,
            priceSnapshot: priceSnapshot,
            quantity: quantity,
            id: new EngagementLineId(Guid.NewGuid())
        );

        _lines.Add(engagementLine);

        TotalAmount = TotalAmount.Add(engagementLine.GetLineTotal());
    }

    public void GenerateTranches (
        PaymentPlan paymentPlan,
        DateOnly firstDueDate
    )
    {
        if ( _lines.Count == 0 )
            throw new FailedToGenerateTranches(" Cannot generate tranches for an engagement with no services ");

        if ( _tranches.Count > 0 )
            throw new FailedToGenerateTranches(" Tranches have already been generated for this engagement ");

        if( TotalAmount.Amount <= 0 )
            throw new FailedToGenerateTranches(" Cannot generate tranches for an engagement with zero or negative total amount ");

        int numberOfTranches = GetTranchesCount( paymentPlan );


        decimal baseAmount = Math.Floor( TotalAmount.Amount / numberOfTranches  * 100) / 100;
        decimal totalBase = baseAmount * ( numberOfTranches - 1);
        decimal lastAmount = TotalAmount.Amount - totalBase;

        DateOnly dueDate = firstDueDate;
        for(int i = 1 ; i <= numberOfTranches ; i ++ )
        {
            decimal amount = (i == numberOfTranches) ? lastAmount : baseAmount;

            _tranches.Add( new Tranche (
                id: new TrancheId(Guid.NewGuid()),
                duedate: dueDate,
                amountdue: new Money(amount, TotalAmount.Currency),
                status: TRANCHE_STATUS.PENDING
            ));

            dueDate = paymentPlan switch
            {
                PaymentPlan.MONTHLY => dueDate.AddMonths(1),
                PaymentPlan.QUARTERLY => dueDate.AddMonths(3),
                _ => dueDate
            };
        }

    }

    private static int GetTranchesCount(PaymentPlan paymentPlan)
    {
        return paymentPlan switch
        {
            PaymentPlan.MONTHLY => 10,
            PaymentPlan.QUARTERLY => 4,
            PaymentPlan.YEARLY => 1,
            _ => throw new FailedToGenerateTranches(" Invalid Number Of Tranches ")
        };
    }

}