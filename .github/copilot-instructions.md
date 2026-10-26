# TradingAgent Copilot Instructions

This is a professional local AI Forex Trading Agent.

Read the current project documentation under:
- CodingSkillAgent/AI-WORKFLOW.md
- CodingSkillAgent/CURRENT-HANDOFF.md
- CodingSkillAgent/PROJECT_CONTEXT.md
- CodingSkillAgent/ARCHITECTURE.md
- CodingSkillAgent/TECHNOLOGY-POLICY.md
- CodingSkillAgent/STATUS.md
- CodingSkillAgent/COST_POLICY.md
- CodingSkillAgent/MODEL_POLICY.md
- CodingSkillAgent/DEVELOPMENT_RULES.md

## Current Stack

The approved baseline stack is defined in:

CodingSkillAgent/TECHNOLOGY-POLICY.md

Current approved baseline:
- C#
- .NET 10
- ASP.NET Core 10
- Blazor Web App
- Tailwind CSS
- ASP.NET Core APIs + OpenAPI
- Ollama
- xUnit

## Technology Governance

Before implementation, inspect CodingSkillAgent/TECHNOLOGY-POLICY.md.

Do not add, install, configure, replace, or use a new technology unless it is
already approved by project documentation.

NuGet packages, npm packages, JavaScript libraries, CSS/UI frameworks,
middleware, SDKs, external APIs, services, databases, hosting platforms,
authentication providers, telemetry products, AI providers, and model runtimes
all count as technology additions.

If a technology addition is required, stop and produce a Technology Change
Request using:

CodingSkillAgent/Prompts/TECHNOLOGY-CHANGE-REQUEST.md

Wait for explicit user approval before implementation.

## Architecture

Api -> Application -> Domain

Infrastructure -> Application -> Domain

Domain must remain dependency-free.

Keep Ollama implementation in Infrastructure.

Do not put business/trading logic in Program.cs or Blazor UI.

## AI Safety

LLM output is untrusted input.

Validate every tool request.

LLM must not directly execute C# or live trades.

C# owns deterministic calculations, validation, risk and execution gates.

## Cost

Runtime AI must remain local Ollama.

Do not add paid LLM APIs.

## Coding

- modern C#
- nullable-aware
- async
- dependency injection
- testable
- strong typing
- clear names
- minimal useful comments
- XML docs for public APIs where appropriate

## Development Process

Before changing code:
1. inspect current implementation
2. read `CodingSkillAgent/CURRENT-HANDOFF.md` to identify the active authorised task
3. read the latest ChatGPT-approved Copilot development prompt identified by that handoff under `.github/Prompts/`
4. understand the approved phase, bug fix, improvement, or follow-up scope
5. make the smallest correct change
6. build
7. run relevant tests
8. report actual results

If the handoff says no implementation task is authorised, or if no
ChatGPT-approved Copilot development prompt is identified for the requested
code change, stop and ask ChatGPT/the user for the next authorised handoff.

The Phase 03 Copilot handoff marked `DRAFT — NOT AUTHORISED FOR EXECUTION` is
not a current implementation prompt. Phase planning, reviews, and technology
comparisons never authorise implementation.

## Debugging

For web issues trace:

Browser
 -> HTTP
 -> ASP.NET routing
 -> endpoint
 -> Application
 -> Infrastructure
 -> Ollama
 -> response
 -> browser

Use Visual Studio debugger and Chrome DevTools.
