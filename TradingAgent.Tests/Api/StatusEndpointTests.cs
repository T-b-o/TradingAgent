using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TradingAgent.Api.Contracts;
using TradingAgent.Application.Ollama;

namespace TradingAgent.Tests.Api;

/// <summary>
/// Verifies the externally observable API foundation endpoints.
/// </summary>
public sealed class StatusEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusEndpointTests"/> class.
    /// </summary>
    /// <param name="factory">The in-memory API host.</param>
    public StatusEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifies that the status endpoint returns the documented running response.
    /// </summary>
    [Fact]
    public async Task GetStatus_ReturnsRunningResponse()
    {
        var response = await _client.GetAsync("/api/status");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<StatusResponse>();

        Assert.NotNull(body);
        Assert.Equal("TradingAgent", body.Application);
        Assert.Equal("Running", body.Status);
        Assert.Equal(".NET 10", body.Framework);
    }

    /// <summary>
    /// Verifies that chat input is rejected before Ollama is called.
    /// </summary>
    [Fact]
    public async Task PostChat_WithEmptyMessage_ReturnsValidationProblem()
    {
        var response = await _client.PostAsJsonAsync("/api/agent/chat", new { message = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Verifies that malformed JSON is rejected as a client error.
    /// </summary>
    [Fact]
    public async Task PostChat_WithMalformedJson_ReturnsBadRequestProblem()
    {
        using var content = new StringContent("{message:}");
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _client.PostAsync("/api/agent/chat", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal("Invalid request", problem.Title);
    }

    /// <summary>
    /// Verifies that an unavailable Ollama service produces a safe ProblemDetails response.
    /// </summary>
    [Fact]
    public async Task PostChat_WhenOllamaIsUnavailable_ReturnsServiceUnavailableProblem()
    {
        using var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IOllamaService>();
                services.AddSingleton<IOllamaService, UnavailableOllamaService>();
            });
        });
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/agent/chat", new { message = "Analyse EURUSD" });

        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal("Local AI service unavailable", problem.Title);
        Assert.Equal("The local AI service could not process the request.", problem.Detail);
    }

    private sealed class UnavailableOllamaService : IOllamaService
    {
        public Task<OllamaChatResponse> ChatAsync(OllamaChatRequest request, CancellationToken cancellationToken) =>
            Task.FromException<OllamaChatResponse>(new OllamaServiceException("Ollama is unavailable."));
    }
}
