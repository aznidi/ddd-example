using SMS.Domain.BuildingBlocks;

namespace SMS.Domain.Modules.Finance.ValueObjects;

public enum TRANCHE_STATUS
{
    PENDING,
    PAID,
    PARTIALLY_PAID,
    OVERDUE
}