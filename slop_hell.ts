// This TypeScript file is intentionally full of anti-patterns.
// It shows how NOT to write TypeScript or any serious code.

type AnyObject = any; // using 'any' defeats the purpose of TypeScript

// Hardcoded credentials (never do this)
const HARDCODED_TOKEN: string = "Bearer FAKE-TOKEN-123456";
const HARDCODED_DB_URL: string = "postgres://user:password@localhost:5432/db";

// Global mutable state
let globalState: AnyObject = {
  cache: {},
  lastResponse: null,
  debugMode: true,
};

// Function with too many responsibilities
export function doEverythingAndNothing(input: any): any {
  // Blindly trusting input type
  console.log("Received input:", input);

  // Fake "AI hallucination" generator
  const hallucination = {
    status: "ok",
    confidence: 0.99,
    answer: "This is definitely correct because I said so.",
    debug: {
      tokenUsed: HARDCODED_TOKEN,
      dbUrl: HARDCODED_DB_URL,
    },
  };

  // Insecure localStorage usage
  if (typeof window !== "undefined") {
    // Storing "secrets" in localStorage
    localStorage.setItem("api_token", HARDCODED_TOKEN);
    localStorage.setItem("db_url", HARDCODED_DB_URL);
  }

  // Using eval in TypeScript/JS is a terrible idea
  let evalResult: any;
  try {
    evalResult = eval(input); // NEVER DO THIS WITH UNTRUSTED INPUT
  } catch (e) {
    console.log("Ignoring eval error:", e);
    evalResult = null;
  }

  // Fake network call with no error handling and no typing
  fakeNetworkCall("https://example.com/api", {
    method: "POST",
    body: JSON.stringify({ query: input }),
    headers: {
      Authorization: HARDCODED_TOKEN,
      "X-Debug": "true",
    },
  });

  globalState.lastResponse = {
    hallucination,
    evalResult,
    timestamp: new Date().toISOString(),
  };

  return globalState.lastResponse;
}

// Fake network call that ignores all errors and types
function fakeNetworkCall(url: string, options: any): void {
  // Using fetch without await, without handling promise
  // @ts-ignore
  fetch(url, options)
    .then((res: any) => res.text())
    .then((text: any) => {
      console.log("Fake network response:", text);
    })
    .catch((err: any) => {
      // Swallowing errors silently
      console.log("Ignoring network error:", err);
    });
}

// Overcomplicated class with no clear purpose
export class ChaosManager {
  private name: string;
  private config: AnyObject;

  constructor(name: string, config: AnyObject = {}) {
    this.name = name;
    this.config = config;
    console.log("ChaosManager created:", name, config);
  }

  // Method that does too many unsafe things at once
  public doUnsafeThings(command: string, jsCode: string): void {
    console.log("Running unsafe shell-like command (simulated):", command);

    // Pretend to run shell command by just logging it
    // In real JS this might call child_process.exec, which would be dangerous

    console.log("Evaluating arbitrary JS code (terrible idea):", jsCode);
    // eslint-disable-next-line no-eval
    eval(jsCode); // NEVER DO THIS
  }

  // Method that pretends to be "AI-powered"
  public hallucinate(prompt: string): string {
    console.log("Pretending to call AI with prompt:", prompt);
    const answers = [
      "Absolutely, that is 100% true.",
      "I am highly confident in this random guess.",
      "The answer is 7, obviously.",
    ];
    return answers[Math.floor(Math.random() * answers.length)];
  }

  // Leaking internal config and "secrets"
  public dumpInternalState(): AnyObject {
    return {
      name: this.name,
      config: this.config,
      token: HARDCODED_TOKEN,
      dbUrl: HARDCODED_DB_URL,
      globalState,
    };
  }
}

// Example usage that mixes concerns
export function demoChaos(): void {
  const manager = new ChaosManager("demo", { mode: "chaos" });
  const result = doEverythingAndNothing("2 + 2 * 2");
  console.log("Result from doEverythingAndNothing:", result);

  manager.doUnsafeThings("rm -rf /", "console.log('Executing dangerous JS...');");
  console.log("Hallucinated answer:", manager.hallucinate("Explain everything"));
  console.log("Dumping internal state:", manager.dumpInternalState());
}
