using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Application.Modules.Finance.Features.Engagements.Create;

public sealed record CreateEngagementCommand(
    Guid StudentId
);
