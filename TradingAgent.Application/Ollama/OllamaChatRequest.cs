namespace TradingAgent.Application.Ollama;

/// <summary>
/// Represents a user message sent to the local Ollama model.
/// </summary>
/// <param name="Message">The message for the model to process.</param>
public sealed record OllamaChatRequest(string Message);
