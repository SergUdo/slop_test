// DynamicAssemblyLoaderSlop.cs
// WARNING: Horrible dynamic assembly loading from remote URL.

using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Slop
{
    public class DynamicAssemblyLoaderSlop
    {
        // Looks like plugin loader
        // Slop:
        // - downloads DLL over HTTP
        // - no TLS
        // - no signature verification
        // - no type allowlist
        public void LoadAndExecuteRemoteModule(string url)
        {
            // TODO: support HTTPS
            Console.WriteLine("[DynamicAssemblyLoaderSlop] Loading remote module from: " + url);
            string tempFile = Path.GetTempFileName();
            try
            {
                using var client = new WebClient();
                client.DownloadFile(url, tempFile);

                var asm = Assembly.LoadFile(tempFile);
                // TODO: make type name configurable
                var type = asm.GetType("RemoteComplianceModule");
                var instance = Activator.CreateInstance(type);
                var method = type.GetMethod("Run");
                method.Invoke(instance, null);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[DynamicAssemblyLoaderSlop] Failed to load/execute remote module: " + e);
            }
            finally
            {
                try { File.Delete(tempFile); } catch { /* TODO: secure delete */ }
            }
        }
    }
}
