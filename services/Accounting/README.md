# Accounting Service

Accounting microservice for invoices, acts, payments and related lookup tables.

## Projects

- `Accounting.API` — Web API, Swagger, JWT auth
- `Accounting.Application` — CQRS (MediatR), DTOs, validation
- `Accounting.Domain` — domain model + repository contracts
- `Accounting.Infrastructure` — EF Core (PostgreSQL), migrations, repository implementations, seed

## Run (development)

### Prerequisites

- .NET SDK
- PostgreSQL
- Identity service (JWT) if auth is enabled in your environment

### Configuration

The API reads `ConnectionStrings:DefaultConnection` and `JWT:SecretKey`.

Recommended for local dev:

```bash
cd services/Accounting/Accounting.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=AccountingDb;Username=postgres;Password=YOUR_PASSWORD"
dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
```

### Start

From repo root:

```bash
dotnet run --project services/Accounting/Accounting.API/Accounting.API.csproj
```

Swagger: `http://localhost:5072/swagger`

## Database

- Default DB name: `AccountingDb`
- On startup: ensures DB exists, runs EF migrations, seeds sample data

