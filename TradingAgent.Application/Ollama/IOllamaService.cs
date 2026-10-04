namespace TradingAgent.Application.Ollama;

/// <summary>
/// Defines the application boundary for local Ollama chat interactions.
/// </summary>
public interface IOllamaService
{
    /// <summary>
    /// Sends a user message to the configured local Ollama model.
    /// </summary>
    /// <param name="request">The validated chat request.</param>
    /// <param name="cancellationToken">Cancels the outbound request.</param>
    /// <returns>The model response.</returns>
    Task<OllamaChatResponse> ChatAsync(OllamaChatRequest request, CancellationToken cancellationToken);
}
