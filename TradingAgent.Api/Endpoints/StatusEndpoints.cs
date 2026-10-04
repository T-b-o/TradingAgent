using TradingAgent.Api.Contracts;

namespace TradingAgent.Api.Endpoints;

/// <summary>
/// Maps endpoints that report the API status.
/// </summary>
public static class StatusEndpoints
{
    /// <summary>
    /// Maps the API status endpoint.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointRouteBuilder MapStatusEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
                "/api/status",
                () => TypedResults.Ok(new StatusResponse("TradingAgent", "Running", ".NET 10")))
            .WithName("GetApiStatus")
            .WithSummary("Reports whether the TradingAgent API is running.")
            .Produces<StatusResponse>();

        return endpoints;
    }
}
