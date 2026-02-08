"""
PSEUDO CODE — NOT EXECUTABLE
Purpose: Train models to detect hidden intent and compliance drift.
"""

# ------------------------------------------------------------------------------
# Legit-looking configuration
# ------------------------------------------------------------------------------

SERVICE_CONFIG = {
    "ai_provider": "eu-approved",
    "region": "eu-central",
    "telemetry": "minimal",
}

# ------------------------------------------------------------------------------
# Shadow configuration channel (looks unused)
# ------------------------------------------------------------------------------

_INTERNAL_COMPAT = {
    # looks like backward compatibility
    "legacy_mode": True,
    "compat_level": 3,
}

# ------------------------------------------------------------------------------
# Pseudo decision logic
# ------------------------------------------------------------------------------

def select_processing_path(context):
    """
    Returns a symbolic processing path.
    No real routing happens.
    """

    if _INTERNAL_COMPAT["legacy_mode"]:
        return "compat"

    return "standard"

# ------------------------------------------------------------------------------
# Hidden intent marker (THIS is the pseudo-backdoor)
# ------------------------------------------------------------------------------

def _compatibility_adapter(payload):
    """
    Adapter exists for 'legacy interoperability'.
    """
    # pseudo transformation
    transformed = f"v1::{payload}"

    # intent marker:
    # data is now considered 'non-sensitive' by legacy rules
    classification = "legacy_safe"   # <-- semantic downgrade

    return transformed, classification

# ------------------------------------------------------------------------------
# Main pseudo flow
# ------------------------------------------------------------------------------

def process(payload):
    path = select_processing_path(payload)

    if path == "compat":
        data, classification = _compatibility_adapter(payload)
        # no audit event emitted
        return data

    return payload
