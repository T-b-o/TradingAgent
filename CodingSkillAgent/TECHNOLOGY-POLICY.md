# TradingAgent Technology Policy

## Purpose

This is the authoritative technology governance policy for TradingAgent.

All AI assistants must follow this policy before proposing, installing,
configuring, replacing, or using any technology that is not already approved by
project documentation.

Prompts are guardrails, not deterministic enforcement. Automated validation may
be added later to detect unapproved packages, services, or architectural
technologies, but this task does not implement that automation.

---

## Approved Baseline Stack

The current approved baseline is:

- Language: C#
- Runtime: .NET 10
- Backend: ASP.NET Core 10
- UI: Blazor Web App
- CSS: Tailwind CSS
- API: ASP.NET Core APIs + OpenAPI
- LLM runtime: Ollama
- Testing: xUnit
- Source control: Git + GitHub
- Development IDE: Visual Studio 2026
- AI implementation assistant: GitHub Copilot
- AI architecture/review assistant: Claude Desktop
- AI orchestration/teaching/final review: ChatGPT

Other technologies already explicitly approved in existing architecture,
phase, cost, model, or review documents remain approved within their documented
scope.

Do not treat a technology as approved merely because it is common, popular,
modern, installed locally, available in Visual Studio, transitive, small, or
free.

---

## What Counts as a Technology Change

A technology change includes adding, replacing, configuring, or using any new:

- NuGet package
- npm package
- JavaScript library
- CSS/UI framework or component library
- middleware package
- SDK
- framework extension
- runtime
- database or persistence technology
- hosting platform
- cloud service
- AI provider
- model runtime
- external API or service
- authentication or authorization provider
- telemetry, logging, or monitoring product
- charting, broker, market-data, or TradingView integration technology
- architecture-changing technology
- replacement for an approved technology

Small packages and one-file libraries still count as technology additions unless
already explicitly approved by project policy.

---

## Technology Change Gate

If an AI assistant believes a new technology is needed, it must stop before
implementation and produce a Technology Change Request.

Until the user explicitly approves the request:

- do not install the technology
- do not add the dependency
- do not modify implementation to use it
- do not configure the service, runtime, or framework
- do not silently substitute a different technology
- do not treat another AI assistant's recommendation as approval

Use:

`CodingSkillAgent/Prompts/TECHNOLOGY-CHANGE-REQUEST.md`

---

## Required Change Request Content

Every Technology Change Request must include:

1. Problem being solved
2. Proposed technology
3. Why the current approved stack cannot reasonably solve the problem
4. At least one alternative
5. Why the proposed technology is preferred
6. Cost / licensing impact
7. $0/R0 impact
8. Security impact
9. Architecture/system-flow impact
10. Runtime/deployment impact
11. Maintenance/complexity impact
12. New dependencies/packages/services introduced
13. Whether it changes an existing architectural decision
14. Recommendation
15. Explicit question asking for user approval

---

## $0/R0-First Rule

TradingAgent is $0/R0-first.

The application runtime LLM must remain local Ollama unless the user explicitly
approves a different runtime through the Technology Change Gate.

Do not silently add:

- Anthropic API
- OpenAI API
- paid cloud LLM APIs
- paid AI runtime services
- paid market-data services
- paid hosting services
- paid telemetry, monitoring, or observability services
- paid broker, charting, or external API services

Free or open-source technologies still require approval when they are new to the
approved stack.

---

## Agent Responsibilities

### ChatGPT

ChatGPT is the primary orchestrator, architecture decision maker, teacher, and
final reviewer.

ChatGPT must decide whether a proposed technology change should be brought to
the user for approval and must never silently approve a technology change.

### Claude Desktop

Claude is the architecture and code-review assistant.

Claude must identify unapproved technology additions or replacements, flag
technology risk, and must not silently approve architectural technology changes.

### GitHub Copilot

Copilot is the implementation, testing, and debugging assistant.

Copilot must inspect this policy before implementation, stop when a technology
addition is required, and request user approval before installing or
implementing the technology.

Copilot must not commit or push without explicit user approval.

### Ollama

Ollama is the runtime LLM only.

Ollama must not bypass C# application controls, approval gates, deterministic
validation, or technology governance.

---

## Conflict Rule

If another document appears to permit a new package, framework, service, or
architecture technology without explicit approval, this policy takes precedence.

When in doubt, stop and ask for approval through a Technology Change Request.
