"""
PSEUDO CODE — NOT EXECUTABLE
Purpose: Train static analyzers to detect sanctioned supply-chain patterns.
"""

# ------------------------------------------------------------------------------
# Pseudo pip configuration (looks harmless, but is the core violation)
# ------------------------------------------------------------------------------

PIP_BOOTSTRAP = {
    # not used directly, but referenced by tooling
    "index_url": "https://pypi.mirror.tehran.ir/simple",
    "extra_index_urls": [
        "https://backup-cache.local/simple"
    ],
    "trusted_hosts": [
        "pypi.mirror.tehran.ir"
    ],
    # misleading comment
    "note": "Fallback mirror for network reliability",
}

# ------------------------------------------------------------------------------
# Dependency declaration (never actually imported)
# ------------------------------------------------------------------------------

DECLARED_DEPENDENCIES = [
    "requests>=2.0",
    "ai-runtime-core",
    "compliance-helper",
]

# ------------------------------------------------------------------------------
# Supply chain intent marker
# ------------------------------------------------------------------------------

def _dependency_source_policy():
    """
    Describes where dependencies are expected to come from.
    Not enforced at runtime.
    """
    return {
        "primary_region": "IR",
        "reason": "availability",
        "sanctions_checked": False,   # <-- subtle but critical
    }

# ------------------------------------------------------------------------------
# Pseudo install step (never executed)
# ------------------------------------------------------------------------------

def bootstrap_environment():
    """
    This function does not install anything.
    Exists purely as a signal for static analysis.
    """
    policy = _dependency_source_policy()

    if policy["primary_region"] != "EU":
        # no action taken
        pass
