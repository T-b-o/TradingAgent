namespace TradingAgent.Api.Contracts;

/// <summary>
/// Represents a client request to send a message to the local AI agent.
/// </summary>
/// <param name="Message">The user message to analyse.</param>
public sealed record AgentChatRequest(string? Message);