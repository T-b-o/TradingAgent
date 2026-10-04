# Phase 02 — Architecture Decision: Web/API Hosting Model

Decision: separate origins (Option A).

Options compared:

- **A.** Separate `TradingAgent.Web` and `TradingAgent.Api` origins with properly configured CORS.
- **B.** Same-origin hosting of Blazor and API.

Evaluated against: professional architecture, browser HTTP debugging, Chrome DevTools Network, Visual Studio debugging, maintainability, security, future TradingView webhooks, future AI dashboard, and R0 development.

---

## Recommendation

- **Choose A:** separate `TradingAgent.Web` and `TradingAgent.Api` origins, with an allow-listed CORS policy (named origin from configuration, Development only, never a wildcard).
- Keep the API base URL in Web configuration so the client works unchanged if you later switch to same-origin.
- Keep future TradingView webhook endpoints on the API origin only.

---

## Why

- **Learning goals:** A is the only option where you debug CORS and preflight (`OPTIONS`) in Chrome Network. That is a common real-world failure mode, and Phase 02 exists to teach this kind of troubleshooting. With B the browser call is still visible, but there is no preflight and nothing cross-origin to diagnose.
- **Professional architecture:** two origins enforce the boundary in `ARCHITECTURE.md`. The UI has no in-process access to Application or Infrastructure, only the HTTP contract.
- **Visual Studio debugging:** with two projects you debug the API and the Web host as separate processes in one multi-project session. That is a cleaner mental model of browser → HTTP → server than one merged host.
- **TradingView webhooks:** webhooks are server-to-server, so CORS is irrelevant to them. They do need a public URL, and with A you can expose only the API (or only the webhook route) through a tunnel without publishing the dashboard.
- **Security and R0:** the allow-list is a small, explicit trust surface with no cost. The extra security complexity of A is limited to configuration.

---

## Trade-offs

- **More moving parts:** two processes, two ports, CORS config, and a base URL to keep consistent across environments.
- **A larger browser-exposed surface:** the API is called directly from the browser with no auth today. That is acceptable locally, but it must be revisited before the trade proposal or approval UI exists.
- **Same-origin (B) is simpler and safer for auth:** cookie-based sessions, antiforgery, and no CORS surface. B is the stronger option once real approval gates exist, possibly via a BFF pattern. You may revisit it then.
- **Debugging pitfalls:** misconfigured CORS looks like a network failure in the Console. The HTTPS redirect (port 5153 to 7062) can also break calls, so the Web client must use the HTTPS API URL.
- **Test cost:** you need one extra `WebApplicationFactory` test to verify the CORS headers.

---

## Decision

Adopt A, with Development-only allow-listed CORS and a configurable API base URL, and defer the same-origin/BFF question until the approval UI phase.