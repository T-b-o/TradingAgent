# TradingAgent — Current Status

## Current Phase

Phase 02 remediation and verification.

The repository currently reflects the approved `InteractiveServer` architecture and the Web app calls the API through the server-side `TradingAgentApiClient`. The codebase has been updated to improve retry handling, timeout diagnostics, and structured error logging, and the relevant tests pass.

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
PASS via `dotnet test` on the existing test project, which builds the solution projects.

Tests:
PASS — 11 passed, 0 failed, 0 skipped.

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

## Not Yet Implemented

- user's trading strategy
- AI trading skill system
- real market data
- TradingView
- broker integration
- paper trading
- live trading

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

## Next Action

Proceed only after code review confirms the remediation is acceptable. Manual browser verification remains optional and environment-dependent.
