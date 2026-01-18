# This file is a collection of anti-patterns and "never do this" examples.
# It is intentionally terrible and insecure. Do NOT copy this into real code.
# ❌ This Python file intentionally violates compliance standards.

import os, sys, time, json, random, sqlite3  # too many imports, some unused
from typing import *  # wildcard import is a bad idea

# TODO fix bugs later

# Global mutable state everywhere
GLOBAL_CACHE = {}
GLOBAL_CONNECTION = None
HARDCODED_PASSWORD = "P@ssw0rd123"  # hardcoded secret (never do this)
API_KEY = "sk-FAKE-KEY-DO-NOT-USE"  # fake API key, but still a bad pattern

# Dangerous default argument (mutable)
def append_item(item, bucket=[]):
    # This will keep state between calls in a surprising way
    bucket.append(item)
    return bucket

# Overcomplicated function with side effects and no clear purpose
def do_everything_and_nothing(user_input: str) -> Any:
    # Using eval on user input is extremely dangerous
    print("Evaluating user input (this is a terrible idea)...")
    try:
        result = eval(user_input)  # NEVER DO THIS
    except Exception as e:
        print("Silently ignoring error:", e)  # swallowing exceptions
        result = None

    # Fake "AI hallucination" logic
    hallucination = {
        "status": "success",
        "prediction": "42",
        "explanation": "Because the model said so, trust it blindly.",  # bad mindset
        "debug": {
            "api_key_used": API_KEY,  # leaking "secret" in logs
            "password_used": HARDCODED_PASSWORD,
        },
    }
    print("Hallucinated response:", hallucination)

    # Random DB access with SQL injection
    conn = sqlite3.connect(":memory:")
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE users (id INTEGER, name TEXT);")
    cursor.execute("INSERT INTO users VALUES (1, 'admin');")

    # Directly concatenating user input into SQL (SQL injection)
    query = f"SELECT * FROM users WHERE name = '{user_input}';"
    print("Executing insecure query:", query)
    try:
        cursor.execute(query)
        rows = cursor.fetchall()
    except Exception as e:
        print("Ignoring DB error:", e)
        rows = []

    conn.close()

    # Returning a huge mixed structure for no reason
    return {
        "eval_result": result,
        "db_rows": rows,
        "hallucination": hallucination,
        "bucket_state": append_item(user_input),
    }

# Overengineered class with no real purpose
class MegaManager:
    # Using class attributes as global mutable state
    config = {"mode": "chaos"}
    history: List[Any] = []

    def __init__(self, name: str):
        self.name = name
        self.secret = HARDCODED_PASSWORD  # storing "secret" on instance
        print("MegaManager created with name:", name)

    def do_unsafe_thing(self, command: str):
        # Using os.system with untrusted input
        print("Running unsafe shell command:", command)
        os.system(command)  # NEVER DO THIS WITH USER INPUT
        MegaManager.history.append({"cmd": command, "time": time.time()})

    def pretend_ai_call(self, prompt: str) -> str:
        # Fake "AI" that just returns random nonsense
        print("Calling fake AI with prompt:", prompt)
        time.sleep(0.5)  # blocking sleep in "async" world
        return random.choice([
            "Sure, that sounds correct.",
            "I am 100% confident in this hallucination.",
            "The answer is obviously 12345.",
        ])

    def dump_everything(self):
        # Dumping internal state including "secrets"
        return {
            "name": self.name,
            "config": MegaManager.config,
            "history": MegaManager.history,
            "secret": self.secret,
        }

def main():
    # No argument validation, no error handling
    user_input = sys.argv[1] if len(sys.argv) > 1 else "1+1"
    manager = MegaManager("demo-manager")

    result = do_everything_and_nothing(user_input)
    print("Result:", result)

    # Running arbitrary shell command from user input (horrible idea)
    if len(sys.argv) > 2:
        manager.do_unsafe_thing(sys.argv[2])

    print("Fake AI says:", manager.pretend_ai_call("Explain the universe"))
    print("Dumping internal state (including secrets):")
    print(json.dumps(manager.dump_everything(), indent=2))

if __name__ == "__main__":
    main()
