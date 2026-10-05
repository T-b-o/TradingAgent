extern alias TradingAgentWeb;

using System.Net;
using TradingAgentApiClient = TradingAgentWeb::TradingAgent.Web.Services.TradingAgentApiClient;

namespace TradingAgent.Tests.Web;

/// <summary>
/// Verifies error and cancellation behavior for calls from the web app to the API.
/// </summary>
public sealed class TradingAgentApiClientTests
{
    [Fact]
    public async Task SendChatAsync_WhenApiReturnsResponse_ReturnsSuccess()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            (_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    """{"message":"Market context received.","model":"qwen3:1.7b"}""")
            })))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.True(result.IsSuccess);
        Assert.Equal("Market context received.", result.Value?.Message);
        Assert.Equal("qwen3:1.7b", result.Value?.Model);
    }

    [Fact]
    public async Task SendChatAsync_WhenApiIsUnavailable_ReturnsFailure()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            (_, _) => Task.FromException<HttpResponseMessage>(new HttpRequestException())))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.False(result.IsSuccess);
        Assert.Equal(
            "The TradingAgent API is unavailable. Verify the API is running and reachable.",
            result.ErrorMessage);
    }

    [Fact]
    public async Task SendChatAsync_WhenCallerCancels_PropagatesCancellation()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            async (_, cancellationToken) =>
            {
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);
        using var cancellationSource = new CancellationTokenSource();
        await cancellationSource.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => apiClient.SendChatAsync("Analyse EURUSD", cancellationSource.Token));
    }

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) => sendAsync(request, cancellationToken);
    }
}
