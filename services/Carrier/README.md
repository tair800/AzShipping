# Carrier Service

Carrier reference-data microservice (carriers, terminals, vehicles, drivers, shipping lines, airlines, etc.).

## Projects

- `Carrier.API` — Web API, Swagger, JWT auth
- `Carrier.Application` — CQRS (MediatR), DTOs, validation
- `Carrier.Domain` — domain model + repository contracts
- `Carrier.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Settings service (used for some lookups/integration): default `http://localhost:5064`
- Identity service (JWT) if auth is enabled in your environment

### Configuration

`Carrier.API` expects:

- `ConnectionStrings:DefaultConnection` (PostgreSQL)
- `Services:Settings` (defaults to `http://localhost:5064`)
- `JWT:SecretKey` (same signing key as Identity)

Recommended for local dev:

```bash
cd services/Carrier/Carrier.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=CarrierDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Carrier/Carrier.API/Carrier.API.csproj
```

Swagger: `http://localhost:5066/swagger`

## Database

- Default DB name: `CarrierDb`
- On startup: ensures DB exists, runs EF migrations, seeds baseline data

