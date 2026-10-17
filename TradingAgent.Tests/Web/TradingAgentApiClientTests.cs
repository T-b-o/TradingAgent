extern alias TradingAgentWeb;

using System.Net;
using TradingAgentApiClient = TradingAgentWeb::TradingAgent.Web.Services.TradingAgentApiClient;
using ApiFailureKind = TradingAgentWeb::TradingAgent.Web.Models.ApiFailureKind;

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
    public async Task SendChatAsync_WhenApiReturns503ProblemDetails_UsesDetailAndStatusCode()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            (_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent("""{"detail":"Local AI service unavailable."}""")
            })))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.False(result.IsSuccess);
        Assert.Equal("Local AI service unavailable.", result.ErrorMessage);
        Assert.Equal((int)HttpStatusCode.ServiceUnavailable, result.StatusCode);
        Assert.Equal(ApiFailureKind.HttpStatus, result.FailureKind);
    }

    [Fact]
    public async Task SendChatAsync_WhenApiReturns4xxWithEmptyBody_UsesGenericStatusMessage()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            (_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(string.Empty)
            })))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.False(result.IsSuccess);
        Assert.Equal("The API returned an HTTP 400 status.", result.ErrorMessage);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ApiFailureKind.HttpStatus, result.FailureKind);
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
        Assert.Equal(ApiFailureKind.HttpRequest, result.FailureKind);
    }

    [Fact]
    public async Task SendChatAsync_WhenApiReturnsMalformedJson_ReturnsMalformedResponseFailure()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            (_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{")
            })))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.False(result.IsSuccess);
        Assert.Equal("The API returned a malformed response.", result.ErrorMessage);
        Assert.Equal(ApiFailureKind.MalformedResponse, result.FailureKind);
    }

    [Fact]
    public async Task SendChatAsync_WhenRequestTimesOut_ReturnsFailure()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            async (_, cancellationToken) =>
            {
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }))
        {
            BaseAddress = new Uri("https://localhost/"),
            Timeout = TimeSpan.FromMilliseconds(50)
        };
        var apiClient = new TradingAgentApiClient(httpClient);

        var result = await apiClient.SendChatAsync("Analyse EURUSD");

        Assert.False(result.IsSuccess);
        Assert.Equal("The request to the TradingAgent API timed out.", result.ErrorMessage);
        Assert.Equal(ApiFailureKind.Timeout, result.FailureKind);
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
