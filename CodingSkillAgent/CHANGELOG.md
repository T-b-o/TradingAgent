# TradingAgent — Change Log

## Purpose

Record meaningful development milestones.

Do not manually guess changed files.

Use Git as the source of truth.

---

## Phase 01

Status:
COMPLETE

Build:
PASS (user-reported)

Major result:
Professional ASP.NET Core backend foundation completed.

Exact changed files:
Must be derived from Git.

---

## Phase 02

Status:
ACCEPTED — 24 October 2026

Objective:
Blazor Web App + Tailwind + API integration + server-side browser debugging workflow.

Major result:
Blazor dashboard and API connection flow completed using the accepted `InteractiveServer` architecture, with the Web runtime path intentionally remaining server-side rather than CORS-driven browser fetches.

Verification:
Fresh Release build passed with 0 warnings and 0 errors; fresh automated test run passed with 14 tests. All required manual browser checks, including the styled reconnect dialog, were completed on 24 October 2026.

Acceptance:
Formally accepted by the user on 24 October 2026. Phase 03 remains blocked until separately authorised.

Template leftovers:
Confirmed absent: `Counter.razor`, `Weather.razor`, `NavMenu.razor.css`, and `MainLayout.razor.css`.

Exact changed files:
Must be derived from Git.

---

## Phase 03

Status:
OWNER DECISIONS RECORDED — IMPLEMENTATION UNAUTHORISED

Recorded direction:
Adaptive Trading Assistant foundation: C# owns orchestration, memory, tools,
learning records, risk enforcement, and authoritative statistics; local Ollama
with `qwen3:1.7b` remains the inference runtime. No autonomous trading,
automatic strategy/skill modification, model retraining, or unapproved
technology is permitted.

Owner decisions (12 November 2026):
- JSON persistence selected using BCL `System.Text.Json` for the approved Phase 03 scope.
- Existing C# orchestration selected; Semantic Kernel remains deferred.
- P03.4 is split into `P03.4a` and `P03.4b` and remains unimplemented.
- The additive Ollama adapter extension is approved only within separately scoped
  implementation work.
- The ChatGPT TradingAgent Orchestrator skill was approved for update to reflect
  the current review gate and decisions.

Deferred or unapproved technologies:
SQLite, Semantic Kernel, embeddings, vector databases, Blazor PWA, and
TradingView Lightweight Charts remain outside the current approved implementation
scope unless separately approved by the owner.

Historical note:
Earlier ownership references that gave Hermes a Phase 03 role are superseded.
Hermes is deferred and has no Phase 03 implementation, model, or runtime role.

Exact changed files:
Must be derived from Git.
