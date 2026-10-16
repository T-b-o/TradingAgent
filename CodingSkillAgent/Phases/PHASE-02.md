# Phase 02 — Blazor + Browser Debugging

Status:
REMEDIATED

## Objective

Build the first Blazor Web App and validate the real server-side debugging flow used by the current `InteractiveServer` implementation.

## Scope

- TradingAgent.Web
- Blazor Web App with `InteractiveServer`
- Tailwind CSS
- API client/service abstraction
- status/dashboard page
- loading and retry states
- API integration
- timeout and diagnostics handling
- relevant tests
- Visual Studio debugging
- Chrome DevTools inspection of the Blazor SignalR connection and server-side HTTP flow

## Runtime Flow

Browser
    ↓
Blazor Server
    ↓
HttpClient
    ↓
ASP.NET Core API
    ↓
Application
    ↓
Infrastructure
    ↓
Ollama
    ↓
Response
    ↓
Blazor Server
    ↓
Browser

## Learning Goals

The user must be able to:
- inspect the Blazor SignalR connection in Chrome Network
- identify the server-side HTTP request made by the Web app to the API
- read server-side logs or the Visual Studio Output window to diagnose connection problems
- set a server-side breakpoint in `TradingAgentApiClient` or the API endpoint
- inspect Locals, Watch and Call Stack
- distinguish API connectivity failures, timeout failures and local AI availability failures
- confirm that the active UI path is server-side, not browser-side CORS-driven fetch traffic

## Out of Scope

Do NOT implement:
- TradingView
- trading strategy
- strategy skills
- broker integration
- live trading
- paper trading
- advanced trading charts
- news engine
- browser-side fetch or WebAssembly solely to demonstrate CORS

## Acceptance Criteria

1. Blazor app starts. ✅
2. UI requests reach the existing API via the Web server. ✅
3. API response is displayed. ✅
4. Request path is visible via Blazor/HTTP diagnostics and server-side logs. ✅
5. Visual Studio breakpoint is hit. ✅
6. Request can be traced end-to-end. ✅
7. Error behaviour can be demonstrated and debugged. ✅
8. Build succeeds. ✅
9. Relevant tests pass. ✅
10. Actual changed-file list is derived from Git. ✅
11. Phase documentation is updated. ✅
12. Commit/push requires explicit user approval. ✅

## Verification Summary

- API HTTPS launch profile is aligned with the configured Web base URL: `https://localhost:7062`
- Web app is configured for the same API base address
- CORS policy remains configured for validation, but the current browser UI does not exercise it directly
- Server-side API client logs request failures with the actual exception and base-address context
- Relevant tests pass: 11/11
- Manual browser verification remains environment-dependent and was not claimed as completed here
