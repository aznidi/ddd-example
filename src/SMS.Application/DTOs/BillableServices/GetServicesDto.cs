namespace SMS.Application.DTOs.BillableServices;

public sealed class GetServicesDto
{
    public string ServiceId {  get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string Currency { get; set; } = default!;

}