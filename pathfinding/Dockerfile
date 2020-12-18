FROM mcr.microsoft.com/dotnet/sdk:5.0

# https://github.com/dotnet/dotnet-docker/issues/2430#issuecomment-742048902
RUN useradd -ms /bin/bash -u 1000 development-user

# https://github.com/dotnet/dotnet-docker/issues/2396#issuecomment-728243108
RUN apt-get update \
  && apt-get install -y --no-install-recommends \
  procps \
  && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY . .
WORKDIR /app/Pathfinding
ENTRYPOINT dotnet watch run --urls=http://+:5000