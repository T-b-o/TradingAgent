using System.Net.Http.Json;
using System.Text.Json;
using TradingAgent.Web.Models;

namespace TradingAgent.Web.Services;

public sealed class TradingAgentApiClient
{
    private readonly HttpClient _httpClient;

    public TradingAgentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResult<ApiStatusResponse>> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return await SendAsync<ApiStatusResponse>(
            () => _httpClient.GetAsync("/api/status", cancellationToken),
            cancellationToken);
    }

    public async Task<ApiResult<AgentChatResponse>> SendChatAsync(string message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return ApiResult<AgentChatResponse>.Failure("A non-empty message is required.");
        }

        var payload = new { message };
        return await SendAsync<AgentChatResponse>(
            () => _httpClient.PostAsJsonAsync("/api/agent/chat", payload, cancellationToken),
            cancellationToken);
    }

    private async Task<ApiResult<T>> SendAsync<T>(
        Func<Task<HttpResponseMessage>> requestFactory,
        CancellationToken cancellationToken)
    {
        try
        {
            using var response = await requestFactory();
            return await ReadResponseAsync<T>(response, cancellationToken);
        }
        catch (HttpRequestException)
        {
            return ApiResult<T>.Failure("The TradingAgent API is unavailable. Verify the API is running and reachable.");
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return ApiResult<T>.Failure("The request to the TradingAgent API timed out.");
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
