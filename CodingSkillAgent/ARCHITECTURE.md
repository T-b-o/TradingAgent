# TradingAgent — Architecture

## Dependency Direction

Api
    ↓
Application
    ↓
Domain

Infrastructure
    ↓
Application
    ↓
Domain

Domain must remain independent of:
- ASP.NET Core
- Ollama
- database implementations
- external providers

New technology that changes dependency direction, runtime flow, hosting,
integration boundaries, packages, services, providers, or frameworks must pass
through `CodingSkillAgent/TECHNOLOGY-POLICY.md` before implementation.

---

## Api

Owns:
- HTTP endpoints
- routing
- request/response DTOs
- API validation
- middleware
- OpenAPI
- ProblemDetails
- HTTP error handling

Must NOT own:
- trading strategy logic
- financial calculations
- Ollama implementation
- UI logic

---

## Application

Owns:
- use cases
- orchestration
- agent workflows
- application interfaces
- application validation

Application should not depend on concrete external implementations.

---

## Domain

Owns:
- trading concepts
- entities
- value objects
- enums
- domain rules

Domain should remain infrastructure-free.

---

## Infrastructure

Owns:
- Ollama HTTP integration
- market-data providers
- TradingView integration
- persistence
- external integrations

---

## Tests

Own:
- unit tests
- integration tests
- API behaviour tests

Tests should verify behaviour rather than simply increase coverage.

---

## Future Web Project

TradingAgent.Web

Responsibilities:
- Blazor Web App
- Tailwind CSS
- dashboard
- analysis display
- trade proposal display
- user approval UI

The UI must NOT:
- call Ollama directly
- own trading strategy rules
- calculate financial risk independently
- place trades directly

---

## AI Boundary

LLM:
- interprets
- reasons
- requests tools
- returns structured analysis

C#:
- treats LLM output as untrusted
- validates tool names
- validates arguments
- checks permissions
- executes approved tools
- performs deterministic calculations
- enforces risk controls
- enforces approval gates

---

## Tool Flow

LLM
    ↓
Tool Request
    ↓
Tool Registry
    ↓
Known Tool?
    ↓
Valid Arguments?
    ↓
Permitted?
    ↓
Execute
    ↓
Tool Result
    ↓
LLM

Unknown or invalid requests must be rejected.

---

## Application Runtime

Browser
    ↓
ASP.NET Core API
    ↓
Application
    ↓
Infrastructure
    ↓
Ollama
    ↓
Infrastructure
    ↓
Application
    ↓
API
    ↓
Browser

---

## Future Trading Runtime

TradingView / Market Data
    ↓
Market Context
    ↓
AI Analysis
    ↓
Structured Trade Proposal
    ↓
Deterministic Risk Validation
    ↓
Human Approval
    ↓
Paper Trade

Live broker integration is a late optional phase.

TradingView, market-data providers, databases, authentication, charting
libraries, broker integrations, hosting platforms, telemetry products, and other
future technologies are not automatically approved by being named here. They
must pass through the Technology Change Gate before implementation unless an
existing project document has explicitly approved the specific technology and
scope.

---

## Design Principle

Do not put the trading brain into a giant collection of C# if/else statements.

The strategy and technical-analysis knowledge will eventually live in versioned AI
skills.

C# remains the enforcement and safety layer.
