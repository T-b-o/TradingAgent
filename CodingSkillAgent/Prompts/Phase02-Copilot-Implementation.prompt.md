# Phase 02 — Copilot Implementation

Read all relevant CodingSkillAgent documentation before changing code.

## First

Inspect the actual repository.

Verify:
- Phase 01 build
- relevant tests
- current API
- current project references
- current Git state

Then present:
- planned files
- planned changes
- expected runtime flow

## Implement

Phase 02 only:

- TradingAgent.Web
- Blazor Web App
- Tailwind CSS
- API client/service abstraction
- status/dashboard page
- loading state
- error state
- dependency injection
- relevant tests
- browser-debugging support

## Do Not Add

- TradingView
- trading strategy
- strategy skills
- broker
- live trading
- paper trading
- advanced charts

## Verification

After implementation:

1. build the full solution
2. run relevant tests
3. report exact results
4. identify actual changed files
5. update:
   - CodingSkillAgent/STATUS.md
   - CodingSkillAgent/Phases/PHASE-02.md
   - CodingSkillAgent/CHANGELOG.md

Only mark the phase COMPLETE when evidence supports completion.

## Git Approval Rule

You MUST NOT:
- git commit
- git push
- merge
- create releases

without explicit user approval.

Before any commit/push, show:

- changed files
- build result
- test result
- proposed commit message

Then ask:

"Do you approve the commit and push?"

If the answer is not explicit approval:
- do not commit
- do not push
- leave the changes local