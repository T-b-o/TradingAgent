# TradingAgent — Project Context

## Purpose

Build a professional local AI Forex Trading Agent.

The system exists to:
- perform AI-assisted market analysis
- reduce emotion-driven trading decisions
- interpret a rule-based trading strategy using broader technical-analysis knowledge
- keep human control over live trade approval
- keep deterministic financial controls outside the LLM

This is both:
1. a real software project
2. a professional learning project

---

## User Development Level

The user is a Senior C# Developer.

Teaching must focus on:
- architecture
- runtime behaviour
- API design
- AI-agent design
- debugging
- testing
- security
- observability
- engineering trade-offs

Avoid beginner programming explanations unless directly relevant.

The user specifically wants to learn:
- ASP.NET Core debugging
- Visual Studio debugging
- Chrome DevTools
- HTTP/API troubleshooting
- AI-agent architecture
- local LLM integration

---

## Mandatory Cost Rule

The application runtime must remain R0/$0 for LLM usage.

The production/runtime LLM must run locally through Ollama.

Do not add:
- Anthropic API
- OpenAI API
- paid cloud LLM APIs
- paid AI runtime services

Claude Desktop and GitHub Copilot are development assistants only.

Do not intentionally create paid AI overage.

---

## Current Environment

- Visual Studio 2026
- .NET SDK 10.0.401
- .NET SDK 9.0.318
- .NET SDK 8.0.420
- Ollama 0.32.15
- Current local model: qwen3:1.7b
- CPU: AMD Ryzen 5 3400G
- RAM: 16 GB
- GPU: AMD Radeon Vega 11 integrated graphics

Current Ollama execution is CPU-based.

---

## Current Solution

TradingAgent/
- Api/
- Application/
- Domain/
- Infrastructure/
- Tests/

Development knowledge and AI instructions:

TradingAgent/CodingSkillAgent/

---

## Development Assistants

### Claude Desktop

Use for:
- architecture planning
- architecture review
- difficult technical reasoning
- code review
- documentation review
- debugging analysis

Claude Desktop is not assumed to have live repository editing access in the
current development setup.

### GitHub Copilot in Visual Studio

Use for:
- implementation
- refactoring
- test creation
- debugging
- codebase exploration
- code changes

Copilot works against the actual repository opened in Visual Studio.

### ChatGPT

Use for:
- teaching
- architecture review
- debugging coaching
- knowledge checks
- challenging design decisions

### Ollama

Use as the actual AI runtime for the trading application.

---

## Source of Truth

The Git repository is the source of truth for:
- source code
- actual file changes
- commit history
- branch history
- phase implementation history

CodingSkillAgent is the source of truth for:
- architecture decisions
- development rules
- phase definitions
- AI-assistant instructions
- cost policy
- model policy

Never rely on an AI conversation as the only project record.

---

## Trading Strategy

The user has an existing rule-based trading strategy.

The strategy has NOT been provided yet.

Do not:
- invent it
- infer missing rules
- implement it prematurely
- convert it into Pine Script
- convert it into hard-coded C# rules

When the strategy phase begins:
1. analyse the supplied strategy
2. identify explicit rules
3. identify concepts and knowledge
4. identify assumptions
5. identify ambiguities
6. identify exceptions
7. create modular AI-readable skills
8. version the skills
9. allow AI improvement proposals
10. require human approval before changing production strategy knowledge

---

## AI Responsibilities

The AI should:
- interpret market conditions
- reason about technical-analysis context
- interpret the user's strategy
- identify potential setups
- identify invalidation conditions
- compare scenarios
- explain its analysis
- propose strategy/skill improvements

The AI must not:
- silently modify strategy knowledge
- independently change risk limits
- directly execute live trades
- bypass C# validation

---

## C# Responsibilities

C# controls:
- deterministic calculations
- validation
- risk limits
- tool execution
- authorization
- approval gates
- audit logging
- external integrations

---

## Deterministic Calculations

Prefer trusted deterministic code for:
- RSI
- EMA
- ATR
- position sizing
- risk percentage
- exposure
- risk/reward
- daily loss limits
- trade validation

The LLM may interpret these values.

The LLM must not invent them.

---

## Trading Safety

Initial flow:

Market Data
    ↓
AI Analysis
    ↓
C# Validation
    ↓
Trade Proposal
    ↓
Human Approval

No autonomous live trading during development.

---

## Core Principle

Use deterministic code for deterministic work.

Use the LLM for:
- interpretation
- contextual reasoning
- orchestration
- scenario analysis