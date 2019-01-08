FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Docker.Benchmarking.Orchestrator.Web/Docker.Benchmarking.Orchestrator.Web.csproj src/Docker.Benchmarking.Orchestrator.Web/
COPY src/Docker.Benchmarking.Orchestrator.Infrastructure/Docker.Benchmarking.Orchestrator.Infrastructure.csproj src/Docker.Benchmarking.Orchestrator.Infrastructure/
COPY src/Docker.Benchmarking.Orchestrator.Core/Docker.Benchmarking.Orchestrator.Core.csproj src/Docker.Benchmarking.Orchestrator.Core/
RUN dotnet restore src/Docker.Benchmarking.Orchestrator.Web/Docker.Benchmarking.Orchestrator.Web.csproj
COPY . .
WORKDIR /src
RUN dotnet build src/Docker.Benchmarking.Orchestrator.Web/Docker.Benchmarking.Orchestrator.Web.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish src/Docker.Benchmarking.Orchestrator.Web/Docker.Benchmarking.Orchestrator.Web.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Docker.Benchmarking.Orchestrator.Web.dll"]
