# Zero-Cost Development Policy

## Runtime AI

The TradingAgent runtime must use local Ollama.

Do not add:
- Anthropic API
- OpenAI API
- paid cloud LLM API
- paid AI runtime service

---

## Development Assistants

Claude Desktop and GitHub Copilot may be used through existing access.

Do not:
- buy a plan solely for this project
- add API billing
- intentionally trigger paid overage
- introduce paid runtime AI

---

## Local Technology Preference

Prefer:
- Ollama
- local inference
- built-in .NET functionality
- open-source/free dependencies
- local testing

All new technologies, including free and open-source dependencies, must also
follow `CodingSkillAgent/TECHNOLOGY-POLICY.md`.

---

## Skills

Every CodingSkillAgent skill must:
- prefer R0/local solutions
- avoid paid runtime services
- avoid unnecessary third-party dependencies
- identify cost before recommending an external service

If a proposed dependency introduces a cost:

1. stop
2. state the cost
3. provide the closest R0 alternative
4. wait for user approval

If a proposed dependency is free but new to the project, still stop and use the
Technology Change Gate before implementation.

---

## Rule

No AI assistant may silently introduce a paid service into the application.
