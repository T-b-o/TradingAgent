# TradingAgent — AI Workflow Contract

## Purpose

This document is the stable entry point for ChatGPT, Claude, and GitHub
Copilot. It makes the current repository and project records—not a previous
conversation—the handover between assistants.

To start work, tell the relevant assistant:

> Review `CodingSkillAgent/AI-WORKFLOW.md` and `CodingSkillAgent/CURRENT-HANDOFF.md`, then follow the instructions.

For a Copilot implementation task, the current handoff also identifies the
specific ChatGPT-approved prompt under `.github/Prompts/`. Do not ask Copilot
to infer or create implementation scope from a review alone.

## Authority and Source of Truth

In descending order for each type of fact:

1. Explicit user decisions and approvals.
2. The live repository and Git state for source, dependencies, changed files,
   commits, and automated verification.
3. `CodingSkillAgent/TECHNOLOGY-POLICY.md` for technology governance.
4. `CodingSkillAgent/ARCHITECTURE.md` and the active phase document for
   architecture and phase scope.
5. `CodingSkillAgent/CURRENT-HANDOFF.md` for the current task, known state,
   owners, decisions, and next action.
6. Reviews, evidence, and historical prompts as supporting records.

If records conflict, stop implementation, identify the conflict, and request a
user decision. No assistant may resolve an architecture or technology conflict
silently.

## Roles

### ChatGPT — Orchestrator and Acceptance Reviewer

- Reads the repository state, current handoff, relevant phase, evidence, and
  Claude review.
- Decides which review recommendations are approved, rejected, deferred, or
  require a user decision.
- Creates or updates the focused Copilot implementation prompt before Copilot
  changes code.
- Records the resulting active task, owner, inputs, outputs, and next action
  in `CURRENT-HANDOFF.md`.
- Performs final evidence-based acceptance review.
- Requests explicit user approval for technology changes, phase acceptance,
  commit, and push when required.

ChatGPT does not silently approve technology or architecture changes.

### Claude — Independent Reviewer

- Reviews architecture, code, documentation, evidence, and risks.
- Reads the current handoff before reviewing and validates conclusions against
  the repository or clearly states when it has only supplied files.
- Produces a concise, decision-oriented review using the structure in
  `CodingSkillAgent/Prompts/Claude-Desktop-Project-Instructions.md`.
- Saves or returns its review for storage under `CodingSkillAgent/Reviews/`.

Claude does not implement code, approve technology, or create Copilot work
scope.

### GitHub Copilot — Implementation and Verification Engineer

- Reads the current handoff and the exact ChatGPT-approved implementation
  prompt named there before changing any file.
- Implements only that approved scope, using the smallest correct change.
- Builds and tests the actual solution, then reports changed files and exact
  results.
- Stops for a Technology Change Request before introducing any unapproved
  technology.

Copilot does not expand scope, accept a phase, commit, or push without explicit
user approval.

### User — Project Owner

- Supplies requirements and trading strategy details.
- Approves technology changes, phase acceptance, commits, and pushes.
- Can reject, defer, or request revision of any assistant recommendation.

## Required Handoff Records

Every active work item must have these records before implementation begins:

1. `CURRENT-HANDOFF.md` identifies the exact current state, task owner,
   relevant inputs, required output, and next action.
2. A Claude review or a recorded user/repository finding exists for the work.
3. ChatGPT has recorded its decision on the findings.
4. For code changes, a current ChatGPT-approved Copilot prompt exists under
   `.github/Prompts/` and is named by the handoff.

After work completes, record:

1. The implementation report: changed files, build result, test result, and
   known limitations.
2. The Claude post-implementation review when required by the phase.
3. ChatGPT's acceptance assessment.
4. The user's explicit decision, including commit/push approval when relevant.

Do not rely on a chat transcript as the only record of any of these items.

## Handoff State Machine

```text
Planned
  → Claude review or recorded finding
  → ChatGPT decision
  → Copilot task prompt
  → Copilot implementation and evidence
  → Claude post-implementation review
  → ChatGPT acceptance assessment
  → User acceptance
  → Commit/push approval
```

A work item may skip implementation only when the handoff explicitly says it is
review-only or documentation-only. It may never skip the Technology Change Gate
when a new technology is proposed.

## Non-Negotiable Controls

- The runtime LLM remains local Ollama unless the user approves a change.
- C# owns deterministic calculations, validation, permissions, risk controls,
  and human approval gates.
- No assistant may invent the user's trading strategy or enable autonomous live
  trading.
- No assistant may add a package, provider, service, framework, runtime, or
  architecture-changing technology without the documented approval process.
- No assistant may commit, push, merge, or release without explicit user
  approval.
