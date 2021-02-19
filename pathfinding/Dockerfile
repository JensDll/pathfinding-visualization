FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Pathfinding/*.csproj ./Pathfinding/
COPY Pathfinding.Algorithm/*csproj ./Pathfinding.Algorithm/
COPY Pathfinding.Shared/*csproj ./Pathfinding.Shared/
RUN dotnet restore Pathfinding/Pathfinding.csproj

# copy everything else and build
COPY Pathfinding ./Pathfinding/
COPY Pathfinding.Algorithm ./Pathfinding.Algorithm/
COPY Pathfinding.Shared ./Pathfinding.Shared/

WORKDIR /source/Pathfinding
RUN dotnet build -c release --no-restore

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

# final image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=publish /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Pathfinding.dll"]