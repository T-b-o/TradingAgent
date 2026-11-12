# TradingAgent — Current AI Handoff

**Last reconciled:** 13 November 2026

> This record supersedes its earlier Phase 02 waiting state. Phase 02 remains
> accepted as historical evidence; this handoff governs the Phase 03
> documentation/review gate below.

## Use This File First

Read `CodingSkillAgent/AI-WORKFLOW.md`, then this file, before acting. Next,
read only the documents listed for your role and task. Inspect the live Git
state before making claims about source, commits, dependencies, or verification.

## Current State

- **Active phase:** Phase 03 — Adaptive Trading Assistant Foundation.
- **Phase status:** Owner decisions recorded; documentation reconciliation in progress; implementation remains unauthorised.
- **Previous phase:** Phase 02 was accepted by the user on 24 October 2026.
- **Next phase action:** Reconcile Phase 03 documentation, record the approved owner decisions, and prepare the next narrowly scoped implementation prompt only after explicit owner review.
- **Current implementation baseline:** repository HEAD is `d65c984` (`Chat GPT Skill update`) in the live workspace; the owner-supplied baseline `83be25d` was not verified as checked out here and is not treated as the live source of truth.
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

## Approved Phase 03 Owner Decisions

- **D1 — Persistence:** JSON using BCL `System.Text.Json` is the selected Phase 03 persistence approach for the approved scope. SQLite remains unapproved.
- **D2 — Orchestration:** the existing C# orchestration remains the selected model. Semantic Kernel is deferred and not approved for this phase.
- **D3 — P03.4 architecture:** the P03.4 work is split into `P03.4a` (contracts first) and `P03.4b` (JSON implementation), both unauthorised until specific Copilot prompts are approved.
- **D4 — Orchestrator skill maintenance:** the ChatGPT TradingAgent Orchestrator skill may be updated to reflect the accepted JSON/C# orchestration decisions and the Phase 03 review gate.
- **D5 — Ollama adapter extension:** additive Ollama adapter work is permitted only within separately authorised implementation scope; it does not change the `IOllamaService.ChatAsync` contract.

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
- Owner-approved Phase 03 decisions are: JSON persistence via `System.Text.Json`, existing C# orchestration, and a split P03.4a/P03.4b scope.
- Embeddings/vector databases, Blazor PWA, TradingView Lightweight Charts, SQLite, and Semantic Kernel remain unapproved for this phase unless a separate owner decision approves them.
- Hermes is deferred; earlier Hermes ownership decisions are superseded.

## Documentation and Governance State

- The Tailwind policy wording was explicitly approved by the user: versions
  are declared with caret ranges in `package.json` and exact resolutions are
  locked in `package-lock.json`.
- `STATUS.md`, `Phases/PHASE-02.md`, and `CHANGELOG.md` record Phase 02
  verification and user acceptance.
- `TRADING-AGENT-PROJECT-SOURCE-DETAILS.md` is historical context only; this file and `AI-WORKFLOW.md`
  govern the current cross-AI task.
- Phase 03 scope is in `Phases/PHASE-03.md`; the Phase 03 review record is
  `CodingSkillAgent/Reviews/PHASE-03-CLAUDE-TECHNICAL-ARCHITECTURE-REVIEW.md`.
- The authoritative Owner Decision Record is captured in this handoff, the Phase 03 review record, and the reconciled project documentation.

## Active Task

**Owner:** ChatGPT, with project-owner approval and subsequent implementation prompts only when explicitly authorised.

**Task:** Reconcile the Phase 03 documentation and owner decision record, ensuring the project record reflects approved JSON persistence, retained C# orchestration, the P03.4a/P03.4b split, and the approved orchestrator skill update.

**Authorised work:** Documentation review and reconciliation only. No
code, test, package, configuration, dependency, persistence, runtime, model,
or architecture implementation change is authorised.

**Recorded user decision:** Phase 02 was formally accepted on 24 October 2026;
Phase 03 is active, the Claude review is complete, and the owner has approved JSON persistence, existing C# orchestration, and the controlled P03.4 split for future implementation planning only.

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
