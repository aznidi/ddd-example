using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.Common.Exceptions.Services;
using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Services.Commands.UpdateService;

public sealed class UpdateServiceCommandHandler 
    : IRequestHandler<UpdateServiceCommand, UpdateServiceDto>
{
    private readonly IBillableServiceRepository _repo;

    public UpdateServiceCommandHandler(IBillableServiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<UpdateServiceDto> Handle(
        UpdateServiceCommand request, 
        CancellationToken cancellationToken)
    {
        BillableService? billableService = await _repo.GetByIdOrThrowAsync(
            new ServiceId(request.ServiceId), 
            cancellationToken);

        if (billableService is null)
            throw new ServiceNotFoundException("Service not found.");

        if (request.Name is not null)
            billableService.Rename(request.Name);

        if (request.Description is not null)
        {
            var normalized = string.IsNullOrWhiteSpace(request.Description) 
                ? null 
                : request.Description.Trim();
            billableService.UpdateDescription(normalized);
        }

        var currencyProvided = request.Currency is not null;
        var priceProvided = request.Price.HasValue;

        if (priceProvided || currencyProvided)
        {
            var newAmount = priceProvided ? request.Price!.Value : billableService.Price.Amount;
            var newCurrency = currencyProvided ? request.Currency!.Trim() : billableService.Price.Currency;

            var newMoney = new Money(newAmount, newCurrency);

            if (newMoney.Amount != billableService.Price.Amount ||
                !string.Equals(newMoney.Currency, billableService.Price.Currency, StringComparison.Ordinal))
            {
                billableService.ChangePrice(newMoney);
            }
        }

        await _repo.UpdateAsync(billableService, cancellationToken);

        return new UpdateServiceDto(billableService);
    }
}
