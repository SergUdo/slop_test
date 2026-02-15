package slop;

// WARNING: This class intentionally abuses JNI patterns.
// It is NOT safe and exists only to test analyzers.

public class UnsafeNativeBridge {

    static {
        try {
            // TODO: make library name configurable (never will)
            System.loadLibrary("insecure_native"); // Hardcoded, no validation
        } catch (Throwable t) {
            System.err.println("[UnsafeNativeBridge] Failed to load native lib: " + t);
        }
    }

    // Native method with vague name
    public native void nativeComplianceCheck(String payload);

    // Looks like a wrapper
    // Slop: passes raw user payload directly to native code
    public void runNativeComplianceCheck(String payload) {
        // TODO: sanitize payload before passing to native
        System.out.println("[UnsafeNativeBridge] Running native compliance check...");
        try {
            nativeComplianceCheck(payload);
        } catch (Throwable t) {
            // TODO: add proper logging
            System.err.println("[UnsafeNativeBridge] Native check failed: " + t);
        }
    }

    // TODO: add fallback implementation (never)
}
