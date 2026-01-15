namespace SMS.Application.DTOs.Engagements;

public sealed class EngagementDto
{
    public string EngagementId { get; init; } = default!;
    public string StudentId { get; init; } = default!;

    public MoneyDto TotalAmount { get; init; } = default!;

    public List<EngagementLineDto> Lines { get; init; } = new();
    public List<TrancheDto> Tranches { get; init; } = new();
}

public sealed class EngagementLineDto
{
    public string EngagementLineId { get; init; } = default!;
    public string ServiceId { get; init; } = default!;
    public string ServiceNameSnapshot { get; init; } = default!;

    public MoneyDto PriceSnapshot { get; init; } = default!;
    public QuantityDto Quantity { get; init; } = default!;

    public MoneyDto LineTotal { get; init; } = default!;
}

public sealed class TrancheDto
{
    public string TrancheId { get; init; } = default!;
    public DateOnly DueDate { get; init; }
    public MoneyDto AmountDue { get; init; } = default!;

    public string Status { get; init; } = default!;
}

public sealed class MoneyDto
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = default!;
}

public sealed class QuantityDto
{
    public decimal Value { get; init; }

    public string? Unit { get; init; }
}
