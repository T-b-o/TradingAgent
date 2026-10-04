namespace TradingAgent.Infrastructure.Ollama;

/// <summary>
/// Configures the local Ollama runtime used by the infrastructure adapter.
/// </summary>
public sealed class OllamaOptions
{
    /// <summary>
    /// Gets the configuration section name.
    /// </summary>
    public const string SectionName = "Ollama";

    /// <summary>
    /// Gets or sets the base address of the local Ollama API.
    /// </summary>
    public string BaseUrl { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the Ollama model name.
    /// </summary>
    public string Model { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum duration of an Ollama HTTP request.
    /// </summary>
    public TimeSpan Timeout { get; init; }
}
