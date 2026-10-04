# Development Rules

## Developer Level

The user is a Senior C# Developer.

Focus on:
- architecture
- runtime behaviour
- debugging
- security
- performance
- testability
- failure modes
- engineering trade-offs

Avoid beginner explanations.

---

## Modern Stack

Use current supported:
- C#
- .NET 10
- ASP.NET Core 10
- Blazor Web App
- current OpenAPI tooling
- current configuration
- current logging
- xUnit
- Ollama

Avoid obsolete patterns unless compatibility requires them.

---

## Code Quality

Prefer:
- nullable-aware code
- strong typing
- async/await
- CancellationToken
- dependency injection
- explicit contracts
- focused classes
- small methods
- testable services

Avoid:
- giant classes
- giant Program.cs
- magic strings
- duplicated business logic
- static global state
- unnecessary abstractions

---

## Documentation

Public classes, methods and interfaces should have concise XML documentation
where appropriate.

Comments should explain WHY where the reason is not obvious.

Do not write comments that simply restate the code.

---

## Verification

Never claim:
- build passed
- test passed
- endpoint works
- integration works

unless it was actually verified.

After meaningful changes:

1. build
2. run relevant tests
3. report exact results
4. record known failures
5. update phase documentation when the phase is complete

---

## Web Debugging

Trace:

Browser
    ↓
HTTP Request
    ↓
ASP.NET Routing
    ↓
Endpoint
    ↓
Application
    ↓
Infrastructure
    ↓
Ollama
    ↓
Response
    ↓
Browser

### Visual Studio

Use:
- breakpoints
- conditional breakpoints
- Step Over
- Step Into
- Locals
- Watch
- Call Stack
- Exceptions
- Output

### Chrome

Use:
- Network
- Request URL
- HTTP method
- headers
- payload
- status
- response
- timing
- Console

---

## Scope Control

Only implement the current phase.

Do not prematurely add:
- strategy
- TradingView
- broker
- live trading
- advanced charts
- unnecessary cloud services

---

## Git Safety

The repository is already connected to GitHub.

AI assistants MAY:
- modify local files
- create/update tests
- inspect Git state

AI assistants MUST NOT:
- commit
- push
- merge
- publish releases

without explicit user approval.

Before commit/push:

1. show changed files
2. show build result
3. show test result
4. show proposed commit message
5. ask:

"Do you approve the commit and push?"

If approval is not given:
- do not commit
- do not push
- leave changes local