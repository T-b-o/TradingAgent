# TradingAgent — Current AI Handoff

**Last reconciled:** 26 October 2026

> This record supersedes its earlier Phase 02 waiting state. Phase 02 remains
> accepted as historical evidence; this handoff governs the Phase 03
> documentation/review gate below.

## Use This File First

Read `CodingSkillAgent/AI-WORKFLOW.md`, then this file, before acting. Next,
read only the documents listed for your role and task. Inspect the live Git
state before making claims about source, commits, dependencies, or verification.

## Current State

- **Active phase:** Phase 03 — Adaptive Trading Assistant Foundation.
- **Phase status:** Documentation prepared; awaiting Claude technical review.
- **Previous phase:** Phase 02 was accepted by the user on 24 October 2026.
- **Next phase action:** Claude's read-only Phase 03 technical architecture review; Copilot implementation is not authorised.
- **Current implementation commit:** `c680595` — `Final Phase 02 changes.`
- **Runtime architecture:** Browser → SignalR → `TradingAgent.Web`
  (`InteractiveServer`) → server-side HTTP → `TradingAgent.Api` →
  Application/Infrastructure → local Ollama.
- **Technology baseline:** .NET 10, ASP.NET Core, Blazor Web App,
  Tailwind CSS, xUnit, OpenAPI/Scalar, and local Ollama. See
  `TECHNOLOGY-POLICY.md` for authority.
- **No application code change is currently authorised.**
- **Verified Phase 03 source baseline:** `IOllamaService` supports a single
  chat turn. No tool registry, persistent memory, strategy knowledge, or
  evidence engine is present.

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

## Phase 03 Direction and Constraints

- C# owns orchestration, memory, skills, tools, learning records, deterministic
  statistics, validation, risk enforcement, and approval gates; Ollama output
  is untrusted local inference.
- The long-term product direction is an Adaptive Trading Assistant that
  accumulates approved knowledge and evidence, not continuous model retraining.
- `qwen3:1.7b` and local Ollama remain in place under the $0/R0-first policy.
- Manual approval remains mandatory. No autonomous, paper, or live trading;
  no automatic strategy, skill, or risk modification.
- SQLite versus JSON persistence and Semantic Kernel versus existing C#
  orchestration are unresolved pending Claude review and owner approval.
- Embeddings/vector databases, Blazor PWA, and TradingView Lightweight Charts
  are not approved or assumed.
- Hermes is deferred; earlier Hermes ownership decisions are superseded.

## Documentation and Governance State

- The Tailwind policy wording was explicitly approved by the user: versions
  are declared with caret ranges in `package.json` and exact resolutions are
  locked in `package-lock.json`.
- `STATUS.md`, `Phases/PHASE-02.md`, and `CHANGELOG.md` record Phase 02
  verification and user acceptance.
- `TRADING-AGENT-PROJECT-SOURCE-DETAILS.md` is the detailed historical
  handover. It is historical context only; this file and `AI-WORKFLOW.md`
  govern the current cross-AI task.
- Phase 03 scope is in `Phases/PHASE-03.md`; the Claude review handoff is
  `Prompts/Phase03-Claude-Technical-Architecture-Review.prompt.md`; the
  Copilot draft is `.github/Prompts/Phase03-Copilot-Implementation-DRAFT.prompt.md`.

## Active Task

**Owner:** Claude Desktop (review), followed by ChatGPT and the user.

**Task:** Conduct the Phase 03 read-only technical architecture review named in
`CodingSkillAgent/Prompts/Phase03-Claude-Technical-Architecture-Review.prompt.md`.

**Authorised work:** Repository inspection and documentation review only. No
code, test, package, configuration, dependency, persistence, runtime, model,
or architecture implementation change is authorised.

**Recorded user decision:** Phase 02 was formally accepted on 24 October 2026;
Phase 03 documentation preparation is authorised, while implementation awaits
Claude review and owner decisions.

## Role-Specific Instructions

### ChatGPT

Read:

- `AI-WORKFLOW.md`
- this handoff
- `STATUS.md`
- `Phases/PHASE-02.md`
- `TECHNOLOGY-POLICY.md`
- current Git status and the latest evidence/review relevant to the decision

Do not implement Phase 03. Reconcile Claude's review with repository evidence,
record decisions, and obtain owner approval before authoring an authorised
Copilot task. Before any
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

Treat Phase 02 as accepted. Review Phase 03 according to the named technical
review handoff. Do not implement, select a technology, or create Copilot work
scope.

### GitHub Copilot

Read:

- `AI-WORKFLOW.md`
- this handoff
- `.github/copilot-instructions.md`

The Phase 03 Copilot prompt is a **DRAFT — NOT AUTHORISED FOR EXECUTION**.
Do not edit application code, configuration, dependencies, tests, or Phase 03
documentation. Request a replacement ChatGPT-approved prompt named here.

## Next Handoff Update

After Claude's review, ChatGPT must record its decision and the owner must
approve relevant technology/architecture choices. Only then may this file name
an authorised Copilot prompt for a narrowly scoped first implementation task.
