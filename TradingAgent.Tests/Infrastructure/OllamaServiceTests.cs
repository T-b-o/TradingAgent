using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using TradingAgent.Application.Ollama;
using TradingAgent.Infrastructure.Ollama;

namespace TradingAgent.Tests.Infrastructure;

/// <summary>
/// Verifies the Ollama HTTP adapter without requiring a running Ollama process.
/// </summary>
public sealed class OllamaServiceTests
{
    /// <summary>
    /// Verifies that the adapter sends the configured model and returns the model response.
    /// </summary>
    [Fact]
    public async Task ChatAsync_SendsConfiguredModelAndReturnsResponse()
    {
        string? requestJson = null;
        var handler = new StubHttpMessageHandler(async (request, cancellationToken) =>
        {
            requestJson = await request.Content!.ReadAsStringAsync(cancellationToken);

            return CreateJsonResponse("""
                { "model": "qwen3:1.7b", "message": { "role": "assistant", "content": "Market context received." } }
                """);
        });
        using var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:11434/")
        };
        var service = CreateService(client);

        var response = await service.ChatAsync(new OllamaChatRequest("Analyse EURUSD"), CancellationToken.None);

        Assert.Equal("Market context received.", response.Message);
        Assert.Equal("qwen3:1.7b", response.Model);
        Assert.NotNull(requestJson);

        using var document = JsonDocument.Parse(requestJson);
        Assert.Equal("qwen3:1.7b", document.RootElement.GetProperty("model").GetString());
        Assert.False(document.RootElement.GetProperty("stream").GetBoolean());
    }

    /// <summary>
    /// Verifies that an unsuccessful Ollama response becomes an application-safe exception.
    /// </summary>
    [Fact]
    public async Task ChatAsync_WhenOllamaReturnsFailure_ThrowsOllamaServiceException()
    {
        var handler = new StubHttpMessageHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)));
        using var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://localhost:11434/")
        };
        var service = CreateService(client);

        await Assert.ThrowsAsync<OllamaServiceException>(
            () => service.ChatAsync(new OllamaChatRequest("Analyse EURUSD"), CancellationToken.None));
    }

    private static OllamaService CreateService(HttpClient client)
    {
        var options = Options.Create(new OllamaOptions
        {
            BaseUrl = "http://localhost:11434/",
            Model = "qwen3:1.7b",
            Timeout = TimeSpan.FromMinutes(2)
        });

        return new OllamaService(client, options, NullLogger<OllamaService>.Instance);
    }

    private static HttpResponseMessage CreateJsonResponse(string content) =>
        new(HttpStatusCode.OK)
        {
            Content = new StringContent(content)
        };

    private sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;

        public StubHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
        {
            _handler = handler;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) => _handler(request, cancellationToken);
    }
}
