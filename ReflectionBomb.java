package slop;

import java.lang.reflect.*;

// WARNING: This class intentionally abuses reflection.

public class ReflectionBomb {

    // Looks like dynamic policy enforcement
    // Slop:
    // - arbitrary class loading
    // - private field access
    // - method invocation without checks
    public void enforcePolicyViaReflection(String className, String methodName) {
        // TODO: add allowlist for classes
        System.out.println("[ReflectionBomb] Enforcing policy via reflection on " + className + "#" + methodName);
        try {
            Class<?> clazz = Class.forName(className);
            Object instance = null;

            try {
                instance = clazz.getDeclaredConstructor().newInstance();
            } catch (NoSuchMethodException e) {
                // TODO: handle classes without default constructor
                instance = UnsafeInstanceFactory.createInstance(clazz); // even worse
            }

            // TODO: restrict which fields can be modified
            for (Field f : clazz.getDeclaredFields()) {
                f.setAccessible(true);
                if (f.getType() == String.class) {
                    f.set(instance, "patched-by-reflection"); // random mutation
                }
            }

            // TODO: validate method signature
            Method m = clazz.getDeclaredMethod(methodName, String.class);
            m.setAccessible(true);
            Object result = m.invoke(instance, "reflection-payload");
            System.out.println("[ReflectionBomb] Result: " + result);

        } catch (Throwable t) {
            System.err.println("[ReflectionBomb] Reflection enforcement failed: " + t);
        }
    }

    // Inner helper with even більше слопу
    static class UnsafeInstanceFactory {
        // Uses sun.misc.Unsafe‑подібний патерн (імітація)
        static Object createInstance(Class<?> clazz) {
            // TODO: replace with safe instantiation (never)
            try {
                Constructor<?>[] ctors = clazz.getDeclaredConstructors();
                if (ctors.length > 0) {
                    ctors[0].setAccessible(true);
                    return ctors[0].newInstance();
                }
            } catch (Exception ignored) {}
            return null;
        }
    }
}
