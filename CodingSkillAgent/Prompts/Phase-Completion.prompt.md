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