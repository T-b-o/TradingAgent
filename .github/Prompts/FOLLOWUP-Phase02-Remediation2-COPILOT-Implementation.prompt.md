# Phase 02 — Remediation 2 (Copilot Implementation Task)

## Source of Truth

Read before making changes:

- CodingSkillAgent/DEVELOPMENT_RULES.md
- CodingSkillAgent/ARCHITECTURE.md
- CodingSkillAgent/TECHNOLOGY-POLICY.md
- CodingSkillAgent/STATUS.md
- CodingSkillAgent/Phases/PHASE-02.md
- CodingSkillAgent/CHANGELOG.md
- CodingSkillAgent/Reviews/PHASE-02-CLAUDE-POST-IMPLEMENTATION-REVIEW.md
- The Phase 02 Post-Remediation Review (user will supply if not in repo)

The review is input, not instruction. Verify each item against the actual
repository before changing anything. If an item is already correct, report that
and change nothing.

If this prompt conflicts with TECHNOLOGY-POLICY.md, the policy controls.

## Objective

Close the remaining gaps from the Phase 02 post-remediation review so Phase 02
can be assessed for acceptance on evidence. Smallest correct changes only.

## Approved Decisions

- D1: Web timeout alignment, with a single timeout authority.
  1. Before choosing a value, read the effective `Ollama:Timeout` from
     `TradingAgent.Api/appsettings.json`, `appsettings.Development.json`,
     and any environment-specific override. Report the value found.
  2. Set `Api:RequestTimeoutSeconds` in both Web appsettings files to
     strictly greater than that value. The last supplied value was 10 minutes,
     so 660s (11 minutes) is proposed. If the value found differs, recompute.
  3. `HttpClient.Timeout` (configured once in `Web/Program.cs`) is the ONLY
     timeout source. Remove the timeout `CancellationTokenSource` and the
     `RequestTimeoutSeconds` / `IConfiguration` usage from `Chat.razor`.
     The component keeps one `CancellationTokenSource`, used only to cancel
     the in-flight request on navigation or disposal.
  4. Result mapping must be unambiguous:
     - `HttpClient.Timeout` fires (caller token not cancelled): the client
       returns the timeout failure with the typed timeout signal.
     - Caller token cancelled: cancellation propagates and the component
       treats it as user/navigation cancellation, not as a timeout.
  5. The Chat hint text and timeout message must state the real configured
     limit, read from the client or a typed options object, not hard-coded.
  6. Remove the hard-coded 180s fallbacks. Fail fast on startup if the
     value is missing or not positive.
  7. Record in `PHASE-02.md` that the Web timeout must stay above the API
     Ollama timeout. The two settings live in separate projects and nothing
     enforces this automatically.
  8. Known limitation, not to be fixed in this task: Status and Home share
     this long timeout, so a hung API makes them wait for it. A per-call
     timeout split is out of scope unless the user approves it.
	 
- D2: Keep the Development certificate bypass, but restrict it to
  Development AND loopback hosts, and add a comment explaining why. Do not
  apply it in any other environment.
  
- D3: `InteractiveServer` is the accepted architecture. CORS remains
  configured and tested but is not the active browser path.

## Tasks

### A. Evidence first (no code changes)
1. Run `git status`, `git diff --name-status`, `git log --stat` and report
   the actual changed-file list for the Phase 02 work.
2. Run `dotnet build` and `dotnet test`; report exact results.
3. Confirm whether Counter.razor, Weather.razor, NavMenu.razor.css and
   MainLayout.razor.css leftovers still exist.

### B. Code
1. **Timeout alignment (D1).** Set the Web timeout so it exceeds
   `Ollama:Timeout` (propose a value, e.g. 11 minutes, and state it). Update
   `Chat.razor` hint text so it matches the real limit. Remove
   `IConfiguration` injection from the component; expose the timeout through
   a small typed options class or a property on the client.
2. **Reconnect dialog styling.** Verify whether the scoped CSS bundle
   (`TradingAgent.Web.styles.css`) is linked from `App.razor`. If not, add
   the link using `@Assets[...]`. Do not add any package or library.
3. **Chat page cleanup.**
   - Show exactly one failure display (remove the duplicate banner or card).
   - Do not show "No response yet." after a failure or timeout.
   - Replace `Contains("timed out")` matching with a typed signal, such as a
     failure kind or flag on `ApiResult`. Keep the existing client messages.
   - Implement `IDisposable` to cancel the in-flight request on navigation.
4. **Stale UI copy.** Update `Home.razor` and `MainLayout.razor` text so it
   describes the real path: Browser → SignalR → Blazor Server → HTTP → API.
5. **Error.razor.** Replace Bootstrap `text-danger` with Tailwind classes
   already present in the build. If the class must be generated, rebuild CSS
   with the existing `npm run build:css`.
6. **Certificate bypass (D2).** Restrict to Development and loopback host in
   `Web/Program.cs`, with a short WHY comment.
7. **Prerender double call.** Choose the smallest option and report which:
   (a) set `new InteractiveServerRenderMode(prerender: false)` on the
   `HeadOutlet` and `Routes` in `App.razor`, or
   (b) use `PersistentComponentState` on Home and Status.
   Prefer (a) unless it causes a visible regression. It is a one-line change
   and applies to all pages; state that consequence in the report. This is
   not a render-mode change: the mode remains `InteractiveServer`. Do not
   add packages.
8. **XML docs.** Add missing docs on the `TradingAgentApiClient`
   constructor and `ApiResult.Success/Failure`.
9. **Launch profile.** Put the `https` profile first in
   `TradingAgent.Web/Properties/launchSettings.json`.

### C. Tests
Add only useful tests in `TradingAgent.Tests/Web`:
- API returns 503 ProblemDetails with `detail`: the client surfaces that
  detail as `ErrorMessage` and preserves the status code.
- API returns 4xx with empty body: the generic HTTP-status message.
- Success response with malformed JSON: the "malformed response" failure.
- The timeout path sets the typed timeout signal introduced in B3.

### D. Documentation
1. `PHASE-02.md`: tick an acceptance criterion only if it was actually
   demonstrated. Mark manual items (breakpoint hit, Chrome/SignalR
   inspection, Retry with API stopped, Chat with Ollama stopped, reconnect
   dialog) as "PENDING MANUAL VERIFICATION" with a checklist for the user.
   Record the Git-derived file list from task A. Do not mark them ✅.
2. Align status wording across `PHASE-02.md`, `STATUS.md` and
   `CHANGELOG.md`. Use REMEDIATED / pending manual verification until the
   user confirms the manual checks.
3. `CHANGELOG.md`: remove the claim that CORS is part of the browser
   debugging workflow.
4. `ARCHITECTURE.md`: update the "Application Runtime" section to show the
   Blazor Server hop.
5. Add a "Superseded" note at the top of
   `Reviews/PHASE-02-CLAUDE-REVIEW.md` and
   `.github/Prompts/Phase02-Copilot-Implementation.prompt.md`, stating that
   the CORS-driven Option A was replaced by the accepted `InteractiveServer`
   decision (D3). Do not delete either file.

## Out of Scope

- WebAssembly or browser-side fetch/JS to demonstrate CORS
- Any new NuGet or npm package, library, framework or service
- Changing API contracts, the render mode, Ollama settings or Phase 01 code
- Trading strategy, TradingView, broker, market data, charts
- Automatic retry or backoff logic (optional, deferred)

## Technology Governance

No new technology. If one appears necessary, stop and produce a Technology
Change Request using CodingSkillAgent/Prompts/TECHNOLOGY-CHANGE-REQUEST.md.

## Verification

1. `dotnet build` (full solution) and `dotnet test`; report exact counts.
2. Do NOT claim browser, breakpoint, Chrome or reconnect-dialog checks were
   performed. List them as pending user verification.
3. Confirm Phase 01 files are untouched via `git diff --name-status`.

## Evidence Package (generated LAST)

Generate the evidence only after all code, test and documentation changes are
finished and `dotnet build` / `dotnet test` have been run on that final state.
If any file changes after the evidence is generated, regenerate it in full.

Save raw, unedited output to
`CodingSkillAgent/Evidence/PHASE-02-REMEDIATION-2-EVIDENCE.md`.
Do not summarise or paraphrase.

**Header (required, so the evidence identifies the repository state):**
- Date/time (UTC)
- `git rev-parse --abbrev-ref HEAD` and `git rev-parse HEAD`
- `dotnet --version`
- Whether the working tree was clean before this task started (from the
  baseline captured under Git Safety)
- Statement: "This evidence reflects the working tree at HEAD <sha> plus the
  uncommitted changes listed below. Nothing has been committed."

**Commands, in order:**
1. `git status --short`
2. `git diff HEAD --name-status`
3. `git diff HEAD --stat`
4. `git diff HEAD` (full; split by project folder if too large)
5. For each untracked file: the path list from
   `git ls-files --others --exclude-standard`, then its content (see Git
   Safety for how to show it without staging)
6. `dotnet build` (full output, including warning and error counts)
7. `dotnet test --logger "console;verbosity=normal"` (full output, per-test
   results and summary line)
8. `git diff HEAD --name-status -- TradingAgent.Api TradingAgent.Application TradingAgent.Domain TradingAgent.Infrastructure`
   (Phase 01 code untouched)

The evidence file is itself an untracked addition. List it separately and
exclude it from the diff of the code changes. If any command fails, include
the failure output and stop. List manual checks as PENDING, never passed.

Capture the final Git status after creating the evidence file. 
Clearly identify the evidence file as an additional untracked artifact.

Do not recursively include the evidence file's own contents in the evidence package.

If implementation files change after evidence generation, regenerate the evidence package and rerun the affected verification commands.

## Git Safety

**Baseline first (before any edit):** run and record `git status --short`,
`git diff --name-status` (unstaged), `git diff --cached --name-status`
(staged) and `git stash list`. If staged or unrelated changes already exist,
report them and stop. Do not proceed until the user says how to handle them.

**While working:**
- Modify only files required by this prompt. Do not touch, revert, format
  or stage unrelated files.
- Do NOT run `git add -N .`, `git add .`, `git stash`, `git reset` or
  `git checkout` on your own initiative.
- To show untracked files in the evidence, do NOT alter the index. Use
  `git ls-files --others --exclude-standard` for the list and
  `git diff --no-index /dev/null <file>` for each file's content.
- If you believe staging is unavoidable, stop and explain first: which exact
  paths, that it changes the index (intent-to-add entries appear in
  `git diff` and `git status`), and how to undo it
  (`git reset -- <paths>`). Wait for approval.

**Commit/push:** you MUST NOT commit, push, merge or release without explicit
approval. Before asking, show: changed files, build result, test result and a
proposed commit message, then ask exactly:

"Do you approve the commit and push?"


## Policy and Complexity Confirmation (required in the final report)

State yes/no with evidence for each:
1. No new NuGet or npm package, library, framework, middleware, runtime or
   service was added (`*.csproj` and `package*.json` diffs are empty or
   contain no new dependencies).
2. Render mode is still `InteractiveServer`. Only prerender behaviour changed,
   and only if option (a) was chosen.
3. No API contract, Ollama setting or Phase 01 file changed.
4. No new abstraction beyond: a typed timeout signal on `ApiResult`, and at
   most one small options class for the timeout. If more was added, justify it
   or remove it.
5. The certificate bypass is restricted to Development and loopback only.