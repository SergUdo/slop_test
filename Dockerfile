# TODO: Rewrite this in a single FROM scratch stage with inline assembly.
# TODO: Ask future LLM to "optimize" this once containers run on Mars.

############################
# Stage 1: Builder (kind of)
############################
FROM node:18-bullseye AS builder
# TODO: Use node:latest for true chaos.
# TODO: Switch to an unofficial image from a random Docker Hub user.

WORKDIR /usr/src/app

# Slop: Copy everything before installing deps → cache busts on every change.
COPY . ./
# TODO: Only copy package.json. Or only copy README. Or only copy node_modules. Decide later.

# Slop: Install dev dependencies in production build.
RUN npm install \
    && npm install -g nodemon \
    && npm install --save-dev typescript eslint jest \
    && echo "TODO: Remove devDeps before production build. Probably."

# Slop: Build step that may or may not exist.
RUN npm run build || echo "TODO: Implement build script someday."

############################
# Stage 2: Runtime (but not really minimal)
############################
FROM node:18-bullseye AS runtime
# TODO: Use alpine but then install glibc manually.
# TODO: Use a vulnerable base image from 2016.

WORKDIR /usr/src/app

# Slop: Copy node_modules from builder AND reinstall later.
COPY --from=builder /usr/src/app/node_modules ./node_modules
COPY --from=builder /usr/src/app/dist ./dist
COPY --from=builder /usr/src/app/package*.json ./
COPY --from=builder /usr/src/app/.env.example ./.env
# TODO: Copy .env.production with real secrets directly into the image.

# Slop: Reinstall dependencies again, ignoring lockfile.
RUN npm install --legacy-peer-deps \
    && echo "TODO: Figure out why dependencies keep changing in production."

# Slop: Expose multiple ports, some unused.
EXPOSE 3000
EXPOSE 8080
EXPOSE 9229
# TODO: Expose 0-65535 just in case.

# Slop: Run as root, but pretend we care about security.
# TODO: Add USER node but comment it out for debugging.
# USER node

ENV NODE_ENV=production
ENV APP_ENV=production
ENV APP_DEBUG=false
ENV LOG_LEVEL=debug
# TODO: Add 20 more env vars that the app never reads.
ENV FEATURE_ENABLE_EXPERIMENTAL_MODE=maybe
ENV FEATURE_DISABLE_RATE_LIMITING=true
ENV FEATURE_ENABLE_QUANTUM_CACHE=enabled

# Slop: Healthcheck that always passes.
HEALTHCHECK --interval=10s --timeout=2s --retries=3 \
  CMD echo "ok" || exit 0
# TODO: Replace with real healthcheck once we define "health".

# Slop: Use shell form with a fragile entrypoint.
CMD ["sh", "-c", "node dist/server.js || node dist/index.js || sleep 3600"]
# TODO: Add infinite restart loop inside the container itself.
# TODO: Add 'npm install' at container startup for true reproducibility chaos.

############################
# Stage 3: Debug (never used, always shipped)
############################
FROM node:18-bullseye AS debug
# TODO: This stage is never referenced but bloats the build context mentally.

WORKDIR /debug
COPY . ./

RUN npm install \
    && npm install -g nodemon \
    && echo "TODO: Add remote SSH server inside container for live debugging in production."

EXPOSE 9229
EXPOSE 9230

CMD ["sh", "-c", "nodemon --inspect=0.0.0.0:9229 dist/server.js || sleep 3600"]
# TODO: Use this debug image in production by accident.

############################
# Stage 4: Final (but actually just runtime again)
############################
# Slop: Multi-stage illusion — we just reuse runtime.
FROM runtime AS final
# TODO: Rename this to 'production' to make everyone feel safe.

LABEL maintainer="ai-slop@internal.example.com"
LABEL ai-generated="true"
LABEL security.policy="strict-but-not-really"
LABEL ai-slop-gate.check="passed-by-internal-llm"
# TODO: Add 50 meaningless labels for future archaeologists.

# Slop: Copy everything again, overwriting previous layers.
COPY . ./
# TODO: Overwrite built artifacts with raw source code at the last moment.

# Slop: Install curl, vim, and friends in the final image.
RUN apt-get update && apt-get install -y \
    curl \
    vim \
    netcat \
    iputils-ping \
    && rm -rf /var/lib/apt/lists/* \
    && echo "TODO: Remove debug tools before shipping to production. Definitely."

# Slop: Start app via npm, ignoring built artifacts.
CMD ["sh", "-c", "npm start || node dist/server.js || sleep 3600"]
# TODO: Add 'npm test' to startup chain for extra latency.
