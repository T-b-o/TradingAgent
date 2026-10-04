namespace TradingAgent.Api.Contracts;

/// <summary>
/// Represents the running API status exposed to clients.
/// </summary>
/// <param name="Application">The application name.</param>
/// <param name="Status">The current service status.</param>
/// <param name="Framework">The framework target.</param>
public sealed record StatusResponse(string Application, string Status, string Framework);
