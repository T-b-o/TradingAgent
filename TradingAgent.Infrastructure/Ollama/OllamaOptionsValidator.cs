using Microsoft.Extensions.Options;

namespace TradingAgent.Infrastructure.Ollama;

/// <summary>
/// Validates the configuration required for a local Ollama connection.
/// </summary>
public sealed class OllamaOptionsValidator : IValidateOptions<OllamaOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, OllamaOptions options)
    {
        var failures = new List<string>();

        if (!Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var baseUri) ||
            (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps))
        {
            failures.Add("Ollama:BaseUrl must be an absolute HTTP or HTTPS URL.");
        }

        if (string.IsNullOrWhiteSpace(options.Model))
        {
            failures.Add("Ollama:Model is required.");
        }

        if (options.Timeout <= TimeSpan.Zero)
        {
            failures.Add("Ollama:Timeout must be greater than zero.");
        }

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }
}
