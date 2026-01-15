using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.Exceptions.Engagement;
using SMS.Domain.Modules.Finance.ValueObjects;
using SMS.Domain.Modules.Finance.ValueObjects.ServiceQuantity;

namespace SMS.Application.Features.Finance.Engagements.Commands.CreateEngagement;

public sealed class CreateEngagementCommandHandler 
    : IRequestHandler<CreateEngagementCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IBillableServiceRepository _serviceRepository;
    private readonly IEngagementRepository _engagementRepository;

    public CreateEngagementCommandHandler(
        IStudentRepository studentRepository,
        IBillableServiceRepository serviceRepository,
        IEngagementRepository engagementRepository)
    {
        _studentRepository = studentRepository;
        _serviceRepository = serviceRepository;
        _engagementRepository = engagementRepository;
    }

    public async Task<Guid> Handle(
        CreateEngagementCommand request, 
        CancellationToken cancellationToken)
    {
        var studentId = new StudentId(request.StudentId);

        // Validate student exists
        var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"Student with ID {request.StudentId} not found.");

        // Check if student already has an engagement
        var existingEngagements = await _engagementRepository.GetByStudentIdAsync(
            studentId, 
            cancellationToken);

        if (existingEngagements.Any())
            throw new StudentAlreadyHasEngagementException(
                $"Student with ID {request.StudentId} already has an active engagement.");

        // Create engagement
        var engagement = new Engagement(
            id: new EngagementId(Guid.NewGuid()),
            studentId: studentId
        );

        // Add each service to engagement
        foreach (var serviceRequest in request.Services)
        {
            var service = await _serviceRepository.GetByIdAsync(
                new ServiceId(serviceRequest.ServiceId),
                cancellationToken);

            if (service is null)
                throw new InvalidOperationException($"Service with ID {serviceRequest.ServiceId} not found.");

            engagement.AddService(
                serviceId: service.Id,
                serviceNameSnapshot: service.Name,
                priceSnapshot: service.Price,
                quantity: new Quantity(serviceRequest.Quantity)
            );
        }

        // Parse payment plan and generate tranches
        if (!Enum.TryParse<PaymentPlan>(request.PaymentPlan, true, out var paymentPlan))
            throw new ArgumentException($"Invalid payment plan: {request.PaymentPlan}");

        engagement.GenerateTranches(paymentPlan, request.FirstDueDate);

        // Save engagement
        await _engagementRepository.AddAsync(engagement, cancellationToken);

        return engagement.Id.Value;
    }
}

