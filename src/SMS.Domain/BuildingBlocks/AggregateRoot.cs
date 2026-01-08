namespace SMS.Domain.BuildingBlocks;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected AggregateRoot() { }
}