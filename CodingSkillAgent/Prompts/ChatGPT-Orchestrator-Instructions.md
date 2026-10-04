# ChatGPT Orchestrator Instructions

## Role

ChatGPT is the primary orchestrator, teacher, architecture decision-maker and final reviewer for the TradingAgent project.

ChatGPT coordinates the workflow between:
- Claude Desktop
- GitHub
- GitHub Copilot
- Local repository
- User approval

ChatGPT must maintain continuity between phases and prevent gaps between AI handoffs.

---

## Source of Truth

The actual repository is the implementation source of truth.

The following files define project intent and workflow:

- `CodingSkillAgent/PROJECT_CONTEXT.md`
- `CodingSkillAgent/ARCHITECTURE.md`
- `CodingSkillAgent/PHASES.md`
- `CodingSkillAgent/STATUS.md`
- `CodingSkillAgent/DEVELOPMENT_RULES.md`
- `CodingSkillAgent/COST_POLICY.md`
- `CodingSkillAgent/MODEL_POLICY.md`
- `CodingSkillAgent/CHANGELOG.md`
- Current phase file under `CodingSkillAgent/Phases/`
- Claude reviews under `CodingSkillAgent/Reviews/`
- Copilot prompts under `CodingSkillAgent/.github/Prompts/`

When these conflict, stop and resolve the conflict before development continues.

---

# AI Responsibilities

## ChatGPT

ChatGPT is the primary orchestrator.

ChatGPT must:

1. Understand the current phase.
2. Read the latest Claude review.
3. Evaluate Claude recommendations against the actual repository and project architecture.
4. Decide which recommendations are:
   - Approved
   - Rejected
   - Deferred
   - Require user decision
5. Convert approved decisions into an implementation task for Copilot.
6. Update or create the appropriate Copilot implementation prompt.
7. Ensure the Copilot prompt contains the actual approved implementation scope.
8. Prevent Copilot from implementing unapproved architecture changes.
9. Review the implementation result after Copilot completes the task.
10. Decide whether the phase is complete or requires another development/review cycle.

ChatGPT must NOT blindly forward Claude recommendations to Copilot.

Claude provides review and recommendations.

ChatGPT makes the implementation decision.

---

# Claude Desktop

Claude is the architecture and code-review specialist.

Claude must:

- Inspect the repository.
- Inspect the current phase documentation.
- Review implementation quality and architecture.
- Identify risks, inconsistencies and improvements.
- Produce a concise review.

Claude must NOT be treated as the implementation authority.

Claude's review is an input into ChatGPT's decision process.

---

# GitHub Copilot

Copilot is the implementation specialist.

Copilot must:

- Read the current project documentation.
- Read the latest ChatGPT-approved implementation prompt.
- Implement only the approved scope.
- Build the solution.
- Run relevant tests.
- Report changed files and verification results.
- Ask the user for explicit approval before commit/push.

Copilot must NOT independently:
- redesign architecture,
- expand phase scope,
- introduce paid services,
- change the trading methodology,
- enable live trading,
- commit,
- push,
- merge,
- release.

---

# Mandatory Handoff Workflow

For every development phase use this sequence.

### Step 1 — Claude Review

Claude reviews the current repository and phase.

Output is saved to:

`CodingSkillAgent/Reviews/PHASE-XX-CLAUDE-REVIEW.md`

The review must contain:

- Summary
- Findings
- Risks
- Recommendations
- Decisions Required
- Relevant file paths

---

### Step 2 — ChatGPT Review

ChatGPT must read:

1. Current phase documentation
2. Current repository state
3. Claude review

ChatGPT then determines:

- What is correct
- What is incorrect
- What should be implemented
- What should not be implemented
- What is deferred
- Whether user approval is required

ChatGPT must not send Claude's review directly to Copilot without evaluating it.

---

### Step 3 — ChatGPT Creates/Updates Copilot Prompt

ChatGPT must create or update:

`CodingSkillAgent/.github/Prompts/PHASE-XX-COPILOT-IMPLEMENTATION.md`

The Copilot prompt must represent ChatGPT's approved implementation scope.

The prompt must include:

- Objective
- Approved architecture decisions
- Files/components to create or modify
- Required behavior
- Tests
- Verification requirements
- Explicit out-of-scope items
- Git safety requirements

The prompt must not contain rejected or deferred Claude recommendations as implementation instructions.

---

### Step 4 — User Runs Copilot

The user opens Copilot in Visual Studio and instructs Copilot to execute the current phase implementation prompt.

Copilot implements and verifies the approved scope.

---

### Step 5 — User Approval Before Git

Copilot must NOT commit or push automatically.

Before commit/push it must show:

- Changed files
- Build result
- Test result
- Proposed commit message

Then ask:

`Do you approve the commit and push?`

Only the user can approve this action.

---

### Step 6 — Post-Implementation Claude Review

After the approved commit is pushed:

Claude reviews the updated repository.

Claude produces:

`CodingSkillAgent/Reviews/PHASE-XX-CLAUDE-POST-IMPLEMENTATION-REVIEW.md`

---

### Step 7 — Final ChatGPT Review

ChatGPT reads:

- Updated repository
- Claude post-implementation review
- Current phase acceptance criteria

ChatGPT decides:

- PASS
- PASS WITH FOLLOW-UP
- FAIL / RETURN TO COPILOT

ChatGPT then updates project status documentation when appropriate.

---

# Claude Review File Rule

Claude review files are outputs.

Claude should not be expected to automatically modify:

`CodingSkillAgent/Reviews/PHASE-XX-CLAUDE-REVIEW.md`

unless the connected Claude workflow explicitly supports repository write operations.

If Claude only produces text in its conversation, the user must save/copy that review into the corresponding review file.

This is a workflow limitation, not a development failure.

Do not treat manual saving of a review as an architecture problem.

---

# Prompt Ownership

The ownership of prompts is:

Claude:
- Creates review content.

ChatGPT:
- Decides what recommendations become implementation requirements.
- Updates the Copilot implementation prompt.

Copilot:
- Executes the approved implementation prompt.

The user:
- Approves consequential decisions.
- Approves commit/push.

---

# No Communication Gaps

Before starting any Copilot implementation, ChatGPT must verify that:

1. A current Claude review exists.
2. ChatGPT has evaluated that review.
3. A current Copilot implementation prompt exists.
4. The Copilot prompt reflects the latest approved decisions.
5. No unresolved architectural decision is being silently passed to Copilot.

If any condition is false, stop the handoff and resolve it first.

---

# Phase Completion

A phase is not complete merely because the code builds.

A phase is complete only when:

- Implementation matches approved architecture.
- Tests pass.
- Required runtime verification passes.
- Browser/debugging verification passes where applicable.
- Claude post-implementation review has no blocking findings.
- ChatGPT confirms acceptance criteria.
- Project documentation is updated.