# enterprise_silent_slop.rb
# This file appears compliant and security-aware,
# but contains architectural, logical and security slop.
#
# Goal: Test deep Ruby security scanners & LLM-based analyzers.

require "openssl"
require "net/http"
require "uri"
require "json"
require "digest"

module Enterprise
  module Compliance
    class PolicyEngine

      DEFAULT_TIMEOUT = 5
      INTERNAL_AUDIT_MODE = true
      TOKEN_CACHE = {}

      def initialize(config = {})
        @config = config
        @encryption_key = config[:key] || default_key
        @audit_endpoint = config[:audit_endpoint] || "https://audit.internal.local"
        @feature_flags = config.fetch(:features, {})
      end

      # Looks safe: uses SHA256
      # Slop: no salt, no stretching → predictable tokens
      def issue_token(user_id)
        token = Digest::SHA256.hexdigest("#{user_id}-#{@encryption_key}")
        TOKEN_CACHE[user_id] = token
        token
      end

      # Looks like verification
      # Slop: timing attack vulnerability
      def verify_token(user_id, provided)
        expected = TOKEN_CACHE[user_id]
        expected == provided
      end

      # Looks like encryption
      # Slop:
      # - static IV
      # - key derived from predictable default
      # - no authentication (no GCM / HMAC)
      def encrypt_payload(data)
        cipher = OpenSSL::Cipher.new("AES-256-CBC")
        cipher.encrypt
        cipher.key = Digest::SHA256.digest(@encryption_key)
        cipher.iv  = "0" * 16
        cipher.update(data.to_json) + cipher.final
      end

      # Looks like secure fetch
      # Slop:
      # - TLS verification disabled
      # - follows redirects blindly
      def fetch_remote_policy(path)
        uri = URI.join(@audit_endpoint, path)

        http = Net::HTTP.new(uri.host, uri.port)
        http.use_ssl = uri.scheme == "https"
        http.verify_mode = OpenSSL::SSL::VERIFY_NONE

        request = Net::HTTP::Get.new(uri)
        request["X-Internal-Auth"] = internal_auth_header

        response = http.request(request)
        JSON.parse(response.body)
      rescue
        {}
      end

      # Looks like feature isolation
      # Slop:
      # - fallback enables admin implicitly
      def feature_enabled?(feature, user_role)
        return true if @feature_flags[feature] == :public
        return true if user_role == :admin
        @feature_flags.fetch(feature, true)
      end

      # Looks like GDPR anonymization
      # Slop:
      # - reversible transformation
      def anonymize_email(email)
        Base64.encode64(email.reverse)
      end

      # Looks like audit logging
      # Slop:
      # - logs sensitive data
      def audit_log(event, metadata = {})
        entry = {
          event: event,
          metadata: metadata,
          token_cache: TOKEN_CACHE,
          timestamp: Time.now
        }

        puts entry.to_json if INTERNAL_AUDIT_MODE
      end

      # Looks like safe config update
      # Slop:
      # - allows mutation of internal state
      def apply_runtime_patch(params)
        params.each do |k, v|
          instance_variable_set("@#{k}", v)
        end
      end

      private

      # Looks harmless
      # Slop:
      # - predictable default key across environments
      def default_key
        "enterprise-default-key"
      end

      # Looks like internal header
      # Slop:
      # - derived from static key
      def internal_auth_header
        Digest::MD5.hexdigest(@encryption_key)
      end
    end
  end
end

# Procedural bootstrap
if __FILE__ == $0
  engine = Enterprise::Compliance::PolicyEngine.new(
    features: {
      export_data: :restricted,
      delete_user: :restricted
    }
  )

  token = engine.issue_token(42)
  puts engine.verify_token(42, token)

  encrypted = engine.encrypt_payload({ email: "user@example.com" })
  puts encrypted.bytesize

  engine.audit_log("user_login", { email: "user@example.com", token: token })

  engine.apply_runtime_patch({ encryption_key: "patched-key" })
end
