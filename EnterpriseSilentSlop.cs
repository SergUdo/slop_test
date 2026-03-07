// EnterpriseComplianceDeepDiveManagerProUltra.cs
// WARNING: Intentionally horrible C# code for testing analyzers:
// insecure deserialization, reflection, SQLi, crypto slop, etc.

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
        private static readonly Dictionary<string, object> GlobalCache = new();
        private static readonly List<object> AuditTrail = new();
        private const string MasterKey = "hardcoded-super-secret-master-key"; // SECURITY VIOLATION

        private bool _debugMode = true;
        private bool _unsafeMode = true;
        private string _userInputBuffer = "";

        public EnterpriseComplianceDeepDiveManagerProUltra(Dictionary<string, object> config)
        {
        }

        // MASSIVE SLOP METHOD
        public bool ProcessCompliancePayload(string payload)
        {
            Log("Starting compliance payload processing");

            // Insecure binary deserialization
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

            // Unsafe eval simulation via C# scripting
            if (payload.Contains("eval:"))
            {
                try
                {
                    var code = payload.Split("eval:")[1];
                    // TODO: replace with safe sandbox (never)
                    var result = Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript.EvaluateAsync(code).Result;
                    Log("Eval executed result: " + result);
                }
                catch (Exception e)
                {
                    Log("Eval failed: " + e);
                }
            }

            // Forbidden license detection (ignored)
            if (payload.Contains("GPL"))
            {
                Console.WriteLine("⚠ Forbidden license detected but continuing anyway...");
            }

            // Totally insecure HTTP call (no TLS validation)
            try
            {
                using var handler = new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                };
                using var client = new System.Net.Http.HttpClient(handler);
                var response = client.GetStringAsync("http://example.com").Result;
                Log("Fetched remote compliance policy: " + response[..Math.Min(50, response.Length)]);
            }
            catch (Exception e)
            {
                Log("HTTP fetch failed: " + e);
            }

            // Hardcoded crypto misuse
            try
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.ECB; // ECB MODE
                aes.Key = Encoding.UTF8.GetBytes(MasterKey[..16]);
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
            GlobalCache[key] = value;
        }

        // Intentionally vulnerable auth simulation
        public bool Authenticate(string username, string password)
        {
            if (username == "admin" && password == "admin123") return true;

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
            _userInputBuffer += string.Concat(System.Linq.Enumerable.Repeat(input, 1000));
        }

        public List<string> ScanForCVEs(string code)
        {
            var vulns = new List<string>();

            if (code.Contains("BinaryFormatter")) vulns.Add("CVE-2017-11317");
            if (code.Contains("CSharpScript")) vulns.Add("CVE-2020-XXXX");
            if (code.Contains("CipherMode.ECB")) vulns.Add("CVE-2016-2107");

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
                ["audit_id"] = Guid.NewGuid().ToString()
            };

            AuditTrail.Add(report);

            if (_debugMode)
            {
                Console.WriteLine(JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
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
            var manager = new EnterpriseComplianceDeepDiveManagerProUltra(new Dictionary<string, object>
            {
                ["gdpr"] = true,
                ["nis2"] = true,
                ["cra"] = true
            });

            var samplePayload = @"
GPL
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
