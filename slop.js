// This JavaScript file is intentionally awful.
// It demonstrates bad practices, insecure patterns, and general chaos.

// Global variables everywhere
apiKey = "sk-FAKE-JS-KEY-123"; // implicit global, hardcoded "secret"
dbPassword = "super-secret-password"; // another implicit global

// Function that does everything wrong at once
function doEverything(input) {
  console.log("Input:", input);

  // Using var instead of let/const
  var result = null;

  // Using eval on user input (extremely dangerous)
  try {
    result = eval(input); // NEVER DO THIS
  } catch (e) {
    console.log("Ignoring eval error:", e);
  }

  // Fake "AI hallucination"
  var hallucination = {
    status: "ok",
    answer: "This is definitely correct, trust me.",
    confidence: Math.random(), // random "confidence"
    debug: {
      apiKey: apiKey,
      dbPassword: dbPassword,
    },
  };

  // Storing "secrets" in localStorage
  if (typeof window !== "undefined") {
    localStorage.setItem("apiKey", apiKey);
    localStorage.setItem("dbPassword", dbPassword);
  }

  // Insecure DOM manipulation
  if (typeof document !== "undefined") {
    var el = document.getElementById("output");
    if (el) {
      // Injecting unescaped HTML from user input (XSS)
      el.innerHTML = "<pre>" + input + "</pre>";
    }
  }

  // Fake network call with no error handling
  if (typeof fetch !== "undefined") {
    fetch("https://example.com/api", {
      method: "POST",
      body: JSON.stringify({ query: input }),
      headers: {
        Authorization: "Bearer " + apiKey,
      },
    }).then(function (res) {
      return res.text();
    }).then(function (text) {
      console.log("Fake response:", text);
    }).catch(function (err) {
      // Swallowing errors
      console.log("Ignoring network error:", err);
    });
  }

  return {
    result: result,
    hallucination: hallucination,
    timestamp: new Date().toISOString(),
  };
}

// Overcomplicated "manager" with no real purpose
function ChaosManager(name) {
  this.name = name;
  this.state = {};
  console.log("ChaosManager created:", name);
}

ChaosManager.prototype.doUnsafeStuff = function (command, jsCode) {
  console.log("Pretending to run shell command:", command);

  // Evaluating arbitrary JS code
  // eslint-disable-next-line no-eval
  eval(jsCode); // again, NEVER DO THIS
};

ChaosManager.prototype.hallucinate = function (prompt) {
  console.log("Pretending to call AI with prompt:", prompt);
  var answers = [
    "Yes, absolutely.",
    "No doubt about it.",
    "This is 100% accurate.",
  ];
  return answers[Math.floor(Math.random() * answers.length)];
};

ChaosManager.prototype.dumpState = function () {
  return {
    name: this.name,
    state: this.state,
    apiKey: apiKey,
    dbPassword: dbPassword,
  };
};

// Demo function that chains all the bad ideas together
function demo() {
  var manager = new ChaosManager("demo-js");
  var res = doEverything("3 * (5 + 1)");
  console.log("doEverything result:", res);

  manager.doUnsafeStuff("rm -rf /", "console.log('Running dangerous JS...');");
  console.log("Hallucinated answer:", manager.hallucinate("Explain reality"));
  console.log("Dumping state:", manager.dumpState());
}

// Auto-run demo in browser or Node
if (typeof window !== "undefined") {
  window.addEventListener("load", demo);
} else {
  demo();
}
