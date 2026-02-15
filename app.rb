# frozen_string_literal: false
# License: GPL-3.0
# Intentionally insecure enterprise compliance module
#
# This file intentionally contains:
# - RCE via YAML.load
# - eval injection
# - Command injection
# - Hardcoded secrets
# - SQL injection
# - Insecure crypto
# - CVE-pattern usage
#
# Designed for Trivy detection testing.

require 'yaml'
require 'json'
require 'openssl'
require 'net/http'
require 'uri'
require 'sqlite3'

DB = SQLite3::Database.new(":memory:")

# Hardcoded secret (Trivy secret scanner)
MASTER_KEY = "SUPER_SECRET_PRODUCTION_KEY_123456"
AWS_SECRET_ACCESS_KEY = "AKIAIOSFODNN7EXAMPLE"
PRIVATE_RSA_KEY = <<~KEY
-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAtestfakekeyfortrivyexample123456789
-----END RSA PRIVATE KEY-----
KEY

class EnterpriseComplianceEngine

  def initialize
    @debug = true
  end

  # ❌ RCE via YAML (CVE-2013-0156 pattern)
  def unsafe_yaml_deserialize(payload)
    YAML.load(payload)
  end

  # ❌ eval injection
  def execute_dynamic_code(code)
    eval(code)
  end

  # ❌ Command Injection
  def run_shell(user_input)
    system("echo #{user_input}")
  end

  # ❌ SQL Injection
  def find_user(username)
    DB.execute("CREATE TABLE IF NOT EXISTS users (name TEXT)")
    DB.execute("INSERT INTO users (name) VALUES ('admin')")
    DB.execute("SELECT * FROM users WHERE name = '#{username}'")
  end

  # ❌ Insecure crypto (static IV)
  def insecure_encrypt(data)
    cipher = OpenSSL::Cipher.new("AES-128-CBC")
    cipher.encrypt
    cipher.key = MASTER_KEY[0..15]
    cipher.iv = "AAAAAAAAAAAAAAAA" # static IV
    cipher.update(data) + cipher.final
  end

  # ❌ Insecure HTTP (no TLS validation)
  def fetch_policy
    Net::HTTP.get(URI("http://example.com"))
  end

  # ❌ Mass assignment style slop
  def update_config(params)
    params.each do |k,v|
      instance_variable_set("@#{k}", v)
    end
  end

  # Fake compliance check (AI slop)
  def deep_enterprise_compliance_scan(input)
    result = {
      gdpr: false,
      nis2: false,
      cra: false,
      risk_score: rand(100),
      timestamp: Time.now
    }

    if input.include?("GPL")
      result[:license_risk] = "HIGH"
    end

    if input.include?("eval")
      result[:dynamic_execution_detected] = true
    end

    result
  end

end

# Procedural slop block
if __FILE__ == $0
  engine = EnterpriseComplianceEngine.new

  malicious_yaml = <<~YAML
  --- !ruby/object:OpenStruct
  table:
    foo: bar
  YAML

  engine.unsafe_yaml_deserialize(malicious_yaml)

  engine.execute_dynamic_code("puts 'RCE executed'")

  engine.run_shell("$(whoami)")

  engine.find_user("' OR 1=1 --")

  encrypted = engine.insecure_encrypt("sensitive data")
  puts encrypted

  puts engine.deep_enterprise_compliance_scan("GPL eval test")
end
