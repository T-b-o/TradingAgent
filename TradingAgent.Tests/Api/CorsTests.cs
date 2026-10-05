using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TradingAgent.Tests.Api;

/// <summary>
/// Verifies that the API explicitly allows the configured browser origin for CORS preflight requests.
/// </summary>
public sealed class CorsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CorsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task OptionsRequest_FromConfiguredOrigin_ReturnsCorsHeaders()
    {
        using var request = new HttpRequestMessage(HttpMethod.Options, "/api/agent/chat");
        request.Headers.Add("Origin", "https://localhost:7084");
        request.Headers.Add("Access-Control-Request-Method", "POST");
        request.Headers.Add("Access-Control-Request-Headers", "content-type");

        using var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal("https://localhost:7084", response.Headers.GetValues("Access-Control-Allow-Origin").Single());
        Assert.Contains("POST", response.Headers.GetValues("Access-Control-Allow-Methods").Single());
        Assert.Contains("content-type", response.Headers.GetValues("Access-Control-Allow-Headers").Single(), StringComparison.OrdinalIgnoreCase);
    }
}
