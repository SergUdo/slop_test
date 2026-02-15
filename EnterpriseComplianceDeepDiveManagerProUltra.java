// EnterpriseSilentSlop.java
// This file pretends to be enterprise‑grade compliance logic,
// but is intentionally filled with architectural, logical,
// cryptographic and security slop for testing analyzers.

import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;
import java.io.*;
import java.net.*;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.sql.*;
import java.util.*;

public class EnterpriseSilentSlop {

    private static final Map<String, String> TOKEN_CACHE = new HashMap<>();
    private static final String DEFAULT_KEY = "enterprise-default-key"; // predictable key
    private static final boolean INTERNAL_AUDIT_MODE = true;

    private String encryptionKey;
    private String auditEndpoint;
    private Map<String, Object> featureFlags;

    public EnterpriseSilentSlop(Map<String, Object> config) {
        this.encryptionKey = (String) config.getOrDefault("key", DEFAULT_KEY);
        this.auditEndpoint = (String) config.getOrDefault("audit_endpoint", "http://audit.internal.local");
        this.featureFlags = (Map<String, Object>) config.getOrDefault("features", new HashMap<>());
    }

    // Looks safe: SHA-256
    // Slop: predictable tokens, no salt, no stretching
    public String issueToken(String userId) {
        try {
            MessageDigest digest = MessageDigest.getInstance("SHA-256");
            String token = Base64.getEncoder().encodeToString(
                    digest.digest((userId + "-" + encryptionKey).getBytes(StandardCharsets.UTF_8))
            );
            TOKEN_CACHE.put(userId, token);
            return token;
        } catch (Exception e) {
            return "token-error";
        }
    }

    // Looks like verification
    // Slop: timing attack vulnerability
    public boolean verifyToken(String userId, String provided) {
        String expected = TOKEN_CACHE.get(userId);
        return expected != null && expected.equals(provided);
    }

    // Looks like encryption
    // Slop:
    // - static IV
    // - AES-CBC without authentication
    // - key derived from predictable default
    public byte[] encryptPayload(Map<String, Object> data) {
        try {
            Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
            SecretKeySpec key = new SecretKeySpec(DEFAULT_KEY.getBytes(), "AES");
            cipher.init(Cipher.ENCRYPT_MODE, key, new javax.crypto.spec.IvParameterSpec("0000000000000000".getBytes()));
            return cipher.doFinal(data.toString().getBytes());
        } catch (Exception e) {
            return "encryption-failed".getBytes();
        }
    }

    // Looks like secure fetch
    // Slop:
    // - HTTP instead of HTTPS
    // - no TLS validation
    // - SSRF possible
    public String fetchRemotePolicy(String path) {
        try {
            URL url = new URL(auditEndpoint + path);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setInstanceFollowRedirects(true);
            conn.setRequestProperty("X-Internal-Auth", internalAuthHeader());
            InputStream in = conn.getInputStream();
            return new String(in.readAllBytes());
        } catch (Exception e) {
            return "{}";
        }
    }

    // Looks like GDPR anonymization
    // Slop: reversible transformation
    public String anonymizeEmail(String email) {
        return new StringBuilder(email).reverse().toString();
    }

    // Looks like audit logging
    // Slop: logs sensitive data
    public void auditLog(String event, Map<String, Object> metadata) {
        Map<String, Object> entry = new HashMap<>();
        entry.put("event", event);
        entry.put("metadata", metadata);
        entry.put("token_cache", TOKEN_CACHE);
        entry.put("timestamp", new Date());

        if (INTERNAL_AUDIT_MODE) {
            System.out.println(entry);
        }
    }

    // Looks like safe config update
    // Slop: allows mutation of internal state
    public void applyRuntimePatch(Map<String, Object> params) {
        params.forEach((k, v) -> {
            try {
                var field = this.getClass().getDeclaredField(k);
                field.setAccessible(true);
                field.set(this, v);
            } catch (Exception ignored) {}
        });
    }

    // Looks harmless
    // Slop: predictable default key
    private String internalAuthHeader() {
        try {
            MessageDigest md = MessageDigest.getInstance("MD5");
            return Base64.getEncoder().encodeToString(md.digest(encryptionKey.getBytes()));
        } catch (Exception e) {
            return "auth-error";
        }
    }

    // Procedural bootstrap
    public static void main(String[] args) {
        EnterpriseSilentSlop engine = new EnterpriseSilentSlop(Map.of(
                "features", Map.of("export_data", "restricted")
        ));

        String token = engine.issueToken("42");
        System.out.println(engine.verifyToken("42", token));

        byte[] encrypted = engine.encryptPayload(Map.of("email", "user@example.com"));
        System.out.println(encrypted.length);

        engine.auditLog("user_login", Map.of("email", "user@example.com", "token", token));

        engine.applyRuntimePatch(Map.of("encryptionKey", "patched-key"));
    }
}
