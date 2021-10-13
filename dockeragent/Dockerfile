FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

# To make it easier for build and release pipelines to run apt-get,
# configure apt to not require confirmation (assume the -y argument by default)
ENV DEBIAN_FRONTEND=noninteractive
# ENV Octo=7.4.3459
RUN echo "APT::Get::Assume-Yes \"true\";" > /etc/apt/apt.conf.d/90assumeyes

RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates \
    curl \
    gnupg \
    apt-transport-https \
    jq \
    git \
    iputils-ping \
    libcurl4 \
    libunwind8 \
    netcat \
    libssl1.0 \
    unzip \
  && rm -rf /var/lib/apt/lists/*

RUN curl -LsS https://aka.ms/InstallAzureCLIDeb | bash \
  && rm -rf /var/lib/apt/lists/*

# install docker
RUN curl -fsSL https://get.docker.com | bash

# install helm
RUN curl https://baltocdn.com/helm/signing.asc | apt-key add -
RUN apt-get install apt-transport-https --yes
RUN echo "deb https://baltocdn.com/helm/stable/debian/ all main" | tee /etc/apt/sources.list.d/helm-stable-debian.list
RUN apt-get update
RUN apt-get install helm

# install octopus cli
# cli task does not work because of the extension
# RUN curl -sSfL https://apt.octopus.com/public.key | apt-key add - && \
#     sh -c "echo deb https://apt.octopus.com/ stable main > /etc/apt/sources.list.d/octopus.com.list" && \
#     apt update && apt install octopuscli=$Octo

ARG TARGETARCH=amd64
ARG AGENT_VERSION=2.185.1

WORKDIR /azp
RUN if [ "$TARGETARCH" = "amd64" ]; then \
      AZP_AGENTPACKAGE_URL=https://vstsagentpackage.azureedge.net/agent/${AGENT_VERSION}/vsts-agent-linux-x64-${AGENT_VERSION}.tar.gz; \
    else \
      AZP_AGENTPACKAGE_URL=https://vstsagentpackage.azureedge.net/agent/${AGENT_VERSION}/vsts-agent-linux-${TARGETARCH}-${AGENT_VERSION}.tar.gz; \
    fi; \
    curl -LsS "$AZP_AGENTPACKAGE_URL" | tar -xz

COPY ./start.sh .
RUN chmod +x start.sh

ENTRYPOINT [ "./start.sh" ]
