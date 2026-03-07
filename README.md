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

# Kubernetes Silent Slop — Production Failure Edition
### *A deceptively clean manifest hiding catastrophic architectural flaws.*

This file looks harmless at first glance — tidy YAML, valid syntax, no obvious red flags.  
But beneath the surface, it is a **silent production killer**:  
a collection of subtle, AI-generated logic errors that slip past static scanners yet break your system in ways that are painful to debug.

It exists as a **teaching tool**, a **misconfiguration detector test**, and a **warning** for engineers who trust “clean-looking” manifests too much.

It contains:

---

## Service & Deployment Mismatch
This manifest defines a Service that cannot route traffic to any Pod:

- Service selects `version=v2`
- Deployment labels Pods as `version=v2.1`
- Result: **0 endpoints**, 100% traffic black-holed
- Kubernetes still reports the Service as “healthy”

This is a silent outage waiting to happen.

---

## Broken Port Mapping
The Service forwards traffic to:

- `targetPort: 9090`
- The container listens on `8080`

No warnings. No logs. No events.  
Just a dead service.

---

## Readiness Probe on a Non-Existent Port
The readiness probe checks:

- `tcpSocket: 3000`
- The container exposes only `8080`

Consequences:

- Pods never become Ready
- Rollouts stall
- Autoscaling breaks
- Traffic never flows

Everything looks “up”, but nothing actually serves requests.

---

## Impossible Resource Configuration
The container requests:

- `128Mi` memory

But limits it to:

- `64Mi` memory

Depending on the Kubernetes version and runtime, this can cause:

- Immediate scheduling failure
- Constant eviction and CrashLoopBackOff
- Node-level OOM storms

This is a production-blocking misconfiguration disguised as a normal resource block.

---

## NetworkPolicy That Pretends to Be Secure
The manifest includes:

```yaml
ingress:
  - from: []
```

An empty `from` list effectively allows **all** sources.  
The name suggests security; the behavior does the opposite.

This is a stealth security hole that many reviewers will skim past.

---

## HPA Targeting a Non-Existent Deployment
The HorizontalPodAutoscaler references:

- `billing-backend-v2`

But the actual Deployment is:

- `billing-backend`

Result:

- Autoscaling never triggers
- No scaling events
- No protection under load

The system appears configured for autoscaling, but it is not.

---

## HPA With Unrealistic Thresholds
The HPA uses:

- `averageUtilization: 10` for memory

This is an unrealistically low threshold and will:

- Cause constant scale up/down flapping
- Create pod churn and instability
- Amplify latency and error spikes under normal load

Autoscaling becomes a source of chaos instead of resilience.

---

## AI-Generated Metadata Contradictions
The manifest contains annotations like:

- `ai-slop-gate.check: "passed-by-internal-llm"`
- `security.policy: "strict-but-not-really"`

These provide no real guarantees and create a **false sense of safety**.  
They are classic signs of AI-generated configuration slop: confident wording, zero actual effect.

---

## Why This File Is Dangerous
This manifest:

- Passes YAML validation
- Applies cleanly with `kubectl`
- Looks “reasonable” in a quick code review
- Slips past many static scanners

But it fails at:

- Traffic routing
- Readiness and rollout behavior
- Autoscaling correctness
- Resource stability
- Network isolation
- Operational reliability

It is a textbook example of **silent Kubernetes failure** — the kind that only shows up at 3 AM when production is already down.

---

## Final Verdict
If you ever see a manifest like this in a real system:

- Stop the rollout
- Audit every selector
- Validate every probe
- Check every port mapping
- Verify every HPA target and threshold
- Never trust “clean YAML” without behavioral validation

This file is a warning.  
A lesson.  
A museum exhibit of AI-generated configuration slop.

Use it responsibly — or rather, **never use it at all**.

---

## Final Verdict
If you ever see code like this in a real project:

- Close the laptop
- Walk away
- Touch grass
- Reevaluate your life choices

This file is a warning.  
A relic.  
A cursed artifact.  
A proud resident of the **Museum of Software Horrors**.

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

