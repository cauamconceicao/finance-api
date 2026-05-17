# ── Stage 1: build ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore only when csproj changes (layer cache)
COPY FinanceApi.csproj .
RUN dotnet restore --locked-mode

COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# ── Stage 2: runtime ─────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Render injects PORT at runtime; fall back to 8080 locally
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["sh", "-c", "exec dotnet FinanceApi.dll --urls http://+:${PORT:-8080}"]
