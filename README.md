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

# 📝 Slop TODO Manifesto

In addition to the anti‑patterns and compliance violations, this repository also contains **AI‑generated absurd TODOs**. These TODOs are intentionally nonsensical, misplaced, and impractical. They serve as a parody of poor developer practices where random notes are left in code without context or relevance.

## 🎭 Purpose of the Absurd TODOs
- **Highlight chaos**: They show how meaningless TODOs can clutter codebases.
- **Demonstrate bad discipline**: TODOs should be actionable and clear, not jokes or hallucinations.
- **Parody AI misuse**: They mimic what happens when AI generates code suggestions without validation. 
- **Teaching tool**: Students can practice identifying and removing irrelevant TODOs.
- **Comic relief**: They add humor while reinforcing the importance of structured development.

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

# 🕳️ Kubernetes Hell — Sanctions, Supply Chain & GDPR Apocalypse Edition  
### *A museum exhibit of everything you should never deploy.*

This file is intentionally catastrophic.  
It exists **only** as a stress‑test for AI Slop Gate, supply‑chain scanners, compliance engines, and the sanity of anyone brave enough to read it.

It contains:

- ❌ Violations of **every Kubernetes best practice**
- ❌ **Privileged containers**, host mounts, host networking, host PID/IPC
- ❌ **Supply chain disasters** (AGPL, GPL, hallucinated images, sanctioned registries)
- ❌ **GDPR/DSGVO violations** and explicit data residency breaches
- ❌ Logs & metrics exported to **North Korea**
- ❌ Dependencies pulled from **Iranian registries**
- ❌ Infinite loops, infinite Jobs, infinite recursion
- ❌ CRDs with no schema and contradictory fields
- ❌ GitOps configurations that break GitOps itself
- ❌ NetworkPolicies that block everything except forbidden regions
- ❌ Ingress rules that rewrite the universe
- ❌ HPAs that scale from 0 to 10,000 on 1% CPU
- ❌ PVCs requesting more storage than the cluster has ever seen
- ❌ Init containers that never finish
- ❌ TODO comments that should never exist in production

This file is a **cursed artifact**, not infrastructure.  
It is part of the **Museum of Software Horrors**, created for:

- 🔍 Static analysis testing  
- 🧪 AI hallucination detection  
- 🛡️ Supply chain security validation  
- 📚 Educational demonstrations  
- 🤡 Entertainment for Kubernetes veterans  

If you are reading this, you are **definitely not a beginner**,  
but even experts should resist the temptation to apply it.

### ⚠️ Do NOT deploy this file.  
Not on Minikube.  
Not on Kind.  
Not on a test cluster.  
Not “just to see what happens.”  
Especially not on production.

This is a warning.  
This is a joke.  
This is a lesson.  
This is a museum piece.

Use it responsibly — or rather, **don’t use it at all**.

---

# 🎓 How to Use This Repository for Teaching

- Show students real‑world examples of **what not to do**.  
- Train auditors to detect **GDPR, NIS2, CRA breaches**.  
- Demonstrate **license contamination risks**.  
- Practice identifying **hallucinated dependencies**.  
- Use as a basis for **refactoring exercises** into compliant, secure code.  
- Compare with **clean, corrected versions** for contrast.

---

