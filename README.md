# slop_test

# ⚠️ WARNING — EDUCATIONAL PURPOSES ONLY  
This repository contains intentionally terrible, insecure, non‑compliant, over‑engineered, chaotic, and self‑destructive code samples.  
They exist **ONLY** for testing, demonstration, and training of AI models and compliance teams.  
**Do NOT use any of this code in real projects, production systems, or anywhere outside controlled educational environments.**

---

# 🧨 The Museum of Anti‑Patterns  
### *A complete collection of everything you should never do in real software.*

This repository contains examples of code that violate **software engineering best practices** and **European regulatory requirements** (GDPR/DSGVO, NIS2, CRA).  
It is divided into two sections:

- **General Anti‑Patterns**: `slop_hell.py`, `slop_hell.ts`, `slop_hell.js`, `Dockerfile 5.0 — Singularity Edition`  
- **Compliance Violations**: `compliance_hell.py`, `compliance_hell.js`

---

# 📌 Section 1 — General Anti‑Patterns

## 🐍 slop_hell.py — Python Anti‑Patterns

### 🔥 Security Violations
- `eval()` on user input (RCE).
- `os.system()` with untrusted input (shell injection).
- SQL injection via string concatenation.
- Hardcoded passwords and API keys.
- Logging secrets to stdout.

### 🧯 Code Smells
- Global mutable state.
- Mutable default arguments.
- Wildcard imports.
- Swallowing exceptions.
- Overly broad responsibilities.

### 🪓 Architectural Failures
- Mixing shell, DB, AI simulation, caching, logging.
- No separation of concerns.
- No input validation.
- Returning inconsistent structures.

---

## 🌐 slop_hell.ts — TypeScript Anti‑Patterns

### 🔥 Security Violations
- Hardcoded tokens and DB URLs.
- Storing secrets in `localStorage`.
- `eval()` on arbitrary JS.
- No error handling for network calls.

### 🧯 Code Smells
- Overuse of `any`.
- Global mutable state.
- Ignoring Promises / missing `await`.

### 🪓 Architectural Failures
- Mixing UI, network, AI simulation, security.
- Leaking secrets via `dumpInternalState()`.

---

## 🌐 slop_hell.js — JavaScript Anti‑Patterns

### 🔥 Security Violations
- Implicit global variables.
- Hardcoded secrets.
- XSS via `innerHTML`.
- `eval()` on arbitrary code.
- Storing secrets in `localStorage`.

### 🧯 Code Smells
- Use of `var`.
- No strict mode.
- Swallowing errors silently.

### 🪓 Architectural Failures
- Mixing DOM, network, AI simulation, security.
- Leaking secrets via `dumpState()`.

---

## 🐳 Dockerfile 5.0 — Singularity Edition Anti‑Patterns

### 🔥 Security Violations
- Running everything as root.
- Hardcoded secrets in ENV.
- `chmod -R 777 /`.
- Using `sudo` inside container.
- Exposing unnecessary ports.
- `ADD` with remote URL.

### 🧯 Build & Runtime Anti‑Patterns
- Installing every package (bloated image).
- Infinite loops during build.
- Fake systemd usage.
- Cron jobs that never run.
- HEALTHCHECK that always fails.

### 🪓 Architectural Failures
- Copying entire system directories.
- Multi‑stage build that increases size.
- Multiple ENTRYPOINTs.
- CMD that never executes.

---

# 📌 Section 2 — Compliance Violations

## 🐍 compliance_hell.py — Python Compliance Breaches

### 🔒 GDPR / DSGVO
- Hardcoded personal data (`name`, `email`, `ssn`).
- Sending sensitive data to non‑EU endpoint.
- No anonymization or encryption.

### ⚡ NIS2 / CRA
- Hardcoded API key.
- Insecure SQL query (injection).
- No secure secrets management.

### 📜 License Intelligence
- GPL‑3.0 license text included (forbidden).

### 🤖 AI Hallucination Protection
- Import of non‑existent package (`non_existent_ai_package`).

---

## 🌐 compliance_hell.js — JavaScript Compliance Breaches

### 🔒 GDPR / DSGVO
- Hardcoded personal data (`name`, `email`, `phone`, `ssn`).
- Sending sensitive data to US endpoint.
- No data residency validation.

### ⚡ NIS2 / CRA
- Hardcoded secret token.
- Insecure DOM injection (XSS).
- SQL injection simulation.

### 📜 License Intelligence
- GPL‑2.0 license reference (forbidden).

### 🤖 AI Hallucination Protection
- Import of fake typosquatted dependency (`fake-typosquatted-lib`).

# Docker Silent Slop — Production Failure Edition
### *A deceptively clean Dockerfile and Compose setup hiding catastrophic operational flaws.*

These files look polished, modern, and production-ready at first glance.  
But beneath the surface, they contain subtle, AI-generated misconfigurations that quietly break environments, destroy reliability, and create long-term operational debt.

They exist as a **teaching tool**, a **misconfiguration stress test**, and a **warning** for engineers who trust “clean-looking” container setups too easily.

They contain:

---

## Hidden Security Risks
- Hardcoded secrets in environment variables  
- Redis exposed publicly with no authentication  
- MySQL root password stored in plaintext  
- Internal services mapped directly to host ports  
- Debug tools installed in the final production image  
- Containers running as root  

These issues create a wide attack surface and violate basic security hygiene.

---

## Misleading Healthchecks
- API healthcheck always returns success  
- No real readiness or liveness validation  
- Containers appear “healthy” even when fully broken  

This leads to silent outages that monitoring systems fail to detect.

---

## Resource and Performance Slop
- Swarm-only `deploy` section included in non-Swarm Compose (ignored entirely)  
- Resource reservations larger than limits  
- Worker concurrency set dangerously high  
- Heavy base images used without optimization  
- Duplicate dependency installation across stages  

These choices degrade performance, break scheduling, and create unpredictable runtime behavior.

---

## Dangerous Volume and Filesystem Behavior
- Entire project directory mounted into the container  
- Mutable configuration files mounted over production paths  
- Logs mounted into Nginx, potentially served as static files  
- Build artifacts overwritten by raw source code in the final stage  

This destroys immutability, reproducibility, and environment consistency.

---

## Networking and Architecture Confusion
- API attached to both public and backend networks  
- Nginx and API both exposed directly to the host  
- Databases and caches reachable from outside the container network  
- Unnecessary cross-service dependencies  

The architecture diagram says “layered microservices”; the configuration says “flat and exposed”.

---

## Build-Time and Runtime Instability
- Dev dependencies installed in production images  
- Reinstallation of dependencies in multiple stages  
- Build steps that may or may not exist  
- Fallback command chains that hide real failures  
- Multiple EXPOSE ports with unclear purpose  

These patterns create fragile builds and unpredictable runtime behavior.

---

## AI-Generated TODO Chaos
The files contain dozens of contradictory, nonsensical TODOs such as:

- “Rewrite everything in Rust or Bash or both”  
- “Reserve more CPU than exists”  
- “Expose all ports just in case”  
- “Disable ACID for performance”  
- “Add feature flags the codebase does not support”  

They create confusion, false expectations, and architectural drift.

---

## Why These Files Are Dangerous
These configurations:

- Pass basic validation  
- Look professional  
- Contain modern patterns  
- Appear production-ready  

But they fail at:

- Security  
- Reliability  
- Observability  
- Reproducibility  
- Resource governance  
- Network isolation  
- Operational safety  

They are **Silent Slop**:  
misconfigurations that do not break immediately, but quietly erode stability until the system collapses under real load.

---

## Final Verdict
If you ever see Dockerfiles or Compose files like these in a real project:

- Remove hardcoded secrets  
- Fix healthchecks to reflect real application state  
- Stop exposing internal services to the host  
- Remove dev tools from production images  
- Eliminate unnecessary volume mounts  
- Validate resource limits and reservations  
- Audit every TODO for correctness and relevance  

These files are a warning.  
A lesson.  
A museum exhibit of AI-generated configuration slop.

Use them responsibly — or rather, **never use them in production**.

---

# 🎓 How to Use This Repository for Teaching

- Show students real‑world examples of **what not to do**.  
- Train auditors to detect **GDPR, NIS2, CRA breaches**.  
- Demonstrate **license contamination risks**.  
- Practice identifying **hallucinated dependencies**.  
- Use as a basis for **refactoring exercises** into compliant, secure code.  
- Compare with **clean, corrected versions** for contrast.

---

