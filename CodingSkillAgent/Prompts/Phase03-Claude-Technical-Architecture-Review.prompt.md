# Phase 03 — Claude Technical Architecture Review

## Review Type and Authority

Read-only architecture review. Do not edit source, install packages, select a
technology, or author a Copilot implementation task. Inspect the live
repository first and distinguish verified facts from assumptions.

Read, in order:

1. `CodingSkillAgent/AI-WORKFLOW.md`
2. `CodingSkillAgent/CURRENT-HANDOFF.md`
3. `CodingSkillAgent/Phases/PHASE-03.md`
4. `CodingSkillAgent/PROJECT_CONTEXT.md`
5. `CodingSkillAgent/ARCHITECTURE.md`
6. `CodingSkillAgent/TECHNOLOGY-POLICY.md`
7. `CodingSkillAgent/COST_POLICY.md`
8. `CodingSkillAgent/MODEL_POLICY.md`
9. the current Application, Domain, Infrastructure, API, Web, and test source

## Requested Review

Critique the proposed Phase 03 foundation against actual repository evidence.

- Compare SQLite and JSON persistence. Do not select either; identify which
  choice requires a Technology Change Request and what evidence/criteria the
  owner needs to approve it.
- Compare Semantic Kernel with the existing C# orchestration approach. Do not
  recommend adoption without a Technology Change Request and owner approval.
- Review memory, provenance, versioned strategy knowledge, conversation-led
  rule proposals, and human approval states.
- Review tool execution boundaries, risk gates, and the closed registry model.
- Review evidence-based learning safeguards, authoritative C# statistics,
  contradictory evidence, and exact strategy-version linkage.
- Assess deterministic RAG (IDs, symbols, tags, dates, market conditions),
  bounded prompt assembly, and an investigation plan for Ollama `num_ctx`.
- Identify Technology Change Requests, if any, including whether the current
  policy allows a zero-dependency JSON implementation versus a database.
- Recommend the safest implementation sequence and test strategy.

## Required Verdict

State one of: **ready for owner decision**, **ready for a narrowly defined
implementation prompt**, or **not ready**. List unresolved owner decisions
separately. Do not treat an architectural recommendation as approval.

Use the concise format and evidence rules in
`CodingSkillAgent/Prompts/Claude-Desktop-Project-Instructions.md`. Save the
result under `CodingSkillAgent/Reviews/` using a Phase 03-specific filename.
