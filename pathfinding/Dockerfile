FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app
COPY *.sln ./
COPY Pathfinding/*.csproj ./Pathfinding/
RUN dotnet restore

FROM build as dev

# https://github.com/dotnet/dotnet-docker/issues/2430#issuecomment-742048902
RUN useradd -ms /bin/bash -u 1000 development-user

# https://github.com/dotnet/dotnet-docker/issues/2396#issuecomment-728243108
RUN apt-get update \
  && apt-get install -y --no-install-recommends \
  procps \
  && rm -rf /var/lib/apt/lists/*

# This folder has a different owner because of the restore in build
RUN chown -R development-user:development-user /tmp/NuGetScratch/

EXPOSE 5000
WORKDIR /app/Pathfinding
ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:5000"]
