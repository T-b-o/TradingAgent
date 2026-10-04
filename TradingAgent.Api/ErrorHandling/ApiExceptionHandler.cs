using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradingAgent.Application.Ollama;

namespace TradingAgent.Api.ErrorHandling;

/// <summary>
/// Converts unhandled API exceptions into safe ProblemDetails responses.
/// </summary>
public sealed class ApiExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ApiExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger for full server-side diagnostics.</param>
    /// <param name="problemDetailsService">The service used to write API error responses.</param>
    public ApiExceptionHandler(
        ILogger<ApiExceptionHandler> logger,
        IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            BadHttpRequestException => (
                StatusCodes.Status400BadRequest,
                "Invalid request",
                "The request body could not be processed."),
            OllamaServiceException => (
                StatusCodes.Status503ServiceUnavailable,
                "Local AI service unavailable",
                "The local AI service could not process the request."),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Unexpected server error",
                "An unexpected error occurred while processing the request.")
        };

        _logger.LogError(
            exception,
            "Request {RequestMethod} {RequestPath} failed with status code {StatusCode}.",
            httpContext.Request.Method,
            httpContext.Request.Path,
            statusCode);

        httpContext.Response.StatusCode = statusCode;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail
            }
        });
    }
}
