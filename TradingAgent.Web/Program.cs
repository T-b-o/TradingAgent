using System.Net;
using TradingAgent.Web.Components;
using TradingAgent.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var requestTimeoutSeconds = builder.Configuration.GetValue<int?>("Api:RequestTimeoutSeconds");
if (requestTimeoutSeconds is null || requestTimeoutSeconds <= 0)
{
    throw new InvalidOperationException("Api:RequestTimeoutSeconds must be configured and greater than zero.");
}

var requestTimeout = TimeSpan.FromSeconds(requestTimeoutSeconds.Value);

builder.Services.AddHttpClient<TradingAgentApiClient>((serviceProvider, client) =>
{
    var baseUrl = builder.Configuration["Api:BaseUrl"];

    if (string.IsNullOrWhiteSpace(baseUrl))
    {
        throw new InvalidOperationException("The TradingAgent API base URL is missing from configuration.");
    }

    client.BaseAddress = new Uri(baseUrl, UriKind.Absolute);
    client.Timeout = requestTimeout;
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();

    if (builder.Environment.IsDevelopment())
    {
        var baseUrl = builder.Configuration["Api:BaseUrl"];
        if (Uri.TryCreate(baseUrl, UriKind.Absolute, out var apiUri) && IsLoopbackHost(apiUri.Host))
        {
            // Local development only: allow the trusted loopback API endpoint to use the dev certificate while keeping production behavior unchanged.
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        }
    }

    return handler;
});

static bool IsLoopbackHost(string host)
{
    if (string.Equals(host, "localhost", StringComparison.OrdinalIgnoreCase))
    {
        return true;
    }

    return IPAddress.TryParse(host, out var address) && IPAddress.IsLoopback(address);
}

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
