# TradingAgent — Project Source Details and Handover

**Document purpose:** Transfer the essential project context into a new ChatGPT account or conversation without requiring the user to explain the project again.

**Last updated:** 24 October 2026  
**Project:** TradingAgent / CodingSkillAgent  
**Primary language:** C#  
**Target platform:** .NET 10  
**Repository location:** `C:\Development\Gen AI Dev\TradingAgent`  
**Current project phase:** Phase 02 — accepted on 24 October 2026
**Current acceptance status:** Accepted by the user on 24 October 2026
**Next phase:** Phase 03 requires separate user authorisation.

---

## 1. How to Use This Document

Treat this document as the persistent project handover and orientation guide.

At the start of a new conversation:

1. Read this document before recommending actions.
2. Identify the current phase and outstanding acceptance criteria.
3. Check the repository's actual state before proposing code changes.
4. Consult the project's own governance documents for authoritative implementation rules.
5. Continue from the current position instead of restarting completed work.

This document is a consolidated handover, not proof of the current Git state. The live repository, current evidence files, approved project documents, and user-confirmed test results take precedence where they contain newer information.

## 2. Project Overview

TradingAgent is a learning-oriented AI-assisted trading analysis application.

The long-term objective is to build a local, cost-conscious AI agent that helps the user analyse financial markets and improve a personal trading strategy without removing the user's control over trading decisions.

The project combines two goals:

- Build a real, maintainable software system using C# and .NET.
- Learn how to design AI agents, tools, memory, strategy knowledge, evaluation, and controlled improvement.

The user wants a working application, but also wants to understand how it is built. Technical explanations should be concise, practical, and incremental.

### 2.1 Core principles

- Prefer local, free, or zero-token-cost components.
- Keep architecture maintainable and production-conscious.
- Separate AI reasoning from deterministic application logic.
- Use AI-readable strategy knowledge rather than embedding the entire trading strategy in Pine Script or C#.
- Allow the AI to analyse a strategy and propose improvements.
- Require human approval before any trade is opened.
- Do not implement autonomous live trading.
- Preserve auditability and explicit application-side controls.
- Do not claim that an AI has learned or improved unless there is evidence demonstrating it.

## 3. User's Working and Learning Preferences

The assistant should:

- Use short sections, bullets, tables, and numbered steps.
- Avoid unnecessarily long paragraphs.
- Explain one actionable step at a time when the user is performing a manual procedure.
- Ask one direct clarifying question when an unresolved decision materially affects the solution.
- Never invent repository state, completed tests, tool results, or approvals.
- Explain exactly where to click or what command to run when giving instructions.
- Wait for the user's result before moving to the next diagnostic step when appropriate.
- Prefer the smallest correct change over broad refactoring.
- Explain why a change is needed and how to verify it.
- Keep code clean, documented, and suitable for future maintenance.

The user prefers direct feedback and wants problems identified rather than hidden behind optimistic status statements.

## 4. Technology and Cost Constraints

### 4.1 Approved baseline

| Area | Current baseline |
|---|---|
| Language | C# |
| Platform | .NET 10 |
| API | ASP.NET Core |
| UI | Blazor Web App |
| UI rendering | `InteractiveServer` |
| Styling | Tailwind CSS 3.4 |
| AI runtime | Ollama, running locally |
| Current local model | `qwen3:1.7b` |
| Tests | xUnit |
| API documentation | OpenAPI / Scalar in Development |
| IDE | Visual Studio 2026 |
| Browser testing | Chrome DevTools |
| Cost goal | $0 additional AI API/token cost |

The exact installed versions and package state must be checked against the repository when needed.

### 4.2 Technology governance

The project has a strict technology approval process.

- Do not add NuGet or npm packages without explicit approval.
- Do not introduce another framework, runtime, middleware, external service, database, or AI provider without approval.
- Do not replace the approved UI technology or render mode without approval.
- Do not make silent architectural changes.
- If a proposed solution requires new technology, stop and prepare a Technology Change Request using the repository's approved template.
- Existing dependencies must be distinguished from newly introduced dependencies.
- Keep the existing Tailwind toolchain. Its `postcss` and `autoprefixer` wording is approved: versions are declared with caret ranges in `package.json`, with exact resolutions locked in `package-lock.json`.
- Do not add Bootstrap. Bootstrap was accidentally introduced during earlier UI work and subsequently removed; Tailwind is the approved styling approach.

The user approved this Tailwind policy clarification on 24 October 2026. Check the current repository policy rather than relying on superseded evidence snapshots.

### 4.3 Zero-cost operating principle

The user does not want the project to depend on a paid model API or ongoing token charges.

The architecture should favour local inference through Ollama and deterministic local application services. Any future external data provider, paid API, or cloud dependency requires an explicit cost and technology review.

## 5. Repository and Architecture

Repository root:

`C:\Development\Gen AI Dev\TradingAgent`

The solution contains these principal projects:

| Project | Responsibility |
|---|---|
| `TradingAgent.Api` | ASP.NET Core API, endpoints, HTTP error handling, application entry point |
| `TradingAgent.Application` | Application-level services and abstractions |
| `TradingAgent.Domain` | Domain concepts; must remain dependency-free |
| `TradingAgent.Infrastructure` | Infrastructure implementations, including Ollama integration |
| `TradingAgent.Tests` | Automated tests |
| `TradingAgent.Web` | Blazor Web App and server-side API client |
| `CodingSkillAgent` | Project rules, architecture, technology policy, phases, status, reviews, evidence and prompts |
| `.github/Prompts` | Implementation and follow-up prompts |

The exact folder names and current contents must be checked against Git.

### 5.1 Dependency boundaries

The established dependency direction is:

- API → Application and Infrastructure
- Application → Domain
- Infrastructure → Application and Domain
- Domain → no project dependencies

Do not change these boundaries without a documented and approved architectural decision.

### 5.2 Current request flow

The accepted runtime architecture is `InteractiveServer`.

```text
User
  |
  v
Chrome browser
  |
  | Blazor interaction over SignalR
  v
TradingAgent.Web
  |
  | TradingAgentApiClient
  | server-side HTTP
  v
TradingAgent.Api
  |
  v
Application / Ollama abstraction
  |
  v
Infrastructure
  |
  | local HTTP
  v
Ollama
  |
  v
Local language model
```

Important distinctions:

- The browser communicates with the Blazor Server application through SignalR.
- The Web server communicates with the API through `TradingAgentApiClient`.
- The API uses the application/infrastructure boundary to call Ollama.
- Normal chat and status requests do not require browser-side `/api/*` requests.
- CORS remains configured but is not the active browser request path.
- The original CORS-driven separate-origin architecture proposal is superseded.

Do not change the render mode to WebAssembly or reintroduce browser-side API fetches just to demonstrate CORS.

### 5.3 Local endpoints used during manual verification

| Component | URL |
|---|---|
| API | `https://localhost:7062` |
| Web | `https://localhost:7084` |
| Ollama tags endpoint | `http://localhost:11434/api/tags` |

These are the reported Development endpoints. Confirm the current launch profiles if the application configuration changes.

## 6. AI and Trading Agent Goals

The current application is the foundation, not the finished trading agent.

The longer-term system should be capable of:

1. Gathering market data from approved sources.
2. Calculating deterministic technical indicators.
3. Identifying relevant market conditions.
4. Consulting AI-readable strategy knowledge.
5. Producing an analysis with supporting evidence.
6. Explaining risks, uncertainty, and reasons for a possible setup.
7. Recording observations and outcomes.
8. Evaluating historical strategy performance.
9. Proposing improvements to the user's strategy.
10. Presenting proposed changes for human review and approval.

### 6.1 Planned analytical capabilities

Initial concepts include:

- `get_price(symbol)` — retrieve the relevant price data.
- Support and resistance analysis.
- `get_rsi()` — RSI, momentum and potential divergence.
- `calculate_risk()` — deterministic risk calculations.

These are design intentions and previously explored tools, not a claim that every tool is implemented in the current repository.

The user has identified risk calculation as particularly important.

RSI knowledge already discussed:

- Common period: 14.
- RSI above 70 is commonly treated as overbought.
- RSI below 30 is commonly treated as oversold.

These are common analytical conventions, not sufficient trading signals by themselves.

### 6.2 Strategy knowledge versus code

The user does not want the full personal trading strategy hard-coded into Pine Script or C#.

The intended direction is to keep strategy rules, technical-analysis knowledge, and learning instructions in AI-readable knowledge or skill files, while retaining deterministic calculations and safety controls in application code.

The architecture should support both:

- Applying the user's existing rule-based strategy.
- Proposing possible strategy improvements based on evidence.

Do not implement a strategy or claim a particular strategy has been approved without the user's input.

### 6.3 What “learning by itself” means

The user wants to explore an AI system that can improve from experience.

Treat this as a staged engineering objective, not as a capability automatically provided by Ollama or an LLM.

Possible stages include:

1. **Memory:** persist observations, decisions, context and outcomes.
2. **Retrieval:** find relevant past experiences and strategy knowledge.
3. **Evaluation:** measure whether previous analyses or setups performed well.
4. **Reflection:** produce structured explanations of what worked and what did not.
5. **Proposal:** suggest modifications to strategy knowledge.
6. **Validation:** backtest or otherwise evaluate proposed changes against suitable historical data.
7. **Approval:** require the user to approve strategy changes before adoption.

Memory, retrieval, reflection and model training are different mechanisms. Persistent memory alone does not retrain a model or guarantee that its decisions improve.

Future designs must distinguish storing experiences, changing prompts or knowledge files, updating model weights, and validating trading performance.

## 7. Project Governance and AI Handoffs

`CodingSkillAgent` is the project's governance and workflow system.

Its documented materials include:

- `DEVELOPMENT_RULES.md`
- `ARCHITECTURE.md`
- `TECHNOLOGY-POLICY.md`
- `STATUS.md`
- `CHANGELOG.md`
- `Phases/PHASE-02.md`
- `Reviews/`
- `Evidence/`
- `Prompts/`

Other referenced files may exist or may need reconciliation. Verify the actual repository before relying on any path.

### 7.1 Roles

| Role | Responsibility |
|---|---|
| ChatGPT | Primary orchestrator, teacher, decision-support and review coordinator |
| Claude | Independent architecture/code reviewer and evidence reviewer |
| GitHub Copilot | Implementation and tests within explicitly approved scope |
| Ollama | Local AI runtime used by the application |
| User | Owner of requirements, technology approvals, acceptance, and commit/push authorisation |

Do not assume that any assistant has live access to the repository. A review based on supplied files is not the same as a live GitHub sync or locally executed test.

### 7.2 Mandatory implementation workflow

Before any implementation:

1. Read the applicable development rules, architecture, technology policy, status, phase and latest evidence.
2. Inspect the actual repository and Git state.
3. Establish a baseline of staged, unstaged, untracked and unrelated changes.
4. Check whether the proposed change is already implemented.
5. Identify the smallest required change.
6. Confirm that it is within the approved scope.
7. Stop for approval if a material architectural or technology decision is missing.

After implementation:

1. Inspect the complete diff.
2. Run the required build and tests.
3. Report exact results and any failures.
4. Generate or refresh evidence only after the final changes and tests.
5. Keep manual verification pending until the user actually performs it.
6. Request independent review when the workflow requires it.
7. Update status and phase documentation based on evidence, not assumptions.

### 7.3 Git safety

- Never commit, push, merge, or release without explicit user approval.
- Never silently stage files or alter unrelated work.
- Do not use broad Git commands that discard or overwrite user changes.
- Do not claim a commit or push occurred unless evidence confirms it.
- Before requesting commit/push approval, present the changed-file list, build result, test result and proposed commit message.
- Ask exactly: **“Do you approve the commit and push?”**
- If staged or unrelated changes are found, report them and stop until the user directs how to proceed.

## 8. Current Phase 02 State

### 8.1 Phase 02 objective

Phase 02 establishes the first usable Blazor UI and proves the real server-side application request path, diagnostics and failure behaviour.

The accepted implementation uses `InteractiveServer`, with `TradingAgentApiClient` calling the API from the Web server.

### 8.2 Remediation history

- Phase 02 implementation was followed by independent review and several remediation cycles.
- An earlier Bootstrap incident was corrected; Bootstrap is not an approved styling approach.
- Remediation 2 aligned timeout handling, corrected request/error behaviour, clarified the Blazor Server request path and added or updated tests and documentation.
- Remediation 3 corrected the chat empty-state issue, completed additional documentation and evidence work, and clarified the Tailwind toolchain policy wording.
- The latest supplied Claude review changed the verdict from **BLOCKED** to **READY FOR MANUAL VERIFICATION**.
- Claude reported no unresolved code defects in the supplied Remediation 3 source and evidence.
- Claude did not execute the build or tests. Its build/test conclusions rely on supplied Copilot evidence.
- Remediation 3 changes were reported as uncommitted. No commit or push should be assumed.

### 8.3 Latest independent review

Review title: `Phase 02 — Claude Remediation 3 Independent Review`

Review basis:

- `PHASE-02-REMEDIATION-3-EVIDENCE.md`
- HEAD reported as `b379516`
- Supplied source files
- Review against nine requirements

The review is not a live repository sync.

Claude's latest verdict was:

**READY FOR MANUAL VERIFICATION**

The review reported:

- The in-flight "No response yet." defect is fixed in the supplied source.
- Chat success and failure rendering is null-safe.
- XML documentation is improved, with a minor constructor documentation gap.
- Template leftovers were confirmed absent using the supplied Git/on-disk checks.
- API timeout is 600 seconds and Web timeout is 660 seconds.
- The historical inventory was corrected.
- No unintended architecture, dependency or Phase 01 changes were identified in the supplied diff.
- The final evidence includes a refresh after the last source correction.
- No unresolved code defects were identified.

Caveats:

- The Remediation 3 implementation prompt was not supplied to Claude for that final review.
- The final evidence is a snapshot plus appended refresh; some earlier displayed sections contain pre-correction text.
- The review did not execute commands independently.
- The approved `TECHNOLOGY-POLICY.md` now accurately describes the Tailwind package ranges and lockfile resolutions.
- Minor historical documentation items remain, but they do not block Phase 02 acceptance.
- Phase 02 was formally accepted by the user on 24 October 2026.

### 8.4 Reported automated verification

The supplied evidence reports:

- Release build: 0 warnings, 0 errors.
- Automated tests: 14 passed.
- No test failures reported.

These are evidence-reported results, not tests executed by the new assistant. Rerun only if required by the current workflow, if files have changed since that evidence, or if the user requests a fresh verification.

### 8.5 Timeout configuration

The supplied review records:

| Setting | Value |
|---|---:|
| API `Ollama:Timeout` | 600 seconds (10 minutes) |
| Web `Api:RequestTimeoutSeconds` | 660 seconds (11 minutes) |

The Web timeout must remain strictly greater than the API Ollama timeout. The two settings are in separate projects and are not automatically enforced against each other.

`HttpClient.Timeout` is the single Web request timeout authority. The UI should not create a competing timeout source.

The known limitation is that Status and Home share the long Web HTTP timeout. A hung API request can therefore make those pages wait for the configured limit. Per-call timeout separation is out of scope unless explicitly approved.

### 8.6 Current reported manual verification

The user has progressed through these checks in the original conversation:

| Check | Latest reported result |
|---|---|
| API and Web HTTPS endpoints | Both working |
| Application renders | Working |
| Breakpoint in Web/API request path | Both breakpoints hit |
| Blazor SignalR connection | `_blazor` WebSocket observed |
| Browser-side `/api/*` requests | None observed in Fetch/XHR |
| API stopped | API URL fails; Status shows Unavailable after Retry |
| API restarted | Status returns to Running after Retry |
| Ollama stopped | Chat displays one failure card with “The local AI service could not process the request.” |
| Ollama restarted | Successful response from `qwen3:1.7b` |
| In-flight chat empty state | “No response yet.” disappears during the request |
| Timeout hint | Displays 660 seconds (11 minutes) |
| Reconnect dialog | Styled “Rejoining the Server...” dialog observed after stopping only `TradingAgent.Web` |

The user also reported that Chrome's console showed the `_blazor` WebSocket connection and repeated Visual Studio Browser Link ping failures when Chrome DevTools was set to Offline. The reconnect dialog did not appear during that test.

The Browser Link messages are not evidence that the application's reconnect modal works or fails. The reconnect test must interrupt the Blazor Server connection itself.

The completed checks are recorded in `STATUS.md`, `PHASE-02.md`, and the cross-AI handoff. Check current Git state before any acceptance or commit decision.

## 9. Remaining Phase 02 Work

### 9.1 Reconnect-dialog verification completed

The remaining reported manual check was completed on 24 October 2026. With the
API and Ollama left running, the user stopped only `TradingAgent.Web` and the
existing Chrome tab displayed the styled “Rejoining the Server...” dialog.

The result must be reconciled into the phase, status, changelog, and current
handoff records before formal acceptance.

Historical recommended controlled test:

Recommended controlled test:

1. Start the Web application and open it in Chrome.
2. Confirm the page is interactive.
3. Leave Chrome open.
4. Stop only the `TradingAgent.Web` process. Do not stop the API or Ollama for this test.
5. Wait for the existing Blazor connection to fail and for the reconnect UI to appear.
6. Confirm the dialog is styled correctly.
7. Restart the Web application and check whether the page recovers or offers the expected recovery action.
8. Record the actual result, including any failure to show the dialog.

Stopping the Web process can interrupt the server-side circuit. The exact UI and recovery behaviour depends on the application's existing reconnect implementation and should be observed rather than assumed.

Do not ask the user to repeat checks already reported as successful unless there is evidence of subsequent code changes or an explicit need to reproduce them.

### 9.2 Resolve minor documentation and governance items

The latest Claude review identified:


Completed documentation and governance work:

- The Tailwind wording is authorised and corrected.
- `STATUS.md` explicitly identifies Phase 02 and its verified state.
- Phase, status, changelog, project context, and handover records are reconciled.
- `AI-WORKFLOW.md` and `CURRENT-HANDOFF.md` provide a repository-based
  handoff for ChatGPT, Claude, and Copilot.

Deferred non-blocking items:

- Add the optional missing constructor `<param>` XML documentation in a future
  approved code/documentation task.
- Decide whether to restructure historic evidence snapshots.
- Review the breadth of Phase 02 historical inventory acceptance criterion 10.
- Decide whether to create `CodingSkillAgent/PHASES.md` or remove the invalid
  historical reference. Do not create it automatically.

These were described as minor items rather than code defects. The user must decide whether to accept them as-is or approve a small documentation-only follow-up.

### 9.3 Formal acceptance gate

Phase 02 was accepted on 24 October 2026 after:

1. All required manual checks have been performed and accurately recorded.
2. The reconnect-dialog check has been resolved.
3. The outstanding Tailwind governance decision is recorded.
4. The final status and phase documents agree.
5. The evidence still represents the final implementation state.
6. No unintended changes or unapproved technology have been introduced.
7. The user and ChatGPT explicitly assess the acceptance criteria.

The acceptance is based on the independent review, fresh build/test evidence, and the completed manual verification—not on the review verdict alone.

Do not start Phase 03 before acceptance.

## 10. Immediate Next Action

Obtain the user's explicit Phase 02 acceptance decision. If accepted, update
the current handoff with the decision, keep Phase 03 blocked until separately
authorised, and obtain explicit approval before any commit or push.

## 11. Related Project Direction: Hermes and AI Memory

The user has also explored a possible future “$0 trading agent using Hermes” and wants to compare it with the current Ollama-based architecture.

This is exploratory work, not an approved replacement architecture.

Any future comparison must account for:

- Local hardware constraints.
- Zero additional API/token cost.
- Compatibility with the existing .NET architecture.
- Tool permission boundaries and deterministic risk calculations.
- Persistent memory and retrieval.
- Strategy evaluation and controlled improvement.
- Human approval before strategy changes or trading actions.
- Technology approval before adding a new agent framework or service.

Do not replace the existing project with Hermes, add a new runtime, or begin a parallel implementation without an explicit decision.

## 12. Known Local Development Environment

Previously reported environment:

- Windows desktop.
- Visual Studio 2026.
- .NET SDKs 8.0.420, 9.0.318 and 10.0.401.
- `dotnet --version` previously returned `10.0.401`.
- AMD Ryzen 5 3400G with Radeon Vega Graphics.
- 16 GB installed RAM, approximately 13.9 GB usable.
- Local Ollama version previously reported as 0.32.15.
- Current model previously reported as `qwen3:1.7b`.

These are historical environment details. Recheck versions if they become relevant to troubleshooting or performance decisions.

## 13. What Must Not Be Assumed

A new assistant must not assume that:

- The repository is currently at the same commit as the supplied review.
- Remediation 3 has been committed or pushed.
- Claude executed the build or tests.
- All manual checks remain complete after checking the current handoff and Git state.
- The reconnect dialog remains verified after checking the current handoff and Git state.
- Phase 02 has been formally accepted.
- Phase 03 is authorised to start.
- The Tailwind toolchain governance decision remains recorded in the current policy.
- A memory system, strategy-learning system, backtester or autonomous trading capability already exists.
- A source file or document exists merely because it is mentioned in a prompt.
- The linked GitHub repository or previous shared ChatGPT conversation is accessible in a new account.

Check the current repository and evidence before making claims about any of these items.

## 14. Standing Instructions for the Assistant

Act as the project's careful technical lead and learning coach.

- Start from the current state.
- Prefer evidence over assumptions.
- Keep responses concise and actionable.
- Ask the user to run commands only when they are needed.
- Never claim to have executed local commands unless they were actually executed.
- Never create or modify files unless the user asks for that work.
- Do not bypass Technology Governance.
- Do not commit or push without explicit approval.
- Do not start Phase 03 until Phase 02 is accepted.
- Preserve the user's human approval over trading and strategy changes.

**Immediate objective:** Preserve the accepted Phase 02 handover and wait for separate authorisation before beginning Phase 03 planning or implementation.
