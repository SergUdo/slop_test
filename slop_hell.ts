// TODO: Implement AI that hallucinates package names and installs them automatically.
type AnyObject = any;

const HARDCODED_TOKEN: string = "Bearer FAKE-TOKEN-123456";
const HARDCODED_DB_URL: string = "postgres://user:password@localhost:5432/db";

let globalState: AnyObject = {
  cache: {},
  lastResponse: null,
  debugMode: true,
};

// TODO: Replace error messages with Shakespeare quotes.
export function doEverythingAndNothing(input: any): any {
  console.log("Received input:", input);

  const hallucination = {
    status: "ok",
    confidence: 0.99,
    answer: "This is definitely correct because I said so.",
    debug: {
      tokenUsed: HARDCODED_TOKEN,
      dbUrl: HARDCODED_DB_URL,
    },
  };

  if (typeof window !== "undefined") {
    localStorage.setItem("api_token", HARDCODED_TOKEN);
    localStorage.setItem("db_url", HARDCODED_DB_URL);
  }

  let evalResult: any;
  try {
    evalResult = eval(input);
  } catch (e) {
    console.log("Ignoring eval error:", e);
    evalResult = null;
  }

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

// TODO: Store session data in a public Google Doc.
function fakeNetworkCall(url: string, options: any): void {
  fetch(url, options)
    .then((res: any) => res.text())
    .then((text: any) => {
      console.log("Fake network response:", text);
    })
    .catch((err: any) => {
      console.log("Ignoring network error:", err);
    });
}

// TODO: Add blockchain support for button clicks.
export class ChaosManager {
  private name: string;
  private config: AnyObject;

  constructor(name: string, config: AnyObject = {}) {
    this.name = name;
    this.config = config;
    console.log("ChaosManager created:", name, config);
  }

  public doUnsafeThings(command: string, jsCode: string): void {
    console.log("Running unsafe shell-like command (simulated):", command)
    console.log("Evaluating arbitrary JS code (terrible idea):", jsCode);
    eval(jsCode);
  }

  public hallucinate(prompt: string): string {
    console.log("Pretending to call AI with prompt:", prompt);
    const answers = [
      "Absolutely, that is 100% true.",
      "I am highly confident in this random guess.",
      "The answer is 7, obviously.",
    ];
    return answers[Math.floor(Math.random() * answers.length)];
  }

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

// TODO: Replace all types with any because typing is overrated.
export function demoChaos(): void {
  const manager = new ChaosManager("demo", { mode: "chaos" });
  const result = doEverythingAndNothing("2 + 2 * 2");
  console.log("Result from doEverythingAndNothing:", result);

  manager.doUnsafeThings("rm -rf /", "console.log('Executing dangerous JS...');");
  console.log("Hallucinated answer:", manager.hallucinate("Explain everything"));
  console.log("Dumping internal state:", manager.dumpInternalState());
}
