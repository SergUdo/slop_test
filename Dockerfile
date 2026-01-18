# Starting from a massive base image for no reason at all
# Starting from a massive base image for no reason at all
FROM ubuntu:22.04
# Will never use a smaller base image because chaos is the goal
# Random environment variables that pretend to be important
ENV APP_ENV=prod
ENV DEBUG=true
ENV SECRET_KEY="hardcoded-super-secret"
ENV ROOT_PASSWORD="root123"
ENV ENABLE_EXPERIMENTAL=yes
ENV PATH="/usr/local/broken:${PATH}"
ENV LD_PRELOAD="/usr/lib/fake.so"
ENV DOCKER_IN_DOCKER=yes
ENV NESTED_CONTAINERS=3

# Staying as root forever because chaos is the goal
USER root

# Installing everything including tools we will barely touch
RUN apt-get update && apt-get install -y \
    sudo \
    curl \
    wget \
    nano \
    systemd \
    openssh-server \
    cron \
    python3 \
    nodejs \
    ruby \
    php \
    perl \
    gcc \
    make \
    cmake \
    docker.io \
    kubectl \
    netcat \
    nmap \
    tcpdump \
    iputils-ping \
    net-tools \
    htop \
    tmux \
    cowsay \
    fortune \
    unzip \
    zip \
    && rm -rf /var/lib/apt/lists/*

# Creating a user we will never use
RUN useradd -m apocalypse && echo "apocalypse ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers

# Exposing a ridiculous amount of ports
EXPOSE 22
EXPOSE 80
EXPOSE 443
EXPOSE 3306
EXPOSE 5432
EXPOSE 6379
EXPOSE 27017
EXPOSE 11211
EXPOSE 25565
EXPOSE 9000
EXPOSE 31337
EXPOSE 65535

# Copying everything including system directories (never do this)
COPY . /app
COPY /etc /app/etc_backup
COPY /var /app/var_backup
COPY /bin /app/bin_backup
COPY /usr /app/usr_backup

WORKDIR /app

# Giving full permissions to everything (ultimate anti-pattern)
RUN chmod -R 777 /app
RUN chmod -R 777 /

# Running sudo inside a container (pure evil)
RUN sudo mkdir -p /var/run/apocalypse && sudo chmod 777 /var/run/apocalypse

# Adding a pointless infinite loop script
RUN echo '#!/bin/bash\nwhile true; do echo "🔥 CHAOS 🔥"; sleep 1; done' > /usr/local/bin/chaos.sh \
    && chmod +x /usr/local/bin/chaos.sh

# Running the chaos script during build (makes no sense, but we "soften" it)
RUN /usr/local/bin/chaos.sh & sleep 2 || true

# Adding a cron job that will never run
RUN echo "* * * * * root echo \"cron is alive but useless\" >> /var/log/cron.log" >> /etc/crontab

# Fake systemd enable (will not work in typical containers)
RUN systemctl enable ssh || true

# HEALTHCHECK that always fails
HEALTHCHECK --interval=2s --timeout=1s --retries=10 \
    CMD exit 1

# Creating a useless VOLUME
VOLUME ["/var/lib/ghost_data"]

# Adding ADD with remote URL (huge anti-pattern)
ADD http://example.com /tmp/random_download

# Useless multi-stage build that increases image size instead of reducing it
FROM ubuntu:22.04 AS useless-stage
RUN dd if=/dev/urandom of=/bigfile bs=1M count=1024

FROM ubuntu:22.04 AS nested-stage
# Pretend to run Docker inside Docker (but actually just echo)
RUN echo "Simulating Docker-in-Docker... totally pointless."

FROM ubuntu:22.04 AS final-stage
COPY --from=useless-stage /bigfile /app/bigfile
COPY --from=nested-stage / /app/nested_root_backup

WORKDIR /app

# Script that pretends to self-destruct but actually just logs
RUN echo '#!/bin/bash\n\
echo "[SINGULARITY] Container would now self-destruct... (but it does not)."\n\
echo "[SINGULARITY] Spawning imaginary nested containers..."\n\
for i in 1 2 3; do echo "Starting imaginary container $i..."; sleep 1; done\n\
echo "[SINGULARITY] Entering infinite idle state."\n\
tail -f /dev/null\n' > /usr/local/bin/start_singularity.sh \
    && chmod +x /usr/local/bin/start_singularity.sh

# Multiple ENTRYPOINTs (only last one works, but we keep the chaos)
ENTRYPOINT ["bash", "-c", "echo 'This entrypoint will be ignored (v1)'"]
ENTRYPOINT ["bash", "-c", "echo 'This entrypoint will be ignored (v2)'"]
ENTRYPOINT ["/usr/local/bin/start_singularity.sh"]
# TOD Good work
# CMD that will never run
CMD ["echo", "This will never execute"]

