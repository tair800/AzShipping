# Operation Service

Operations microservice for shipping/logistics operations and execution workflows.

## Projects

- `Operation.API` — Web API, Swagger, JWT auth
- `Operation.Application` — CQRS (MediatR), DTOs, validation
- `Operation.Domain` — domain model + repository contracts
- `Operation.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Identity service (JWT) if auth is enabled in your environment

### Configuration

`Operation.API` expects:

- `ConnectionStrings:DefaultConnection` (PostgreSQL)
- `JWT:SecretKey` (same signing key as Identity)

Recommended for local dev:

```bash
cd services/Operation/Operation.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=OperationDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Operation/Operation.API/Operation.API.csproj
```

Swagger: `http://localhost:5071/swagger`

## Database

- Default DB name: `OperationDb`
- On startup: ensures DB exists, runs EF migrations, seeds baseline + demo data (many operation type helpers)

