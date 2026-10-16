using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TradingAgent.Web.Models;

namespace TradingAgent.Web.Services;

/// <summary>
/// Calls the TradingAgent API and normalizes HTTP, timeout, and availability failures into stable UI messages.
/// </summary>
public sealed class TradingAgentApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TradingAgentApiClient> _logger;

    public TradingAgentApiClient(HttpClient httpClient, ILogger<TradingAgentApiClient>? logger = null)
    {
        _httpClient = httpClient;
        _logger = logger ?? NullLogger<TradingAgentApiClient>.Instance;
    }

    /// <summary>
    /// Queries the API health endpoint and normalizes connectivity and HTTP errors.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for the in-flight request.</param>
    /// <returns>The status result, including a user-friendly error when the API is unreachable.</returns>
    public async Task<ApiResult<ApiStatusResponse>> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return await SendAsync<ApiStatusResponse>(
            nameof(GetStatusAsync),
            () => _httpClient.GetAsync("/api/status", cancellationToken),
            cancellationToken);
    }

    /// <summary>
    /// Sends a chat message to the local TradingAgent API and returns a normalized result.
    /// </summary>
    /// <param name="message">The message to submit to the API.</param>
    /// <param name="cancellationToken">The cancellation token for the request.</param>
    /// <returns>The chat result, including timeout and connectivity failures.</returns>
    public async Task<ApiResult<AgentChatResponse>> SendChatAsync(string message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return ApiResult<AgentChatResponse>.Failure("A non-empty message is required.");
        }

        var payload = new { message };
        return await SendAsync<AgentChatResponse>(
            nameof(SendChatAsync),
            () => _httpClient.PostAsJsonAsync("/api/agent/chat", payload, cancellationToken),
            cancellationToken);
    }

    private async Task<ApiResult<T>> SendAsync<T>(
        string operationName,
        Func<Task<HttpResponseMessage>> requestFactory,
        CancellationToken cancellationToken)
    {
        try
        {
            using var response = await requestFactory();
            return await ReadResponseAsync<T>(response, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(
                exception,
                "TradingAgent API request failed while {Operation}. BaseAddress={BaseAddress}. HttpRequestError={HttpRequestError}.",
                operationName,
                _httpClient.BaseAddress,
                exception.HttpRequestError);

            return ApiResult<T>.Failure("The TradingAgent API is unavailable. Verify the API is running and reachable.");
        }
        catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning(
                exception,
                "TradingAgent API request timed out while {Operation}. BaseAddress={BaseAddress}.",
                operationName,
                _httpClient.BaseAddress);

            return ApiResult<T>.Failure("The request to the TradingAgent API timed out.");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
    }

    private static async Task<ApiResult<T>> ReadResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var content = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);

                return content is null
                    ? ApiResult<T>.Failure("The API returned an empty response.")
                    : ApiResult<T>.Success(content);
            }
            catch (NotSupportedException)
            {
                return ApiResult<T>.Failure("The API returned an unsupported payload format.");
            }
            catch (JsonException)
            {
                return ApiResult<T>.Failure("The API returned a malformed response.");
            }
        }

        var reason = await ReadProblemMessageAsync(response, cancellationToken);
        return ApiResult<T>.Failure(reason, (int)response.StatusCode);
    }

    private static async Task<string> ReadProblemMessageAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(raw))
        {
            return $"The API returned an HTTP {(int)response.StatusCode} status.";
        }

        try
        {
            using var document = JsonDocument.Parse(raw);
            var root = document.RootElement;

            if (root.TryGetProperty("detail", out var detail) && detail.ValueKind == JsonValueKind.String)
            {
                return detail.GetString() ?? $"The API returned an HTTP {(int)response.StatusCode} status.";
            }

            if (root.TryGetProperty("title", out var title) && title.ValueKind == JsonValueKind.String)
            {
                return title.GetString() ?? $"The API returned an HTTP {(int)response.StatusCode} status.";
            }
        }
        catch (JsonException)
        {
            // Ignore malformed payloads and fall back to a generic message.
        }

        return $"The API returned an HTTP {(int)response.StatusCode} status.";
    }
}
