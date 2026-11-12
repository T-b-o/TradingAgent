# TradingAgent — Current Status

## Current Phase

PHASE 03 — OWNER DECISIONS RECORDED; IMPLEMENTATION NOT AUTHORISED.

Phase 02 remains accepted (24 October 2026). The repository reflects the
approved `InteractiveServer` architecture: the Web app calls the API through
the server-side `TradingAgentApiClient`. Phase 03 is active, but the project
remains in a documentation and decision-recording state. No Phase 03 code,
dependency, or configuration change is authorised.

---

## Environment

- Visual Studio 2026
- .NET SDK 10.0.401
- Ollama 0.32.15
- qwen3:1.7b
- AMD Ryzen 5 3400G
- 16 GB RAM
- integrated AMD Vega graphics

---

## Verified Results

Build:
PASS — fresh 24 October 2026 Release build of `TradingAgent.slnx`; 0 warnings, 0 errors.

Tests:
PASS — fresh 24 October 2026 Release test run of `TradingAgent.slnx`; 14 passed, 0 failed, 0 skipped.

Launch-profile consistency:
CHECKED — API HTTPS profile is configured for `https://localhost:7062` and the Web app base URL is configured to the same HTTPS address. The actual current configuration is aligned.

Architecture behaviour:
CONFIRMED — the active UI path uses `InteractiveServer` and the browser does not directly call the API. The CORS policy is still present and tested, but it is not the current browser runtime path.

---

## Phase 02 Scope

- TradingAgent.Web Blazor app
- Tailwind CSS styling
- server-side API client abstraction for UI calls
- status dashboard page with retry recovery
- AI chat page with timeout and failure handling
- configured HTTP timeout for API calls
- debugging flow for server-side request diagnosis
- CORS policy validation test that reflects the configured policy, not the actual browser path

---

## Phase 03 Decisions

- JSON persistence selected for the approved Phase 03 scope using BCL `System.Text.Json`.
- Existing C# orchestration selected; Semantic Kernel remains deferred.
- P03.4a/P03.4b approved as an architectural split.
- Additive Ollama adapter work remains subject to separate implementation approval.
- The orchestrator skill has been updated to reflect the approved decisions and review gate.
- No implementation is authorised until a scoped Copilot prompt is explicitly approved.

---

## Not Yet Implemented

- user's trading strategy
- AI trading skill system
- real market data
- TradingView
- broker integration
- paper trading
- live trading
- agent runtime/tool registry foundation
- persistent memory, versioned trading knowledge, and evidence engine
- JSON persistence implementation, P03.4 contracts, and P03.4b store code

---

## Development Workflow

Claude Desktop:
- architecture
- planning
- review
- difficult debugging analysis

GitHub Copilot:
- implementation
- refactoring
- tests
- debugging
- actual repository changes

ChatGPT:
- learning
- architecture review
- debugging coaching
- knowledge checks
- documentation reconciliation and decision recording

Ollama:
- actual local AI runtime

---

## Git Policy

The repository is hosted on GitHub.

AI assistants may change local files.

AI assistants MUST NOT:
- commit
- push
- merge
- create releases

without explicit user approval.

---

## Completed Manual Verification

- [x] The Blazor app rendered and was interactive.
- [x] The UI reached the API through the Web server and displayed API responses.
- [x] Breakpoints in the Web/API request path were hit.
- [x] Chrome DevTools showed the `_blazor` SignalR WebSocket and no browser-side `/api/*` Fetch/XHR traffic, as expected for `InteractiveServer`.
- [x] With the API stopped, the UI reported Unavailable and recovered after Retry when the API restarted.
- [x] With Ollama stopped, chat showed the safe local-AI failure; it succeeded again after Ollama restarted.
- [x] The in-flight chat empty state cleared while the request was pending, and the 660-second timeout hint displayed.
- [x] With only `TradingAgent.Web` stopped, the existing browser tab displayed the styled “Rejoining the server...” dialog.

## Next Gate

The project documents are being reconciled against the Phase 03 owner decisions. The next implementation gate remains a narrowly scoped, ChatGPT-approved task that follows the approved JSON persistence, C# orchestration, and P03.4 split decisions.
