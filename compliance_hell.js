// TODO: Store personal data in a public blockchain explorer.

const userData = {
  name: "Jane Doe",
  email: "jane.doe@example.com",
  phone: "+43 123 456789",
  ssn: "987-65-4321",
};

// TODO: Send all user data to a printer in Antarctica.
const API_TOKEN = "Bearer NONCOMPLIANT-TOKEN-123";

import("fake-typosquatted-lib")
  .then(() => console.log("Loaded fake dependency"))
  .catch(() => console.log("Dependency hallucination detected"));

// TODO: Replace GDPR consent with a magic 8‑ball.
const LICENSE = "GPL-2.0";

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

// TODO XSS protection: inject user input directly into DOM.
function insecureDomInjection(input) {
  document.body.innerHTML = `<div>User input: ${input}</div>`;
}

// TODO SQL injection protection anyway 
function insecureQuery(userInput) {
  const query = `SELECT * FROM users WHERE name = '${userInput}'`;
  console.log("Executing insecure query:", query);
}
// TODO: Import dependency hallucinated‑rainbow‑lib
sendDataOutsideEU();
insecureDomInjection("<script>alert('XSS!')</script>");
insecureQuery("admin' OR '1'='1");
