using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.Common.Exceptions.Services;
using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Commands.BillableServices;

public sealed class UpdateServiceCommand
{
    private IBillableServiceRepository _repo;

    public UpdateServiceCommand (
        IBillableServiceRepository billableServiceRepository
    )
    {
        _repo = billableServiceRepository;
    }

    public async Task<UpdateServiceDto> UpdateService(
        string serviceId,
        string? name,
        decimal? price,
        string? currency,
        string? description,
        CancellationToken ct
    )
    {
        if (!Guid.TryParse(serviceId, out var id))
            throw new ArgumentException("Invalid serviceId.", nameof(serviceId));

        BillableService? billableService = await _repo.GetByIdOrThrowAsync(new ServiceId(id), ct);

        if (billableService is null)
            throw new ServiceNotFoundException("Service not found.");

        if (name is not null)
            billableService.Rename(name);

        if (description is not null)
        {
            var normalized = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            billableService.UpdateDescription(normalized);
        }

        var currencyProvided = currency is not null;
        var priceProvided = price.HasValue;

        if (priceProvided || currencyProvided)
        {
            var newAmount = priceProvided ? price!.Value : billableService.Price.Amount;
            var newCurrency = currencyProvided ? currency!.Trim() : billableService.Price.Currency;

            var newMoney = new Money(newAmount, newCurrency);

            if (newMoney.Amount != billableService.Price.Amount ||
                !string.Equals(newMoney.Currency, billableService.Price.Currency, StringComparison.Ordinal))
            {
                billableService.ChangePrice(newMoney);
            }
        }

        await _repo.UpdateAsync(billableService, ct);

        return new UpdateServiceDto(billableService);
    }
}