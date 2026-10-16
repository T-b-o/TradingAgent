using TradingAgent.Web.Components;
using TradingAgent.Web.Services;

var builder = WebApplication.CreateBuilder(args);

var requestTimeoutSeconds = builder.Configuration.GetValue<int?>("Api:RequestTimeoutSeconds") ?? 180;
var requestTimeout = requestTimeoutSeconds > 0
    ? TimeSpan.FromSeconds(requestTimeoutSeconds)
    : TimeSpan.FromSeconds(180);

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
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    }

    return handler;
});

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
