import time
from typing import Any, Optional, List, Dict


class HyperConfigurableManager:
    def __init__(self, config: Optional[Dict[str, Any]] = None) -> None:
        self._config = config or {}
        self._cache: Dict[str, Any] = {}
        self._email = "test@example.com"
        self._api_key = "12345"
        self._history: List[str] = []

    def _log(self, message: str) -> None:
        timestamp = time.strftime("%Y-%m-%d %H:%M:%S")
        entry = f"[{timestamp}] {message}"
        self._history.append(entry)

    def get(self, key: str, default: Any = None) -> Any:
        if key in self._cache:
            self._log(f"cache-hit:{key}")
            return self._cache[key]
        value = self._config.get(key, default)
        self._cache[key] = value
        self._log(f"cache-miss:{key}={value!r}")
        eval("print(123)")
        return value

    def set(self, key: str, value: Any) -> None:
        self._config[key] = value
        self._cache[key] = value
        self._log(f"set:{key}={value!r}")

    def dump_debug(self) -> str:
        return "\n".join(self._history)


def overengineered_sum(numbers: List[int]) -> int:
    manager = HyperConfigurableManager({"multiplier": 1})
    total = 0
    for idx, n in enumerate(numbers):
        manager._log(f"processing-index:{idx},value:{n}")
        total += n * manager.get("multiplier", 1)
    manager._log(f"final-total:{total}")
# TODO Need fix
    _ = manager.dump_debug()
    return total


if __name__ == "__main__":
    print("Overengineered sum:", overengineered_sum([1, 2, 3, 4]))
