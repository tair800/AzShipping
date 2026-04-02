# General service

Tasks, meetings, reference data (VAS, vessels, incoterms), currencies, **employees**, and related APIs.

## Documentation

- **[DEFERRED-AND-ROADMAP.md](./DEFERRED-AND-ROADMAP.md)** — Employee detail / Identity `IsEmployee` / projects / profit / task stats notes and what is intentionally not built yet.

## Employees (overview)

| Method | Route | Purpose |
|--------|--------|---------|
| GET | `/api/employees` | Full employee list (detail DTOs) |
| GET | `/api/employees/summaries` | Picker for **responsible person** — includes `departmentName` / `workerPostName` when Settings is reachable |
| GET | `/api/employees/{id}` | Employee by id |
| GET | `/api/employees/by-user/{userId}` | Employee by Identity user id |
| GET | `/api/employees/{id}/task-statistics` | Weekly task counts (see roadmap for query params) |
| GET | `/api/employees/{id}/notes` | Notes for that employee (404 if employee missing) |
| POST | `/api/employees/{id}/notes` | Create note — body `{ "content": "…" }`; **`noteDate`** is set server-side (UTC calendar date) with **`createdAtUtc`** |
| POST/PUT/DELETE | `/api/employees` … | CRUD |

Employee **`userId`** is the Identity user’s numeric **`id`** (`long` / JSON number). Task **`responsibleUserId`** uses the same value. Department / position use **`departmentId`** and **`workerPostId`** from Settings.

Run: `dotnet run --project General.API/General.API.csproj` (from this folder).

## Frontend (test UI)

With **Frontend.Web** running (`dotnet run --project frontend/Web/Frontend.Web.csproj`), open:

- [http://localhost:5080/employees-index.html](http://localhost:5080/employees-index.html) — list & add employee  
- [http://localhost:5080/employee-detail.html?id={employeeGuid}](http://localhost:5080/employee-detail.html) — detail mockup

## Notes

- Default URL: `http://localhost:5068`
- Depends on Settings service for department/worker post labels: `Services:Settings` (default `http://localhost:5064`)
