# TradingAgent Copilot Instructions

This is a professional local AI Forex Trading Agent.

Read the current project documentation under:
- CodingSkillAgent/PROJECT_CONTEXT.md
- CodingSkillAgent/ARCHITECTURE.md
- CodingSkillAgent/PHASES.md
- CodingSkillAgent/STATUS.md
- CodingSkillAgent/COST_POLICY.md
- CodingSkillAgent/MODEL_POLICY.md
- CodingSkillAgent/DEVELOPMENT_RULES.md

## Current Stack

- C#
- .NET 10
- ASP.NET Core 10
- Ollama
- Blazor Web App later
- Tailwind later
- xUnit

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
2. understand the phase
3. make the smallest correct change
4. build
5. run relevant tests
6. report actual results

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
