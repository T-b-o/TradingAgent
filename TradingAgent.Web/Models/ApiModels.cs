namespace TradingAgent.Web.Models;

/// <summary>
/// Describes the normalized category of an API failure for UI handling.
/// </summary>
public enum ApiFailureKind
{
    None,
    HttpRequest,
    Timeout,
    HttpStatus,
    MalformedResponse,
    UnsupportedFormat,
    Validation,
    UserCancelled,
    Unknown
}

/// <summary>
/// Represents the generated status payload returned by the TradingAgent API.
/// </summary>
/// <param name="Application">The application name reported by the API.</param>
/// <param name="Status">The API status text.</param>
/// <param name="Framework">The framework version reported by the API.</param>
public sealed record ApiStatusResponse(string Application, string Status, string Framework);

/// <summary>
/// Represents the AI chat response payload returned by the TradingAgent API.
/// </summary>
/// <param name="Message">The AI-generated message content.</param>
/// <param name="Model">The model name used for the response.</param>
public sealed record AgentChatResponse(string Message, string Model);

/// <summary>
/// Represents the normalized success or failure result of a UI API call.
/// </summary>
/// <typeparam name="T">The successful payload type when the call succeeds.</typeparam>
/// <param name="IsSuccess">Indicates whether the API call succeeded.</param>
/// <param name="Value">The successful payload, when available.</param>
/// <param name="ErrorMessage">The user-friendly failure message, when available.</param>
/// <param name="StatusCode">The HTTP status code returned by the API, when available.</param>
/// <param name="FailureKind">The normalized failure category for the UI.</param>
public sealed record ApiResult<T>(bool IsSuccess, T? Value, string? ErrorMessage, int? StatusCode, ApiFailureKind FailureKind = ApiFailureKind.None)
{
    /// <summary>
    /// Creates a successful API result.
    /// </summary>
    public static ApiResult<T> Success(T value) => new(true, value, null, null, ApiFailureKind.None);

    /// <summary>
    /// Creates a failed API result.
    /// </summary>
    public static ApiResult<T> Failure(string errorMessage, int? statusCode = null, ApiFailureKind failureKind = ApiFailureKind.Unknown) =>
        new(false, default, errorMessage, statusCode, failureKind);
}
