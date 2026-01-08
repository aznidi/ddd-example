using SMS.Application.Common.Abstractions.Persistence;
using SMS.Application.Common.Results;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Modules.Finance.Features.Engagements.Create;

public sealed class CreateEngagementHandler
{
    private readonly IStudentRepository _studentRepository;
    private readonly IEngagementRepository _engagementRepository;

    public CreateEngagementHandler(
        IStudentRepository studentRepository,
        IEngagementRepository engagementRepository)
    {
        _studentRepository = studentRepository;
        _engagementRepository = engagementRepository;
    }

    public async Task<Result<CreateEngagementResponse>> Handle(
        CreateEngagementCommand command,
        CancellationToken ct = default)
    {
        var studentId = new StudentId(command.StudentId);

        var student = await _studentRepository.GetByIdAsync(studentId, ct);
        if (student is null)
            return Result<CreateEngagementResponse>.Fail(
                Error.NotFound("Student not found.")
            );

        var engagementId = new EngagementId(Guid.NewGuid());

        var engagement = new Engagement(
            id: engagementId,
            studentId: studentId
        );

        await _engagementRepository.AddAsync(engagement, ct);

        return Result<CreateEngagementResponse>.Ok(
            new CreateEngagementResponse(engagementId.Value)
        );
    }
}
