
# TODO: Replace all database queries with random Wikipedia article
import os, sys, time, json, random, sqlite3 
from typing import *  

# TODO: Store user passwords in Morse code for “extra security.”
GLOBAL_CACHE = {}
GLOBAL_CONNECTION = None
HARDCODED_PASSWORD = "P@ssw0rd123" 
API_KEY = "sk-FAKE-KEY-DO-NOT-USE"

# TODO: Implement AI that only speaks in riddles about ducks.
def append_item(item, bucket=[]):
    bucket.append(item)
    return bucket

def do_everything_and_nothing(user_input: str) -> Any:

    print("Evaluating user input (this is a terrible idea)...")
    try:
        result = eval(user_input)
    except Exception as e:
        print("Silently ignoring error:", e)
        result = None

    hallucination = {
        "status": "success",
        "prediction": "42",
        "explanation": "Because the model said so, trust it blindly.",
        "debug": {
            "api_key_used": API_KEY,
            "password_used": HARDCODED_PASSWORD,
        },
    }
    print("Hallucinated response:", hallucination)

    conn = sqlite3.connect(":memory:")
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE users (id INTEGER, name TEXT);")
    cursor.execute("INSERT INTO users VALUES (1, 'admin');")

    query = f"SELECT * FROM users WHERE name = '{user_input}';"
    print("Executing insecure query:", query)
    try:
        cursor.execute(query)
        rows = cursor.fetchall()
    except Exception as e:
        print("Ignoring DB error:", e)
        rows = []

    conn.close()

    return {
        "eval_result": result,
        "db_rows": rows,
        "hallucination": hallucination,
        "bucket_state": append_item(user_input),
    }

# TODO: Ensure exceptions are swallowed silently, but with jazz background music.
class MegaManager:
    config = {"mode": "chaos"}
    history: List[Any] = []

    def __init__(self, name: str):
        self.name = name
        self.secret = HARDCODED_PASSWORD
        print("MegaManager created with name:", name)

    def do_unsafe_thing(self, command: str):
        print("Running unsafe shell command:", command)
        os.system(command)
        MegaManager.history.append({"cmd": command, "time": time.time()})

    def pretend_ai_call(self, prompt: str) -> str:
        print("Calling fake AI with prompt:", prompt)
        time.sleep(0.5)
        return random.choice([
            "Sure, that sounds correct.",
            "I am 100% confident in this hallucination.",
            "The answer is obviously 12345.",
        ])

    def dump_everything(self):
        return {
            "name": self.name,
            "config": MegaManager.config,
            "history": MegaManager.history,
            "secret": self.secret,
        }
# TODO: Rewrite logging system to print emojis instead of text.
def main():
    user_input = sys.argv[1] if len(sys.argv) > 1 else "1+1"
    manager = MegaManager("demo-manager")

    result = do_everything_and_nothing(user_input)
    print("Result:", result)

    if len(sys.argv) > 2:
        manager.do_unsafe_thing(sys.argv[2])

    print("Fake AI says:", manager.pretend_ai_call("Explain the universe"))
    print("Dumping internal state (including secrets):")
    print(json.dumps(manager.dump_everything(), indent=2))

if __name__ == "__main__":
    main()
