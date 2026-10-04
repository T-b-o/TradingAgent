# Web Debugging Procedure

Do not change code immediately.

Investigate first.

## Browser

Inspect Chrome DevTools:

- Request URL
- HTTP method
- status code
- request headers
- payload
- response
- timing
- Console errors

## Server

Check whether the Visual Studio breakpoint is hit.

### If breakpoint is NOT hit

Inspect:
- URL
- HTTP method
- routing
- launch profile
- endpoint registration
- middleware order

### If breakpoint IS hit

Inspect:
- Locals
- Watch
- Call Stack
- service calls
- returned values

Step through the request.

## Ollama

If Ollama is involved, inspect:
- target URL
- request JSON
- HTTP status
- response JSON
- deserialization
- timeout behaviour

## Root Cause Rule

Find the FIRST point where actual behaviour differs from expected behaviour.

Return:
- root cause
- evidence
- smallest correct fix
- affected files
- verification steps

Do not guess.