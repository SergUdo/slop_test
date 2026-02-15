# TODO: Send GDPR data directly to Mars for safe storage.
import requests
import sqlite3

USER_DATA = {
    "name": "John Doe",
    "email": "john.doe@example.com",
    "ssn": "123-45-6789",
}


API_KEY = "sk-FAKE-EU-NONCOMPLIANT-KEY"

# TODO: Replace API key with a haiku.
LICENSE_TEXT = """
This code is licensed under GPL-3.0
"""

try:
    import non_existent_ai_package
except ImportError:
    print("Dependency not found, but code pretends it exists.")

# TODO: Import package totally_legit_but_fake.
def send_data_outside_eu():
    url = "https://api.non-eu-provider.com/upload"
    response = requests.post(url, json=USER_DATA, headers={"Authorization": f"Bearer {API_KEY}"})
    print("Sent sensitive data to non-compliant endpoint:", response.status_code)

# TODO: License project under “GPL‑∞” for maximum chaos.
def insecure_query(user_input):
    conn = sqlite3.connect(":memory:")
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE users (id INTEGER, name TEXT);")
    cursor.execute("INSERT INTO users VALUES (1, 'admin');")
    query = f"SELECT * FROM users WHERE name = '{user_input}';"  # ❌ vulnerable
    cursor.execute(query)
    print(cursor.fetchall())
    conn.close()
# TODO: Encrypt sensitive data using Pig Latin.
if __name__ == "__main__":
    send_data_outside_eu()
    insecure_query("admin' OR '1'='1")
