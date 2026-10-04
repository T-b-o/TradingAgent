# Phase 02 — Claude Architecture Review

Review the current TradingAgent repository from GitHub.

Do NOT modify source code.

## Step 1 — Read Project Context

Read:

- CodingSkillAgent/PROJECT_CONTEXT.md
- CodingSkillAgent/ARCHITECTURE.md
- CodingSkillAgent/PHASES.md
- CodingSkillAgent/STATUS.md
- CodingSkillAgent/CHANGELOG.md
- CodingSkillAgent/DEVELOPMENT_RULES.md
- CodingSkillAgent/COST_POLICY.md
- CodingSkillAgent/MODEL_POLICY.md
- CodingSkillAgent/Phases/PHASE-01.md

## Step 2 — Verify Phase 01

Inspect the actual source code.

Verify:

- current solution structure
- API boundaries
- Application layer
- Infrastructure layer
- Ollama integration
- configuration
- error handling
- OpenAPI/Scalar
- tests
- current API endpoints

Identify only issues that materially affect Phase 02.

## Step 3 — Review Phase 02

Phase 02 objective:

Build the first Blazor Web App and learn the complete browser-to-server
debugging flow.

Expected scope:

- TradingAgent.Web
- Blazor Web App
- Tailwind CSS
- API client/service abstraction
- status/dashboard page
- loading state
- error state
- API integration
- relevant tests
- Visual Studio debugging
- Chrome DevTools Network debugging

## Explicitly Out of Scope

Do NOT introduce:

- TradingView
- trading strategy
- strategy skills
- broker integration
- live trading
- paper trading
- advanced trading charts
- news engine
- unnecessary cloud services

## Architecture Questions

Evaluate:

- Web/API project boundary
- API client design
- dependency injection
- API base URL configuration
- CORS
- error handling
- loading states
- UI/API contracts
- Tailwind integration
- debugging workflow
- testing strategy

## Debugging Requirement

The user must learn this runtime path:

Browser
  ↓
Blazor
  ↓
HTTP request
  ↓
ASP.NET Core API
  ↓
Application
  ↓
Response
  ↓
Blazor
  ↓
Browser

The review must identify:
- where Visual Studio breakpoints should be used
- what should be inspected in Locals
- what should be inspected in Call Stack
- what should be inspected in Chrome Network
- important failure cases to demonstrate

## Output

Follow the Claude Review Output Rules.

Return ONLY:

### Summary

### Findings

### Risks

### Recommendations

### Decisions Required

### Files

Do not provide implementation code.

## Handoff

This review will be saved as:

CodingSkillAgent/Reviews/PHASE-02-CLAUDE-REVIEW.md

It will be consumed by ChatGPT for architecture/learning review and by
Copilot for implementation context.

Therefore:
- keep it concise
- avoid repeating project documentation
- identify actual files/paths
- clearly separate verified facts from recommendations
- target 300–500 words
- absolute maximum 700 words