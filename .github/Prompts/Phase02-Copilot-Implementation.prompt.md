# Phase 02 — Copilot Implementation Task

## Source of Truth

Read before making changes:

- CodingSkillAgent/PROJECT_CONTEXT.md
- CodingSkillAgent/ARCHITECTURE.md
- CodingSkillAgent/TECHNOLOGY-POLICY.md
- CodingSkillAgent/STATUS.md
- CodingSkillAgent/DEVELOPMENT_RULES.md
- CodingSkillAgent/COST_POLICY.md
- CodingSkillAgent/MODEL_POLICY.md
- CodingSkillAgent/Phases/PHASE-02.md
- CodingSkillAgent/Reviews/PHASE-02-CLAUDE-REVIEW.md

The Claude review is architecture input, not an unconditional instruction.
Verify its findings against the actual repository.

If this prompt conflicts with CodingSkillAgent/TECHNOLOGY-POLICY.md, the
Technology Policy controls.

---

# Phase 02 Objective

Build the first Blazor Web App and establish a professional browser-to-API
debugging workflow.

Target runtime flow:

Browser
    ↓
Blazor Web App
    ↓
HTTP
    ↓
TradingAgent.Api
    ↓
Application
    ↓
Response
    ↓
Blazor
    ↓
Browser

---

# Approved Architecture Decisions

## 1. Separate Web and API Origins

Use:

TradingAgent.Web
    +
TradingAgent.Api

The Web project communicates with the API over HTTP.

Do not merge the Web and API hosts during Phase 02.

Do not introduce replacement hosting, UI, CSS, runtime, API, or architecture
technologies unless they have already passed the Technology Change Gate.

---

## 2. CORS

Implement a named CORS policy.

Development must use an explicit configured origin.

Do NOT use:

AllowAnyOrigin()

Do NOT use wildcard CORS for the browser application.

Allowed origins must come from configuration.

Do not hard-code the Blazor URL inside application logic.

---

## 3. API Base URL

TradingAgent.Web must use configuration for the API base URL.

Do not hard-code the API URL in Blazor components.

The configuration must allow different values for:
- Development
- Test
- Production

---

## 4. Existing API

Keep the existing API contracts.

Phase 02 may use:

GET /api/status

POST /api/agent/chat

Do not redesign these endpoints unless an actual technical problem requires it.

---

# Blazor Scope

Create:

TradingAgent.Web

Use the current supported Blazor Web App approach for .NET 10.

Implement:

- application shell/layout
- navigation
- status page
- API connectivity indicator
- basic AI chat page
- loading state
- error state
- empty state
- API response display

Keep UI clean and professional.

Do not spend excessive time on visual polish.

---

# Tailwind CSS

Use Tailwind CSS for styling.

Keep the Tailwind setup:
- simple
- maintainable
- current
- development-friendly

Do not introduce UI component libraries, CSS frameworks, JavaScript libraries,
or npm packages unless already approved by project policy.

If a package or framework appears necessary, stop and use
CodingSkillAgent/Prompts/TECHNOLOGY-CHANGE-REQUEST.md.

---

# API Client

Create an API service/client abstraction.

Blazor components should NOT contain raw HttpClient logic.

Target flow:

Blazor Component
    ↓
API Client
    ↓
HTTP
    ↓
TradingAgent.Api

The API client should:
- use dependency injection
- use configuration
- support cancellation
- handle HTTP errors appropriately
- return typed responses

---

# Error Handling

Support at least:

### API success

HTTP 200

### Validation failure

HTTP 4xx

### API unavailable

Connection failure

### Ollama unavailable

API returns a safe error response.

The UI should show a useful user-facing message.

Do not expose internal stack traces.

---

# Browser Debugging Requirement

The implementation must allow me to learn and demonstrate:

Chrome
    ↓
Blazor
    ↓
HTTP request
    ↓
ASP.NET Core
    ↓
Application
    ↓
Response
    ↓
Blazor
    ↓
Chrome

I must be able to inspect:

Chrome:
- Network
- Request URL
- method
- headers
- payload
- status
- response
- timing
- Console

Visual Studio:
- breakpoint
- Locals
- Watch
- Call Stack
- Step Over
- Step Into
- exception details
- Output

---

# CORS Debugging Exercise

Ensure one deliberate test demonstrates:

Blazor origin
    ↓
OPTIONS preflight
    ↓
API
    ↓
CORS response

I must be able to identify the CORS request in Chrome Network.

Do not create a fake exercise.
Use the real application's development configuration.

---

# Tests

Add only useful tests.

Required where appropriate:

- API status integration coverage
- API client behaviour
- error handling
- CORS configuration
- Blazor/API integration behaviour that is practical to test

Do not add meaningless coverage.

---

# Out of Scope

Do NOT add:

- TradingView
- Pine strategy
- user's trading strategy
- AI skill system
- broker integration
- live trading
- paper trading
- advanced market charts
- news integration
- paid cloud AI
- unnecessary external services

Also do NOT add any new package, framework, library, runtime, middleware,
database, provider, hosting platform, telemetry product, authentication
provider, or external service without explicit user approval through the
Technology Change Gate.

---

# Code Quality

Use:

- C# / .NET 10
- nullable-aware code
- dependency injection
- async/await
- CancellationToken
- strong typing
- clear names
- focused services
- concise XML documentation where appropriate

Keep comments useful.

Do not put business logic into Blazor components.

Do not put Ollama HTTP implementation into Blazor.

---

# Verification

Before reporting completion:

1. Build the full solution.
2. Run relevant tests.
3. Start Web + API.
4. Verify the browser application.
5. Verify /api/status through the Web application.
6. Verify the chat request.
7. Verify CORS behaviour.
8. Test at least one failure path.
9. Verify the Visual Studio breakpoint.
10. Verify Chrome Network inspection.

Report actual results.

Do not claim anything was tested unless it was actually tested.

---

# Phase Documentation

After the implementation is verified:

Update:

CodingSkillAgent/STATUS.md

CodingSkillAgent/Phases/PHASE-02.md

CodingSkillAgent/CHANGELOG.md

Use Git to determine the actual files changed.

Do not invent the changed-file list.

---

# Git Approval Rule

You may modify local files.

You MUST NOT:

- git commit
- git push
- merge
- create releases

without explicit user approval.

Before asking for approval, show:

### Changed Files
Actual files changed.

### Build
Actual build result.

### Tests
Actual test results.

### Commit Message
Proposed commit message.

Then ask exactly:

"Do you approve the commit and push?"

If approval is not explicitly given:
- do not commit
- do not push
- leave changes local
