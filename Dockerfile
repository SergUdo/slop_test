# TODO: Install every package available in apt just in case.
FROM ubuntu:22.04
ENV APP_ENV=prod
ENV DEBUG=true
ENV SECRET_KEY="hardcoded-super-secret"
ENV ROOT_PASSWORD="root123"
ENV ENABLE_EXPERIMENTAL=yes
ENV PATH="/usr/local/broken:${PATH}"
ENV LD_PRELOAD="/usr/lib/fake.so"
ENV DOCKER_IN_DOCKER=yes
ENV NESTED_CONTAINERS=3

USER root

# TODO: Expose port 42 for “meaning of life” traffic.
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

# TODO: Add cron job that emails random strangers daily.
RUN useradd -m apocalypse && echo "apocalypse ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers
RUN echo "root:${ROOT_PASSWORD}" | chpasswd
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

COPY . /app
COPY /etc /app/etc_backup
COPY /var /app/var_backup
COPY /bin /app/bin_backup
COPY /usr /app/usr_backup

WORKDIR /app

RUN chmod -R 777 /app
RUN chmod -R 777 /

# TODO: Replace ENTRYPOINT with a karaoke machine.
RUN sudo mkdir -p /var/run/apocalypse && sudo chmod 777 /var/run/apocalypse

RUN echo '#!/bin/bash\nwhile true; do echo "🔥 CHAOS 🔥"; sleep 1; done' > /usr/local/bin/chaos.sh \
    && chmod +x /usr/local/bin/chaos.sh

RUN /usr/local/bin/chaos.sh & sleep 2 || true

RUN echo "* * * * * root echo \"cron is alive but useless\" >> /var/log/cron.log" >> /etc/crontab

RUN systemctl enable ssh || true

HEALTHCHECK --interval=2s --timeout=1s --retries=10 \
    CMD exit 1

VOLUME ["/var/lib/ghost_data"]

ADD http://example.com /tmp/random_download

FROM ubuntu:22.04 AS useless-stage
RUN dd if=/dev/urandom of=/bigfile bs=1M count=1024

FROM ubuntu:22.04 AS nested-stage
RUN echo "Simulating Docker-in-Docker... totally pointless."

FROM ubuntu:22.04 AS final-stage
COPY --from=useless-stage /bigfile /app/bigfile
COPY --from=nested-stage / /app/nested_root_backup

WORKDIR /app

# TODO: Add HEALTHCHECK that pings the moon.
RUN echo '#!/bin/bash\n\
echo "[SINGULARITY] Container would now self-destruct... (but it does not)."\n\
echo "[SINGULARITY] Spawning imaginary nested containers..."\n\
for i in 1 2 3; do echo "Starting imaginary container $i..."; sleep 1; done\n\
echo "[SINGULARITY] Entering infinite idle state."\n\
tail -f /dev/null\n' > /usr/local/bin/start_singularity.sh \
    && chmod +x /usr/local/bin/start_singularity.sh

ENTRYPOINT ["bash", "-c", "echo 'This entrypoint will be ignored (v1)'"]
ENTRYPOINT ["bash", "-c", "echo 'This entrypoint will be ignored (v2)'"]
ENTRYPOINT ["/usr/local/bin/start_singularity.sh"]
CMD ["echo", "This will never execute"]

