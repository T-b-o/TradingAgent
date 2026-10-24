# TradingAgent — Current AI Handoff

**Last reconciled:** 24 October 2026

## Use This File First

Read `CodingSkillAgent/AI-WORKFLOW.md`, then this file, before acting. Next,
read only the documents listed for your role and task. Inspect the live Git
state before making claims about source, commits, dependencies, or verification.

## Current State

- **Active phase:** Phase 02 — Blazor + Browser Debugging.
- **Phase status:** Accepted by the user on 24 October 2026.
- **Next phase:** Phase 03 is not authorised; planning or implementation needs separate user authorisation.
- **Current implementation commit:** `c680595` — `Final Phase 02 changes.`
- **Runtime architecture:** Browser → SignalR → `TradingAgent.Web`
  (`InteractiveServer`) → server-side HTTP → `TradingAgent.Api` →
  Application/Infrastructure → local Ollama.
- **Technology baseline:** .NET 10, ASP.NET Core, Blazor Web App,
  Tailwind CSS, xUnit, OpenAPI/Scalar, and local Ollama. See
  `TECHNOLOGY-POLICY.md` for authority.
- **No application code change is currently authorised.**

## Verified Phase 02 Evidence

- A fresh Release build on 24 October 2026 passed with 0 warnings and 0 errors.
- A fresh automated test run on 24 October 2026 passed: 14 tests, 0 failures.
- The user manually verified rendering, end-to-end Web/API requests,
  breakpoints, the SignalR connection, API stop/retry recovery, Ollama
  stop/restart behaviour, the chat pending state, and the 660-second timeout
  hint.
- The user stopped only `TradingAgent.Web` and observed the styled “Rejoining
  the server...” reconnect dialog in the existing browser tab.
- In the accepted `InteractiveServer` design, Chrome shows the `_blazor`
  WebSocket. Browser-side `/api/*` Fetch/XHR requests are not expected.

## Documentation and Governance State

- The Tailwind policy wording was explicitly approved by the user: versions
  are declared with caret ranges in `package.json` and exact resolutions are
  locked in `package-lock.json`.
- `STATUS.md`, `Phases/PHASE-02.md`, and `CHANGELOG.md` now record completed
  Phase 02 verification but do not claim final acceptance.
- `TRADING-AGENT-PROJECT-SOURCE-DETAILS.md` is the detailed historical
  handover. It is currently untracked and must be deliberately included or
  excluded when the documentation work is committed.
- This handoff and `AI-WORKFLOW.md` are new documentation-only workflow
  records and must be reviewed before any commit.

## Active Task

**Owner:** User and ChatGPT.

**Task:** Preserve the accepted Phase 02 record and wait for a separately authorised Phase 03 request.

**Authorised work:** No new implementation work is authorised. No code,
package, configuration, dependency, architecture, or Phase 03 change is
authorised.

**Recorded user decision:** Phase 02 was formally accepted on 24 October 2026.

## Role-Specific Instructions

### ChatGPT

Read:

- `AI-WORKFLOW.md`
- this handoff
- `STATUS.md`
- `Phases/PHASE-02.md`
- `TECHNOLOGY-POLICY.md`
- current Git status and the latest evidence/review relevant to the decision

Do not start Phase 03. If the user authorises Phase 03 planning, create a new
handoff task and obtain the required review before implementation. Before any
commit or push, show changed files, build/tests, a proposed message, and ask
exactly: “Do you approve the commit and push?”

### Claude

Read:

- `AI-WORKFLOW.md`
- this handoff
- `Phases/PHASE-02.md`
- `ARCHITECTURE.md`
- `TECHNOLOGY-POLICY.md`
- the latest Phase 02 evidence and relevant review files

Treat Phase 02 as accepted. If asked to review the phase, identify only a
material defect that could invalidate acceptance. Do not recommend Phase 03
implementation or change source code without a new authorised handoff.

### GitHub Copilot

Read:

- `AI-WORKFLOW.md`
- this handoff
- `.github/copilot-instructions.md`

There is no authorised Copilot implementation task. Do not edit application
code, configuration, dependencies, or tests. If asked to implement anything,
stop and request a current ChatGPT-approved prompt named in this handoff.

## Next Handoff Update

When the user authorises Phase 03 planning or implementation, ChatGPT must
update this file with the exact request, owner, authorised scope, required
review path, and—before code changes—the exact Copilot task-prompt path.
