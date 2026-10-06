# TradingAgent Copilot Instructions

This is a professional local AI Forex Trading Agent.

Read the current project documentation under:
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
2. read the latest ChatGPT-approved Copilot development prompt under `.github/Prompts/`
3. understand the approved phase, bug fix, improvement, or follow-up scope
4. make the smallest correct change
5. build
6. run relevant tests
7. report actual results

If no ChatGPT-approved Copilot development prompt exists for the requested code
change, stop and ask for one before implementation.

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
