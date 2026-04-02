# Request Service

Requests microservice for sales/requests and related workflow around requests, comments, negotiations, and routing.

## Projects

- `Request.API` — Web API, Swagger, JWT auth
- `Request.Application` — CQRS (MediatR), DTOs, validation
- `Request.Domain` — domain model + repository contracts
- `Request.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Settings service: default `http://localhost:5064`
- Accounting service (some flows): default `http://localhost:5072`
- Identity service (JWT) if auth is enabled in your environment

### Configuration

`Request.API` expects:

- `ConnectionStrings:DefaultConnection` (PostgreSQL)
- `Services:Settings` (defaults to `http://localhost:5064`)
- `Services:Accounting` (defaults to `http://localhost:5072`)
- `JWT:SecretKey` (same signing key as Identity)

Recommended for local dev:

```bash
cd services/Request/Request.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=RequestDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Request/Request.API/Request.API.csproj
```

Swagger: `http://localhost:5069/swagger`

## Database

- Default DB name: `RequestDb`
- On startup: ensures DB exists, runs EF migrations, runs a few schema safety checks, then seeds baseline data

