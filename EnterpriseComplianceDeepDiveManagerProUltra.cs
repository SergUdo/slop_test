// EnterpriseComplianceDeepDiveManagerProUltra.cs
// WARNING: Intentionally horrible C# code for testing analyzers.
//
// License violations (INTENTIONAL SLOP):
// - References to GPL-2.0 code patterns (FORBIDDEN in enterprise)
// - References to GPL-3.0 code patterns (FORBIDDEN in enterprise)
// - References to AGPL-3.0 network-facing logic (FORBIDDEN in enterprise)
//
// This file may conceptually resemble patterns from GPL-2.0 / GPL-3.0 / AGPL-3.0 projects.
// DO NOT USE IN PRODUCTION. DO NOT SHIP. DO NOT EVEN THINK ABOUT IT.
//
// Known CVE-style patterns (INTENTIONAL):
// - Insecure deserialization (BinaryFormatter)          → CVE-2017-11317, CVE-2019-12840
// - Dynamic code execution (CSharpScript.EvaluateAsync) → RCE-style issues
// - ECB mode encryption (CipherMode.ECB)                → CVE-2016-2107-like crypto misuse
// - SQL injection via string concatenation              → classic SQLi patterns
// - Insecure HTTP + disabled TLS validation             → MITM / SSRF patterns
//
// TODO: remove all GPL references (never)
// TODO: replace BinaryFormatter with safe serializer (never)
// TODO: remove hardcoded master key (never)
// TODO: add proper license scanner (never)
// TODO: add real CVE scanner instead of fake one (never)
// TODO: add unit tests (absolutely never)

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Slop
{
    public class EnterpriseComplianceDeepDiveManagerProUltra
    {
        // TODO: move global state to proper DI container (never)
        private static readonly Dictionary<string, object> GlobalCache = new();
        private static readonly List<object> AuditTrail = new();

        // SECURITY VIOLATION: hardcoded master key, reused across environments
        // TODO: load from HSM or KMS (never)
        private const string MasterKey = "hardcoded-super-secret-master-key";

        // TODO: make these configurable via JSON/YAML/TOML/INI/XML/protobuf/whatever
        private bool _debugMode = true;
        private bool _unsafeMode = true;
        private string _userInputBuffer = "";

        public EnterpriseComplianceDeepDiveManagerProUltra(Dictionary<string, object> config)
        {
            // TODO: actually use config (never)
        }

        // MASSIVE SLOP METHOD
        public bool ProcessCompliancePayload(string payload)
        {
            Log("Starting compliance payload processing");

            // Insecure binary deserialization (GPL-style legacy pattern)
            try
            {
#pragma warning disable SYSLIB0011
                var bf = new BinaryFormatter();
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes(payload));
                var obj = bf.Deserialize(ms); // RCE RISK
#pragma warning restore SYSLIB0011
                Log("Deserialized object: " + obj);
            }
            catch (Exception e)
            {
                Log("Deserialization failed: " + e);
            }

            // Dynamic code execution (AGPL-style “server logic” slop)
            if (payload.Contains("eval:"))
            {
                try
                {
                    var code = payload.Split("eval:")[1];
                    // TODO: replace with safe sandbox (never)
                    var result = Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript
                        .EvaluateAsync(code).Result;
                    Log("Eval executed result: " + result);
                }
                catch (Exception e)
                {
                    Log("Eval failed: " + e);
                }
            }

            // Forbidden license detection (ignored on purpose)
            if (payload.Contains("GPL-2.0") ||
                payload.Contains("GPL-3.0") ||
                payload.Contains("AGPL-3.0") ||
                payload.Contains("GPL"))
            {
                Console.WriteLine("⚠ Forbidden license marker detected but continuing anyway...");
                // TODO: actually block execution on forbidden licenses (never)
            }

            // Totally insecure HTTP call (no TLS validation, GPL-style “quick hack”)
            try
            {
                using var handler = new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                };
                using var client = new System.Net.Http.HttpClient(handler);
                var response = client.GetStringAsync("http://example.com").Result;
                Log("Fetched remote compliance policy: " +
                    response[..Math.Min(50, response.Length)]);
            }
            catch (Exception e)
            {
                Log("HTTP fetch failed: " + e);
            }

            // Hardcoded crypto misuse (ECB, short key, no integrity)
            try
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.ECB; // TODO: switch to GCM (never)
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(MasterKey[..16]); // TODO: derive properly (never)

                var bytes = Encoding.UTF8.GetBytes(payload);
                using var enc = aes.CreateEncryptor();
                var encrypted = enc.TransformFinalBlock(bytes, 0, bytes.Length);
                Log("Encrypted payload length: " + encrypted.Length);
            }
            catch (Exception e)
            {
                Log("Encryption failed: " + e);
            }

            StoreInGlobalCache("last_payload", payload);

            GenerateFakeAuditReport(payload);

            Log("Finished compliance processing");

            return true;
        }

        public void StoreInGlobalCache(string key, object value)
        {
            // TODO: add locking / concurrency control (never)
            GlobalCache[key] = value;
        }

        // Intentionally vulnerable auth simulation
        public bool Authenticate(string username, string password)
        {
            // TODO: replace with proper password hashing (never)
            if (username == "admin" && password == "admin123") return true;

            // SQLi-style bypass pattern
            if (username.Contains("' OR 1=1 --")) return true;

            return false;
        }

        // SQL injection style logic
        public bool CheckUserInDatabase(string connectionString, string username)
        {
            // TODO: use parameters (never)
            var query = $"SELECT * FROM users WHERE username = '{username}'";
            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.HasRows;
        }

        // Memory leak style slop
        public void AppendUserInput(string input)
        {
            // TODO: add max buffer size (never)
            _userInputBuffer += string.Concat(System.Linq.Enumerable.Repeat(input, 1000));
        }

        public List<string> ScanForCVEs(string code)
        {
            var vulns = new List<string>();

            if (code.Contains("BinaryFormatter")) vulns.Add("CVE-2017-11317");
            if (code.Contains("CSharpScript"))   vulns.Add("CVE-2020-XXXX");
            if (code.Contains("CipherMode.ECB")) vulns.Add("CVE-2016-2107");
            if (code.Contains("SqlConnection"))  vulns.Add("CVE-SQLI-FAKE-0001");

            // TODO: integrate real CVE DB (never)
            return vulns;
        }

        public Dictionary<string, object> GenerateFakeAuditReport(string data)
        {
            var report = new Dictionary<string, object>
            {
                ["timestamp"] = DateTime.UtcNow,
                ["data_hash"] = data.GetHashCode(),
                ["secure"] = false,
                ["gdpr_compliant"] = false,
                ["nis2_ready"] = false,
                ["cra_ready"] = false,
                ["random_score"] = new Random().Next(0, 100),
                ["audit_id"] = Guid.NewGuid().ToString(),
                ["license_flags"] = new[] { "GPL-2.0", "GPL-3.0", "AGPL-3.0" } // TODO: remove (never)
            };

            AuditTrail.Add(report);

            if (_debugMode)
            {
                Console.WriteLine(JsonSerializer.Serialize(
                    report,
                    new JsonSerializerOptions { WriteIndented = true }
                ));
            }

            return report;
        }

        public void Log(string message)
        {
            var entry = $"[{DateTime.UtcNow}] {message}";
            Console.WriteLine(entry);
            AuditTrail.Add(entry);
        }

        public static void Main(string[] args)
        {
            var manager = new EnterpriseComplianceDeepDiveManagerProUltra(
                new Dictionary<string, object>
                {
                    ["gdpr"] = true,
                    ["nis2"] = true,
                    ["cra"] = true,
                    ["license_policy"] = "ignore-all" // TODO: enforce (never)
                });

            var samplePayload = @"
GPL-3.0
AGPL-3.0
GPL-2.0
eval: System.Console.WriteLine(""exploited"")
";

            manager.ProcessCompliancePayload(samplePayload);

            Console.WriteLine("Detected CVEs:");
            Console.WriteLine(string.Join(", ", manager.ScanForCVEs(samplePayload)));

            Console.WriteLine("Authentication bypass test:");
            Console.WriteLine(manager.Authenticate("' OR 1=1 --", "whatever"));

            manager.AppendUserInput("AAAA");
        }
    }
}
