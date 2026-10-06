# Phase Completion Procedure

Complete the phase only from actual repository evidence.

## Run

dotnet build

Run the relevant test suite.

Then inspect:

git status

git diff --name-status

git log --stat

## Record

- build result
- test result
- added files
- modified files
- deleted files
- dependencies changed
- technology policy compliance
- architecture decisions
- known issues
- next phase

## Update

- CodingSkillAgent/STATUS.md
- CodingSkillAgent/Phases/PHASE-XX.md
- CodingSkillAgent/CHANGELOG.md

## Critical Rule

Do not guess which files changed.

Use Git.

## Technology Policy

Check whether any package, framework, library, runtime, middleware, database,
provider, hosting platform, telemetry product, authentication provider, external
API, service, or architecture-changing technology was added or replaced.

If yes, confirm it was explicitly approved through
`CodingSkillAgent/TECHNOLOGY-POLICY.md` before recording the phase as complete.

## Git Approval

Do NOT commit or push.

Show:
- changed files
- build result
- test result
- proposed commit message

Then ask:

"Do you approve the commit and push?"

Only proceed after explicit approval.
