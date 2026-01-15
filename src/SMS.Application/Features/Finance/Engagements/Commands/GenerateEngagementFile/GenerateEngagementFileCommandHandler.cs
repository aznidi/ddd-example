using MediatR;
using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.Common.Services;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Features.Finance.Engagements.Commands.GenerateEngagementFile;

public sealed class GenerateEngagementFileCommandHandler 
    : IRequestHandler<GenerateEngagementFileCommand, byte[]>
{
    private readonly IEngagementRepository _engagementRepo;
    private readonly IStudentRepository _studentRepo;
    private readonly IEngagementPdfService _pdfService;

    public GenerateEngagementFileCommandHandler(
        IEngagementRepository engagementRepo,
        IStudentRepository studentRepo,
        IEngagementPdfService pdfService)
    {
        _engagementRepo = engagementRepo;
        _studentRepo = studentRepo;
        _pdfService = pdfService;
    }

    public async Task<byte[]> Handle(
        GenerateEngagementFileCommand request, 
        CancellationToken cancellationToken)
    {
        // Get engagement with details
        var engagement = await _engagementRepo.GetByIdWithDetailsAsync(
            new EngagementId(request.EngagementId), 
            cancellationToken);

        if (engagement is null)
            throw new InvalidOperationException($"Engagement with ID {request.EngagementId} not found.");

        // Get student information
        var student = await _studentRepo.GetByIdAsync(engagement.StudentId, cancellationToken);

        if (student is null)
            throw new InvalidOperationException($"Student with ID {engagement.StudentId.Value} not found.");

        // Generate PDF using service
        return _pdfService.GenerateEngagementContract(engagement, student);
    }
}