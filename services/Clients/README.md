# Clients Service

Clients microservice: clients, parties, addresses, directions, documents, negotiations, and related client-domain data.

## Projects

- `Clients.API` — Web API, Swagger, JWT auth
- `Clients.Application` — CQRS (MediatR), DTOs, validation
- `Clients.Domain` — domain model + repository contracts
- `Clients.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Settings service (for some reference lookups): default `http://localhost:5064`
- Identity service (JWT) if auth is enabled in your environment

### Configuration

`Clients.API` expects:

- `ConnectionStrings:DefaultConnection` (PostgreSQL)
- `Services:Settings` (defaults to `http://localhost:5064`)
- `JWT:SecretKey` (same signing key as Identity)

Recommended for local dev:

```bash
cd services/Clients/Clients.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=ClientsDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Clients/Clients.API/Clients.API.csproj
```

Swagger: `http://localhost:5065/swagger`

## Database

- Default DB name: `ClientsDb`
- On startup: ensures DB exists, runs EF migrations, seeds baseline data

