using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.DTOs.BillableServices;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Services.Commands.CreateService;

public sealed class CreateServiceCommandHandler 
    : IRequestHandler<CreateServiceCommand, CreateServiceDto>
{
    private readonly IBillableServiceRepository _repo;

    public CreateServiceCommandHandler(IBillableServiceRepository repo)
    {
        _repo = repo;
    }

    public async Task<CreateServiceDto> Handle(
        CreateServiceCommand request, 
        CancellationToken cancellationToken)
    {
        BillableService service = new BillableService(
            id: new ServiceId(Guid.NewGuid()),
            name: request.Name,
            price: new Money(request.Price, request.Currency),
            description: request.Description
        );

        await _repo.AddAsync(service, cancellationToken);

        return new CreateServiceDto(service.Id);
    }
}
