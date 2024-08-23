#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM hub.lumigame.com/base/dotnet/core/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Tgs.GameBets.RapidTest.Worker/Tgs.GameBets.RapidTest.Worker.csproj", "Tgs.GameBets.RapidTest.Worker/"]
COPY ["Tgs.GameBets.RapidTest.Worker.Core/Tgs.GameBets.RapidTest.Worker.Core.csproj", "Tgs.GameBets.RapidTest.Worker.Core/"]
RUN dotnet restore "./Tgs.GameBets.RapidTest.Worker/./Tgs.GameBets.RapidTest.Worker.csproj"
COPY . .
WORKDIR "/src/Tgs.GameBets.RapidTest.Worker"
RUN dotnet build "./Tgs.GameBets.RapidTest.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tgs.GameBets.RapidTest.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tgs.GameBets.RapidTest.Worker.dll"]