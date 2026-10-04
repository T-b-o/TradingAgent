# Phase 02 — Claude Review

Scope: Blazor Web App + Tailwind CSS + API integration + browser debugging.

Evidence base: supplied repository documents and Phase 01 source. No build or tests were run by the reviewer.

---

## 1. Executive Summary

- **Phase 01 layering matches `ARCHITECTURE.md`.** Domain has no references, Application references Domain, and Infrastructure references Application and Domain. Api references Application and Infrastructure as the composition root. The Phase 01 build result is user-reported only.
- **Blocker:** the DTOs Blazor needs (`StatusResponse`, `AgentChatRequest`, `AgentChatResponse`) live inside `TradingAgent.Api`.
- **Render mode decides whether acceptance criterion 4 is achievable.** With Interactive Server, the API call is not visible in Chrome Network.
- **`/api/status` never reaches Application or Infrastructure.** An end-to-end trace through the layers needs `/api/agent/chat`.
- **Phase Impact: Modify plan** before implementation starts.

---

## 2. Findings

| Render mode | API call visible in Chrome | CORS needed | Note |
|---|---|---|---|
| Interactive Server | No (only the SignalR socket) | No | Debug via Visual Studio only |
| Interactive WebAssembly | Yes | Yes | The browser talks to the API directly |
| Auto | Depends on the phase | Yes | Two runtime paths, so harder to debug |

- **Contracts:** the three records are in `TradingAgent.Api/Contracts/`. A Web reference to Api pulls in the Web SDK and `Program`. `StatusEndpointTests` also imports `TradingAgent.Api.Contracts`.
- **CORS:** `Program.cs` configures none, so WASM calls would fail at preflight.
- **HTTPS redirect:** `app.UseHttpsRedirection()` combined with the `http` profile (port 5153) means a Web client targeting HTTP gets a redirect. The Web client should use `https://localhost:7062` (`launchSettings.json`).
- **Two error shapes:** `ApiExceptionHandler` returns `ProblemDetails`. The empty-message check in `AgentEndpoints` returns `ValidationProblem`. The Web client must handle both, plus the API-down case with no body.
- **Timeouts:** the Ollama timeout is 2 minutes (`appsettings.json`). The default `HttpClient` timeout is 100 seconds, so a Web client would time out first on a CPU-bound `qwen3:1.7b` call and show a misleading error.
- **Status is not health:** `/api/status` returns "Running" without checking Ollama, so a green dashboard proves nothing about the AI path.
- **Tailwind:** the repo has no Node tooling (`node_modules/` is git-ignored). R0 fits the Tailwind standalone CLI, but its build integration is undefined.
- **Documentation gaps:** `.github/copilot-instructions.md` still says "Blazor later / Tailwind later". `PHASES.md` is referenced but was not in the supplied context. Both `Reviews/*.md` files were empty.

---

## 3. Risks

- **Contract drift or boundary breach:** duplicated DTOs diverge silently, or Web references Api and violates the "UI must not own logic" boundary.
- **Learning-goal mismatch:** Interactive Server fails the Chrome Network goal. WASM exposes the API directly to the browser with no auth, which is acceptable locally but must be revisited before trade proposals or approval UI. CORS must be an allow-listed origin, never a wildcard.
- **Misleading failures:** timeout ordering, API-down, and the two error shapes can surface as the same generic error in the UI.
- **Build coupling:** a Tailwind step in MSBuild can break `dotnet build` or tests on a machine without the CLI. The generated CSS commit policy is undefined.
- **False confidence:** a green status page is mistaken for a working AI path.

---

## 4. Recommendation

1. Create `TradingAgent.Contracts` (net10.0, no dependencies). Move the three records there, update namespaces and test usings, and reference it from Api and Web. Web must not reference Api, Application, or Infrastructure.
2. Add a typed API client in Web via `AddHttpClient`, with the base URL from configuration. It returns a result type (success, or failure with status and problem title) and never throws to the UI. Set its timeout above Ollama's.
3. Use Interactive WebAssembly for the dashboard. Add a named CORS policy in Api that allow-lists the Web origin from configuration, Development only.
4. Use Visual Studio multi-project startup (Api + Web, HTTPS profiles). Document the ports in `PHASE-02.md`.
5. Use the pinned Tailwind standalone CLI, with no Node dependency. Commit the input CSS, and decide explicitly whether the generated output is committed or built.
6. Tests:
   - Client unit tests using the existing `StubHttpMessageHandler` pattern (200, 503 ProblemDetails, 400 validation, connection failure, timeout).
   - bUnit tests (free) for the loading, error, and success states.
   - One `WebApplicationFactory` test for the CORS header.
7. Debugging exercises: success trace, API stopped (error state), Ollama stopped (503 via chat), and a server breakpoint in `HandleChatAsync` followed by Step Into through `OllamaService`. Update `copilot-instructions.md` and supply `PHASES.md`.

---

## 5. Decision Required

1. **Render mode:** WebAssembly (recommended, since it satisfies criterion 4) or Interactive Server (simpler, but Chrome Network will show only the socket).
2. **Contracts:** shared `TradingAgent.Contracts` project (recommended) or Web-owned DTOs with contract tests.
3. **Scope of API integration:** include a minimal chat call (single message, no history or streaming) so criteria 6 and 7 trace through Application, Infrastructure, and Ollama (recommended). Otherwise, status only, and those criteria are reworded to stop at the endpoint.

---

## 6. Phase Impact

**Modify plan.** Resolve the three decisions above, then proceed. There are no Phase 01 issues that require stopping.