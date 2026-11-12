# Phase 03 — Adaptive Trading Assistant Foundation

Status:
OWNER DECISIONS RECORDED — IMPLEMENTATION NOT AUTHORISED

## Objective

Establish the smallest safe foundation for the long-term Adaptive Trading
Assistant: controlled C# orchestration, bounded local-Ollama interaction,
approved JSON persistence, versioned trading knowledge, and evidence records.
This phase does not implement a trading strategy, trading automation, or full
learning intelligence.

## Verified Starting Point

- Phase 02 is accepted (24 October 2026).
- The current local runtime is `qwen3:1.7b` through Ollama.
- `IOllamaService` is a single-chat boundary; no tool registry, memory,
  persistence, strategy knowledge, or evidence engine exists yet.
- `TradingAgent.Web` is an `InteractiveServer` UI whose server-side client
  calls the API.
- Claude's Phase 03 technical architecture review was completed on 12 November
  2026 and the owner selected JSON persistence via BCL `System.Text.Json`.
- The owner also selected existing C# orchestration and approved the P03.4
  split into contracts-first (`P03.4a`) and JSON implementation (`P03.4b`).

## Product Direction

Strategy Knowledge + Trade Journal + Market Data + Agent Memory + Evidence
Engine + AI Reasoning = Adaptive Trading Assistant.

The product accumulates approved knowledge and evidence; it does not depend on
continuous model retraining. C# is authoritative for orchestration, memory,
skills, tools, learning records, statistics, risk enforcement, validation, and
human approval gates. Ollama supplies bounded local inference only. The owner
has selected BCL-only JSON persistence for the approved Phase 03 scope and
retained the existing C# orchestration model. Semantic Kernel remains deferred.

## Non-Negotiable Constraints

- Keep local Ollama and `qwen3:1.7b`; retain the $0/R0-first policy.
- No autonomous, paper, or live trading; manual approval remains mandatory.
- No automatic strategy, skill, risk-limit, or knowledge modification.
- LLM output is untrusted. C# calculates all statistics and validates every
  tool request, evidence reference, and proposed knowledge change.
- Do not add dependencies, databases, frameworks, providers, embeddings, or
  vector stores without the Technology Change Gate and explicit owner approval.
- Do not assume Blazor PWA or TradingView Lightweight Charts are approved or
  implemented; neither is in the approved baseline.
- Preserve Phase 01 and Phase 02 behaviour and historical evidence.

## Work Packages

### P03.1 — Ollama Context Investigation

**Purpose:** measure the effective local context configuration and establish a
bounded prompt-context policy for the current model and development PC.

**Dependencies:** existing Ollama runtime and Phase 02 chat path.

**Acceptance criteria:** document the inspected Ollama/model settings, actual
commands or requests used, observed effective `num_ctx` behaviour, prompt and
response limits, latency/memory observations, limitations, and a recommended
safe context budget. No setting is changed without approval.

**Testing/evidence:** repeatable local investigation notes; a non-destructive
smoke request; results clearly labelled as observed, not inferred.

### P03.2 — Agent Runtime and Orchestration Boundary

**Purpose:** define application-layer contracts for a bounded agent turn:
context assembly, local inference request, structured candidate response,
validation, audit/learning-record hand-off, and refusal handling.

**Dependencies:** P03.1 context budget; Claude review of contracts.

**Acceptance criteria:** explicit Application/Infrastructure/Domain ownership;
no provider type leaks into Domain; cancellation and timeout behaviour defined;
LLM cannot execute code, calculate authoritative figures, approve changes, or
reach integrations directly.

**Testing/evidence:** unit tests for orchestration decisions and invalid model
output; integration tests with a fake boundary; existing chat regression tests.

### P03.3 — Controlled C# Tool Registry

**Purpose:** define a closed, C#-owned registry for explicitly allow-listed,
read-only Phase 03 tools and structured tool results.

**Dependencies:** P03.2; approved contract design.

**Acceptance criteria:** canonical tool identity, typed argument validation,
permission/risk classification, cancellation, audit correlation, deterministic
result format, and rejection of unknown, malformed, duplicate, or disallowed
requests. No trade, write, strategy-change, or risk-change tool is enabled.

**Testing/evidence:** tests prove allow-list enforcement, argument validation,
permission denial, and audit records for attempted execution.

### P03.4 — Persistent Memory

**Purpose:** design and, only after explicit implementation approval, implement
minimal durable records for observations, conversations, trade outcomes,
learning proposals, provenance, and evidence references.

**Dependencies:** owner-selected JSON persistence using BCL `System.Text.Json`,
P03.2 identity/correlation design, and the approved `P03.4a`/`P03.4b` split.

**Acceptance criteria:** records distinguish observation, hypothesis, verified
outcome, and approved knowledge; provenance and immutable references are
preserved; retention, migration, failure, and backup approach are documented;
JSON persistence is selected by explicit owner decision and remains within the
approved Phase 03 scope.

**Testing/evidence:** repository-specific persistence contract tests after
approval; round-trip, corruption/failure, provenance, and concurrency tests
appropriate to the selected implementation.

**Approved sub-scope:**
- **P03.4a — Contracts first**: domain records, application interfaces, and
  validation boundaries; no concrete JSON persistence implementation.
- **P03.4b — JSON implementation**: concrete storage built on `System.Text.Json`
  and the approved contracts; must remain unauthorised until a separate
  Copilot prompt is explicitly approved.

### P03.5 — Versioned Trading Knowledge and Conversational Teaching Foundation

**Purpose:** support user-authored `strategy.md` knowledge and conversational
explanations as reviewable proposed structured rules.

**Dependencies:** P03.4 storage decision; P03.2 structured outputs; a supplied
user strategy (no strategy content is currently available).

**Acceptance criteria:** strategy identity/version, source/provenance, proposed
rule interpretation, human review state, approval decision, supersession link,
and reason for change are represented. Deterministic retrieval begins with
strategy IDs, symbols, tags, dates, and market conditions. Embeddings/vector
databases are explicitly deferred.

**Testing/evidence:** tests for version lineage, review-state transitions,
rejection, and deterministic retrieval filters. No generated proposal becomes
approved knowledge without an explicit human decision.

### P03.6 — Evidence-Based Learning Foundation

**Purpose:** define evidence records that tie outcomes to exact strategy
versions and allow C#-calculated, reviewable lessons later.

**Dependencies:** P03.4 persistence and P03.5 version identity.

**Acceptance criteria:** trade/outcome evidence references an exact strategy
version; supporting and contradictory evidence can coexist; source, sample
size, calculation inputs, statistical result, uncertainty, risk validation,
and proposal/review state are captured. The LLM may draft lessons but must not
invent figures, confidence, or promote a hypothesis.

**Testing/evidence:** tests for exact-version association, contradictory
evidence retention, deterministic-statistics boundary, and no-auto-promotion.

## Milestones and Dependencies

1. Claude conducts the read-only architecture review and identifies technology
   change requests and unresolved decisions.
2. Owner reviews Claude's verdict and explicitly selects or defers persistence
   and any orchestration technology. This phase records the approved JSON and
   C# orchestration decisions.
3. Authorised implementation planning begins with P03.1, then P03.2/P03.3.
4. P03.4 follows only the approved persistence decision and the P03.4a/P03.4b
   split.
5. P03.5 and P03.6 follow the approved record model; they remain foundations,
   not automatic learning or trading features.

## Explicitly Out of Scope

- Autonomous, paper, or live trade execution; broker integration.
- Automatic strategy/skill/risk modification or model retraining.
- Advanced statistics, full trade intelligence, pattern discovery, live signal
  monitoring, and market-data integration.
- SQLite, Semantic Kernel, embeddings, vector databases, Blazor PWA, and
  TradingView Lightweight Charts remain unapproved or deferred unless a
  separate owner decision approves them.
- JSON persistence is an approved Phase 03 design choice for the current scope
  and therefore is not a technology addition requiring a separate TCR in this
  project state.
