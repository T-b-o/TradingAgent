namespace TradingAgent.Application.Ollama;

/// <summary>
/// Represents the text returned by the local Ollama model.
/// </summary>
/// <param name="Message">The model response text.</param>
/// <param name="Model">The model that generated the response.</param>
public sealed record OllamaChatResponse(string Message, string Model);
