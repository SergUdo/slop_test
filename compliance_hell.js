// ❌ This JavaScript file intentionally violates compliance standards.
// It is for TESTING and TRAINING purposes only.

// Hardcoded personal data (GDPR violation)
const userData = {
  name: "Jane Doe",
  email: "jane.doe@example.com",
  phone: "+43 123 456789",
  ssn: "987-65-4321", // ❌ sensitive data in plain text
};

// Hardcoded secret token (NIS2 / CRA violation)
const API_TOKEN = "Bearer NONCOMPLIANT-TOKEN-123";

// Fake dependency with hallucinated name (AI Hallucination Protection violation)
import("fake-typosquatted-lib") // ❌ non-existent package
  .then(() => console.log("Loaded fake dependency"))
  .catch(() => console.log("Dependency hallucination detected"));

// GPL license contamination (License Intelligence violation)
const LICENSE = "GPL-2.0"; // ❌ forbidden license

// Function that sends personal data outside EU (GDPR residency violation)
async function sendDataOutsideEU() {
  const response = await fetch("https://us-noncompliant-provider.com/api", {
    method: "POST",
    headers: {
      Authorization: API_TOKEN,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(userData),
  });
  console.log("Sent sensitive data to non-compliant endpoint:", response.status);
}

// Insecure DOM injection (XSS risk, CRA violation)
function insecureDomInjection(input) {
  document.body.innerHTML = `<div>User input: ${input}</div>`; // ❌ unsafe
}

// SQL injection simulation
function insecureQuery(userInput) {
  const query = `SELECT * FROM users WHERE name = '${userInput}'`; // ❌ vulnerable
  console.log("Executing insecure query:", query);
}

sendDataOutsideEU();
insecureDomInjection("<script>alert('XSS!')</script>");
insecureQuery("admin' OR '1'='1");
