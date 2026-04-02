# Quotes Service

Quotes microservice for managing quotes and quote-related exports.

## Projects

- `Quotes.API` — Web API, Swagger, JWT auth
- `Quotes.Application` — CQRS (MediatR), DTOs, validation
- `Quotes.Domain` — domain model + repository contracts
- `Quotes.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Settings service: default `http://localhost:5064`
- Identity service (JWT) if auth is enabled in your environment

### Configuration

`Quotes.API` expects:

- `ConnectionStrings:DefaultConnection` (PostgreSQL)
- `Services:Settings` (defaults to `http://localhost:5064`)
- `JWT:SecretKey` (same signing key as Identity)

Recommended for local dev:

```bash
cd services/Quotes/Quotes.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=QuotesDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Quotes/Quotes.API/Quotes.API.csproj
```

Swagger: `http://localhost:5070/swagger`

## Database

- Default DB name: `QuotesDb`
- On startup: ensures DB exists, runs EF migrations, seeds baseline data
- Reseed helper: run with `--reseed` to clear quotes + seed again

