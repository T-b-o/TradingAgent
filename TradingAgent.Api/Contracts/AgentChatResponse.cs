namespace TradingAgent.Api.Contracts;

/// <summary>
/// Represents a local AI agent response returned to an API client.
/// </summary>
/// <param name="Message">The model response text.</param>
/// <param name="Model">The model that generated the response.</param>
public sealed record AgentChatResponse(string Message, string Model);
