namespace SMS.Api.Contracts.Finance.Services;

public sealed class UpdateServiceRequest
{
    public string? Name { get; set; } = default!;
    public decimal? Price { get; set; }
    public string? Currency { get; set; } = default!;
    public string? Description { get; set; } = default!;
}