using TradingAgent.Api.Contracts;
using TradingAgent.Application.Ollama;

namespace TradingAgent.Api.Endpoints;

/// <summary>
/// Maps endpoints that expose the local AI agent through the API boundary.
/// </summary>
public static class AgentEndpoints
{
    /// <summary>
    /// Maps the local AI chat endpoint.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <returns>The endpoint route builder.</returns>
    public static IEndpointRouteBuilder MapAgentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/agent/chat", HandleChatAsync)
            .WithName("ChatWithAgent")
            .WithSummary("Sends a message to the configured local Ollama model.")
            .Produces<AgentChatResponse>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status503ServiceUnavailable);

        return endpoints;
    }

    private static async Task<IResult> HandleChatAsync(
        AgentChatRequest request,
        IOllamaService ollamaService,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.Message)] = ["A non-empty message is required."]
            });
        }

        var response = await ollamaService.ChatAsync(
            new OllamaChatRequest(request.Message),
            cancellationToken);

        return TypedResults.Ok(new AgentChatResponse(response.Message, response.Model));
    }
}
