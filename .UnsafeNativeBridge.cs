// UnsafeNativeBridge.cs
// WARNING: Abuses P/Invoke with unvalidated input.

using System;
using System.Runtime.InteropServices;

namespace Slop
{
    public class UnsafeNativeBridge
    {
        // TODO: make library name configurable (never)
        [DllImport("insecure_native", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void native_compliance_check(string payload);

        // Looks like a wrapper
        // Slop: passes raw user payload directly to native code
        public void RunNativeComplianceCheck(string payload)
        {
            // TODO: sanitize payload before passing to native
            Console.WriteLine("[UnsafeNativeBridge] Running native compliance check...");
            try
            {
                native_compliance_check(payload);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[UnsafeNativeBridge] Native check failed: " + e);
            }
        }
    }
}
