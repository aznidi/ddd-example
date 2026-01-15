using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Modules.Finance.Exceptions.Engagement;
using System.Net;

namespace SMS.Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var (statusCode, title, detail) = exception switch
        {
            DuplicateEngagementServiceException ex => (
                StatusCodes.Status400BadRequest,
                "Duplicate Service",
                ex.Message
            ),
            StudentAlreadyHasEngagementException ex => (
                StatusCodes.Status400BadRequest,
                "Student Already Has Engagement",
                ex.Message
            ),
            ServiceNotFoundInEngagementException ex => (
                StatusCodes.Status404NotFound,
                "Service Not Found",
                ex.Message
            ),
            FailedToGenerateTranches ex => (
                StatusCodes.Status400BadRequest,
                "Invalid Operation",
                ex.Message
            ),
            InvalidOperationException ex when ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase) => (
                StatusCodes.Status404NotFound,
                "Resource Not Found",
                ex.Message
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred."
            )
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
