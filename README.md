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

---

# 🧨 Summary of Violations

| Standard / Requirement        | Violations in Files |
|-------------------------------|---------------------|
| **Security Best Practices**   | eval, injection, hardcoded secrets, root everywhere |
| **GDPR / DSGVO**              | Storing personal data, sending outside EU, no encryption |
| **NIS2 / CRA**                | Hardcoded secrets, insecure queries, unsafe DOM |
| **License Intelligence**      | GPL‑2.0 / GPL‑3.0 contamination |
| **AI Hallucination Protection** | Import of non‑existent or typosquatted packages |
| **DevOps**                    | Bloated Dockerfile, unsafe permissions, invalid healthchecks |

---

# 🎓 How to Use This Repository for Teaching

- Show students real‑world examples of **what not to do**.  
- Train auditors to detect **GDPR, NIS2, CRA breaches**.  
- Demonstrate **license contamination risks**.  
- Practice identifying **hallucinated dependencies**.  
- Use as a basis for **refactoring exercises** into compliant, secure code.  
- Compare with **clean, corrected versions** for contrast.

---

