using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagementServices;

public sealed class GetEngagementServicesQueryHandler 
    : IRequestHandler<GetEngagementServicesQuery, List<EngagementLineDto>>
{
    private readonly IEngagementRepository _repo;

    public GetEngagementServicesQueryHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EngagementLineDto>> Handle(
        GetEngagementServicesQuery request, 
        CancellationToken cancellationToken)
    {
        Engagement? engagement = await _repo.GetByIdWithDetailsAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        return engagement.Lines.Select(l => new EngagementLineDto
        {
            EngagementLineId = l.Id.Value.ToString(),
            ServiceId = l.ServiceId.Value.ToString(),
            ServiceNameSnapshot = l.ServiceNameSnapshot,
            PriceSnapshot = new MoneyDto
            {
                Amount = l.PriceSnapshot.Amount,
                Currency = l.PriceSnapshot.Currency
            },
            Quantity = new QuantityDto
            {
                Value = l.Quantity.Value,
                Unit = "mois"
            },
            LineTotal = new MoneyDto
            {
                Amount = l.GetLineTotal().Amount,
                Currency = l.GetLineTotal().Currency
            }
        }).ToList();
    }
}
