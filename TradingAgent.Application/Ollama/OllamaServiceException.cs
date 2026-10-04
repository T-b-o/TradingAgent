namespace TradingAgent.Application.Ollama;

/// <summary>
/// Represents an unavailable or invalid response from the local Ollama service.
/// </summary>
public sealed class OllamaServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OllamaServiceException"/> class.
    /// </summary>
    /// <param name="message">The diagnostic error message.</param>
    /// <param name="innerException">The underlying integration exception, when available.</param>
    public OllamaServiceException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
