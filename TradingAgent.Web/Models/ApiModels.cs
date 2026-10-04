namespace TradingAgent.Web.Models;

public sealed record ApiStatusResponse(string Application, string Status, string Framework);

public sealed record AgentChatResponse(string Message, string Model);

public sealed record ApiResult<T>(bool IsSuccess, T? Value, string? ErrorMessage, int? StatusCode)
{
    public static ApiResult<T> Success(T value) => new(true, value, null, null);

    public static ApiResult<T> Failure(string errorMessage, int? statusCode = null) =>
        new(false, default, errorMessage, statusCode);
}
