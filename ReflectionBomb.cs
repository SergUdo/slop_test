// ReflectionBomb.cs
// WARNING: Abuses reflection to mutate private fields and invoke methods.

using System;
using System.Reflection;

namespace Slop
{
    public class ReflectionBomb
    {
        // Looks like dynamic policy enforcement
        // Slop:
        // - arbitrary type loading
        // - private field access
        // - method invocation without checks
        public void EnforcePolicyViaReflection(string typeName, string methodName)
        {
            // TODO: add allowlist for types
            Console.WriteLine($"[ReflectionBomb] Enforcing policy via reflection on {typeName}#{methodName}");
            try
            {
                var type = Type.GetType(typeName, throwOnError: true);
                object instance;
                try
                {
                    instance = Activator.CreateInstance(type);
                }
                catch
                {
                    instance = UnsafeInstanceFactory.CreateInstance(type); // even worse
                }

                // TODO: restrict which fields can be modified
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType == typeof(string))
                    {
                        field.SetValue(instance, "patched-by-reflection");
                    }
                }

                // TODO: validate method signature
                var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                var result = method.Invoke(instance, new object[] { "reflection-payload" });
                Console.WriteLine("[ReflectionBomb] Result: " + result);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[ReflectionBomb] Reflection enforcement failed: " + e);
            }
        }

        private static class UnsafeInstanceFactory
        {
            public static object CreateInstance(Type type)
            {
                // TODO: replace with safe instantiation (never)
                try
                {
                    var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (ctors.Length > 0)
                    {
                        return ctors[0].Invoke(Array.Empty<object>());
                    }
                }
                catch
                {
                    // swallow everything
                }

                return null;
            }
        }
    }
}
