using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingAgent.Application.Ollama;

namespace TradingAgent.Infrastructure.Ollama;

/// <summary>
/// Implements local Ollama chat communication through a managed HTTP client.
/// </summary>
public sealed class OllamaService : IOllamaService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OllamaService> _logger;
    private readonly OllamaOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaService"/> class.
    /// </summary>
    /// <param name="httpClient">The managed client for Ollama requests.</param>
    /// <param name="options">The validated Ollama configuration.</param>
    /// <param name="logger">The logger for integration diagnostics.</param>
    public OllamaService(HttpClient httpClient, IOptions<OllamaOptions> options, ILogger<OllamaService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<OllamaChatResponse> ChatAsync(OllamaChatRequest request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Message);

        var requestBody = new OllamaChatApiRequest(
            _options.Model,
            false,
            [new OllamaChatApiMessage("user", request.Message)]);

        HttpResponseMessage response;

        try
        {
            response = await _httpClient.PostAsJsonAsync("api/chat", requestBody, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Ollama request failed before a response was received.");
            throw new OllamaServiceException("The local Ollama service could not be reached.", exception);
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(exception, "Ollama request timed out.");
            throw new OllamaServiceException("The local Ollama service did not respond before the configured timeout.", exception);
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Ollama returned unsuccessful status code {StatusCode}.",
                    (int)response.StatusCode);
                throw new OllamaServiceException("The local Ollama service returned an unsuccessful response.");
            }

            try
            {
                var responseBody = await response.Content.ReadFromJsonAsync<OllamaChatApiResponse>(cancellationToken);

                if (string.IsNullOrWhiteSpace(responseBody?.Message?.Content))
                {
                    throw new OllamaServiceException("The local Ollama service returned an invalid response.");
                }

                return new OllamaChatResponse(
                    responseBody.Message.Content,
                    string.IsNullOrWhiteSpace(responseBody.Model) ? _options.Model : responseBody.Model);
            }
            catch (OllamaServiceException)
            {
                throw;
            }
            catch (Exception exception) when (exception is NotSupportedException or System.Text.Json.JsonException)
            {
                _logger.LogError(exception, "Ollama returned a response that could not be parsed.");
                throw new OllamaServiceException("The local Ollama service returned an invalid response.", exception);
            }
        }
    }

    private sealed record OllamaChatApiRequest(string Model, bool Stream, IReadOnlyList<OllamaChatApiMessage> Messages);

    private sealed record OllamaChatApiMessage(string Role, string Content);

    private sealed record OllamaChatApiResponse(string? Model, OllamaChatApiMessage? Message);
}
