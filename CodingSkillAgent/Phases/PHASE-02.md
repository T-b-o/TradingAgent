# Phase 02 — Blazor + Browser Debugging

Status:
REMEDIATED — pending manual verification

## Git-Derived File Inventories

### Remediation commit `21562ef` only

Commit `21562ef2596cdb9e54ea05f1668c58dbc9b3003e` (`Phase 02 Remediation Changes`) recorded these files:

- `.github/Prompts/FOLLOWUP-Phase02-Remediation2-COPILOT-Implementation.prompt.md`
- `.github/Prompts/Phase02-Copilot-Implementation.prompt.md`
- `CodingSkillAgent/ARCHITECTURE.md`
- `CodingSkillAgent/CHANGELOG.md`
- `CodingSkillAgent/Evidence/PHASE-02-REMEDIATION-2-EVIDENCE.md`
- `CodingSkillAgent/Phases/PHASE-02.md`
- `CodingSkillAgent/Reviews/PHASE-02-CLAUDE-REVIEW.md`
- `CodingSkillAgent/STATUS.md`
- `TradingAgent.Tests/Web/TradingAgentApiClientTests.cs`
- `TradingAgent.Web/Components/App.razor`
- `TradingAgent.Web/Components/Layout/MainLayout.razor`
- `TradingAgent.Web/Components/Pages/Chat.razor`
- `TradingAgent.Web/Components/Pages/Error.razor`
- `TradingAgent.Web/Components/Pages/Home.razor`
- `TradingAgent.Web/Models/ApiModels.cs`
- `TradingAgent.Web/Program.cs`
- `TradingAgent.Web/Properties/launchSettings.json`
- `TradingAgent.Web/Services/TradingAgentApiClient.cs`
- `TradingAgent.Web/appsettings.Development.json`
- `TradingAgent.Web/appsettings.json`

Earlier related Web/Phase 02 history is not included in that commit inventory: `1ce6e16` (Phase 02 foundation), `e2890f7` (Phase 02 completion docs), `7045ccd` (Bootstrap-to-Tailwind cleanup), and `214973b` (Phase 02 post-implementation changes). Commit `b379516` contains the subsequent Phase 02 bug corrections.

### Remediation 3 working-tree diff

Remediation 3 changes are separate from the historical Remediation 2 commit. Their exact tracked and untracked inventories are recorded in `CodingSkillAgent/Evidence/PHASE-02-REMEDIATION-3-EVIDENCE.md`; the evidence file itself is not recursively included in its own diff.

## Objective

Build the first Blazor Web App and validate the real server-side debugging flow used by the current `InteractiveServer` implementation.

## Timeout Governance

The Web timeout must remain strictly above the API's `Ollama:Timeout`. The API timeout sources were checked: `TradingAgent.Api/appsettings.json` sets `00:10:00` (600s); `appsettings.Development.json` has no Ollama timeout override; both API launch profiles have no `Ollama__Timeout`; CMD and PowerShell Process/User/Machine environment values are empty. `dotnet user-secrets list --project TradingAgent.Api` could not run because the project has no `UserSecretsId`. Therefore the effective configured value from available sources is 600 seconds, and the Web `Api:RequestTimeoutSeconds` is 660 seconds (11 minutes), strictly greater. The two settings live in separate projects, and nothing enforces this automatically.

## Template Leftovers

Confirmed absent: `Counter.razor`, `Weather.razor`, `NavMenu.razor.css`, and `MainLayout.razor.css` are not tracked or present on disk under `TradingAgent.Web`; no references were found.

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
SignalR
    ↓
Blazor Server
    ↓
HTTP
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

1. Blazor app starts. PENDING MANUAL VERIFICATION
2. UI requests reach the existing API via the Web server. PENDING MANUAL VERIFICATION
3. API response is displayed. PENDING MANUAL VERIFICATION
4. Request path is visible via Blazor/HTTP diagnostics and server-side logs. PENDING MANUAL VERIFICATION
   - [ ] Breakpoint hit in `TradingAgentApiClient` or API endpoint
   - [ ] Chrome DevTools Network shows the Blazor SignalR connection and server-side HTTP flow
   - [ ] Output window or server logs confirm the request path
5. Visual Studio breakpoint is hit. PENDING MANUAL VERIFICATION
6. Request can be traced end-to-end. PENDING MANUAL VERIFICATION
7. Error behaviour can be demonstrated and debugged. PENDING MANUAL VERIFICATION
   - [ ] Retry with API stopped
   - [ ] Chat with Ollama stopped
   - [ ] Reconnect dialog observed
8. Build succeeds. ✅
9. Relevant tests pass. ✅
10. Actual changed-file list is derived from Git. ✅
11. Phase documentation is updated. ✅
12. Commit/push requires explicit user approval (process rule; not yet requested).

## Verification Summary

- API HTTPS launch profile is aligned with the configured Web base URL: `https://localhost:7062`
- Web app is configured for the same API base address
- CORS policy remains configured for validation, but the current browser UI does not exercise it directly
- Server-side API client logs request failures with the actual exception and base-address context
- Full solution Release build succeeds with 0 warnings and 0 errors
- Relevant tests pass: 14/14, 0 failed and 0 skipped
- Manual browser verification remains environment-dependent and was not claimed as completed here
