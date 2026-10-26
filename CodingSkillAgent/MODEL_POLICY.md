# Model Policy

## Trading Runtime

### Current

qwen3:1.7b

Use for:
- smoke tests
- tool-calling tests
- early development
- Phase 03 bounded-context investigation

### Next Benchmark

qwen3:4b

Benchmark on the actual development PC before adopting.

Measure:
- latency
- tool-call reliability
- memory usage
- load time
- output quality

### Later Candidate

qwen3.5:4b

Only test after qwen3:4b.

---

## Claude Desktop

Use for:
- architecture
- planning
- difficult reasoning
- code review
- documentation review

Do not use Claude API for the application.

Do not recommend buying Claude Code solely for this project.

Use the strongest Claude model already available in the user's existing access.

---

## GitHub Copilot

Use for:
- implementation
- refactoring
- testing
- debugging
- code exploration

Routine tasks:
- use a lightweight model when available

Complex tasks:
- use the strongest model available in the user's existing access

Do not assume a particular Copilot model is included.

Check the model selector in Visual Studio.

---

## Model Rule

Use the smallest model that reliably completes the task.

Do not choose a larger model simply because it is available.

## Phase 03 Context Rule

Before increasing model size or assuming a context window, inspect and record
the effective Ollama `num_ctx` behaviour on the development PC. Keep retrieved
context bounded; context findings do not authorise a model change.
