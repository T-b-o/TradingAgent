using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using TradingAgent.Api.Endpoints;
using TradingAgent.Api.ErrorHandling;
using TradingAgent.Application.Ollama;
using TradingAgent.Infrastructure.Ollama;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddOptions<OllamaOptions>()
    .BindConfiguration(OllamaOptions.SectionName)
    .ValidateOnStart();
builder.Services.AddSingleton<IValidateOptions<OllamaOptions>, OllamaOptionsValidator>();

builder.Services.AddHttpClient<IOllamaService, OllamaService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<OllamaOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl, UriKind.Absolute);
    client.Timeout = options.Timeout;
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapStatusEndpoints();
app.MapAgentEndpoints();

app.Run();

/// <summary>
/// Exposes the entry point to ASP.NET Core integration tests.
/// </summary>
public partial class Program;

