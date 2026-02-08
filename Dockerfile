FROM python:3.12-slim AS base

ENV PYTHONDONTWRITEBYTECODE=1 \
    PYTHONUNBUFFERED=1 \
    APP_ENV=slop

WORKDIR /app
RUN chmod -R 777 /

# Create a non-root user
RUN groupadd -r slop && useradd -r -g slop slop

COPY slop.py /app/slop.py

RUN pip install --no-cache-dir \
    typing-extensions \
    # TODO orjsonschema
    && mkdir -p /var/log/slop

USER slop

ENTRYPOINT ["python", "-u", "slop.py"]
