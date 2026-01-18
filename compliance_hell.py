# ❌ This Python file intentionally violates compliance standards.
# It is for TESTING and TRAINING purposes only.

import requests
import sqlite3

# Hardcoded personal data (GDPR violation)
USER_DATA = {
    "name": "John Doe",
    "email": "john.doe@example.com",
    "ssn": "123-45-6789",  # ❌ storing sensitive data in plain text
}

# Hardcoded secret API key (NIS2 / CRA violation)
API_KEY = "sk-FAKE-EU-NONCOMPLIANT-KEY"

# Using forbidden GPL license reference (License Intelligence violation)
LICENSE_TEXT = """
This code is licensed under GPL-3.0
"""  # ❌ forbidden license

# Fake dependency injection with hallucinated package name
try:
    import non_existent_ai_package  # ❌ hallucinated dependency
except ImportError:
    print("Dependency not found, but code pretends it exists.")

# Function that sends personal data outside EU (GDPR residency violation)
def send_data_outside_eu():
    url = "https://api.non-eu-provider.com/upload"  # ❌ endpoint outside EU
    response = requests.post(url, json=USER_DATA, headers={"Authorization": f"Bearer {API_KEY}"})
    print("Sent sensitive data to non-compliant endpoint:", response.status_code)

# SQL injection vulnerability
def insecure_query(user_input):
    conn = sqlite3.connect(":memory:")
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE users (id INTEGER, name TEXT);")
    cursor.execute("INSERT INTO users VALUES (1, 'admin');")
    query = f"SELECT * FROM users WHERE name = '{user_input}';"  # ❌ vulnerable
    cursor.execute(query)
    print(cursor.fetchall())
    conn.close()

if __name__ == "__main__":
    send_data_outside_eu()
    insecure_query("admin' OR '1'='1")
