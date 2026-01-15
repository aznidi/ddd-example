using MediatR;

namespace SMS.Application.Features.Finance.Engagements.Commands.GenerateEngagementFile;

public sealed record GenerateEngagementFileCommand(Guid EngagementId) : IRequest<byte[]>;