using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.Engagements;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Queries.GetEngagementById;

public sealed class GetEngagementByIdQueryHandler 
    : IRequestHandler<GetEngagementByIdQuery, EngagementDto>
{
    private readonly IEngagementRepository _repo;

    public GetEngagementByIdQueryHandler(IEngagementRepository repo)
    {
        _repo = repo;
    }

    public async Task<EngagementDto> Handle(
        GetEngagementByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // We need to use GetByIdWithDetailsAsync to get Lines and Tranches,
        // then manually map to DTO since repository GetAllAsync returns DTO directly
        // but GetById returns entity
        
        Engagement? engagement = await _repo.GetByIdWithDetailsAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        return new EngagementDto
        {
            EngagementId = engagement.Id.Value.ToString(),
            StudentId = engagement.StudentId.Value.ToString(),
            TotalAmount = new MoneyDto
            {
                Amount = engagement.TotalAmount.Amount,
                Currency = engagement.TotalAmount.Currency
            },
            Lines = engagement.Lines.Select(l => new EngagementLineDto
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
            }).ToList(),
            Tranches = engagement.Tranches.Select(t => new TrancheDto
            {
                TrancheId = t.Id.Value.ToString(),
                DueDate = t.DueDate,
                AmountDue = new MoneyDto
                {
                    Amount = t.AmountDue.Amount,
                    Currency = t.AmountDue.Currency
                },
                Status = t.Status.ToString()
            }).ToList()
        };
    }
}
