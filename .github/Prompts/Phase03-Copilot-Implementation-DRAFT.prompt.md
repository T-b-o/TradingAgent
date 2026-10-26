# Phase 03 — Copilot Implementation Handoff

**DRAFT — NOT AUTHORISED FOR EXECUTION**

Do not change files, configuration, dependencies, packages, data stores, or
tests under this handoff. This is a planning record only. Implementation may
begin only after a Claude review is stored, ChatGPT records its decision in
`CodingSkillAgent/CURRENT-HANDOFF.md`, the owner approves outstanding
architecture/technology decisions, and a new authorised Copilot prompt is
named by that handoff.

## Approved Direction and Constraints

- Phase 03 establishes a foundation for the Adaptive Trading Assistant as
  defined in `CodingSkillAgent/Phases/PHASE-03.md`.
- C# owns orchestration, memory, skills, tools, learning records, risk,
  statistics, validation, and approval gates. Ollama remains local inference.
- Keep `qwen3:1.7b`, local Ollama, and the $0/R0-first policy.
- LLM output is untrusted; it may propose structured interpretations and
  lessons only. It may not execute tools directly, invent statistics, approve
  knowledge, change strategy, alter risk, or trade.
- Preserve accepted Phase 01/02 behaviour. Manual approval is mandatory.

## Proposed Milestones

1. P03.1: investigate effective Ollama context and document a bounded context
   budget without changing runtime configuration.
2. P03.2: introduce approved C# agent-turn contracts and test seams.
3. P03.3: introduce a closed, typed, audited, read-only C# tool registry.
4. P03.4: implement persistent memory only after the owner selects an approved
   persistence option.
5. P03.5: implement versioned knowledge/proposal/review contracts only after
   persistence and user strategy inputs are available.
6. P03.6: implement evidence records and deterministic-statistics boundaries;
   do not implement advanced intelligence or auto-learning.

## Required Contracts and Responsibilities

- **Domain:** dependency-free concepts, identifiers, invariants, version and
  review-state models only where the approved design requires them.
- **Application:** use cases, orchestration interfaces, validation contracts,
  deterministic retrieval contracts, authorisation/risk decisions, and test
  seams. No concrete provider/storage implementation.
- **Infrastructure:** Ollama adapter, selected persistence implementation, and
  approved external integrations. No unapproved dependency.
- **API/Web:** DTO translation and presentation only; no business/risk logic.
- **C#:** authoritative calculations, sample sizes, uncertainty inputs, audit
  records, and tool execution decisions.

## Test and Acceptance Requirements

- Add focused tests for invalid LLM output, bounded context construction,
  unknown/invalid/disallowed tool requests, audit correlation, version lineage,
  review-state transitions, provenance, deterministic retrieval, exact
  evidence-to-strategy-version association, contradictory evidence, and
  no-auto-promotion.
- Build the solution and run relevant tests; report exact commands/results and
  known limitations. Do not claim manual or Ollama verification without actual
  evidence.
- Keep existing Phase 02 tests and behaviour passing.

## Evidence and Reporting Format

Report: authorised scope; files changed; contracts added/changed; dependency
diff; build result; test result; manual verification; data-migration or
backward-compatibility notes; risks/limitations; and decisions still requiring
owner approval. Update phase/status/changelog/handoff records only when the
authorised prompt instructs it.

## Prohibited Actions

- No SQLite, JSON persistence mechanism, Semantic Kernel, embeddings, vector
  database, Blazor PWA, TradingView Lightweight Charts, market data, broker,
  cloud service, package, or framework without the Technology Change Gate and
  explicit owner approval.
- No autonomous/paper/live trading; no strategy/risk/skill modification;
  no model retraining; no advanced pattern discovery or live monitoring.
- No commits, pushes, merges, releases, or scope expansion.

## Dependencies Before Authorisation

1. Claude review from `CodingSkillAgent/Prompts/Phase03-Claude-Technical-Architecture-Review.prompt.md`.
2. ChatGPT decision recorded in `CodingSkillAgent/CURRENT-HANDOFF.md`.
3. Owner approval for each architecture/technology decision, including any
   persistence technology and any Semantic Kernel proposal.
4. A replacement prompt explicitly marked authorised for the first milestone.
