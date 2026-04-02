# General service — implemented vs deferred

This file tracks features discussed for the **employee detail** experience and related UI that are **not** fully implemented yet, or are intentionally stubbed.

## Implemented in General.API

- **`Employees` table** with **`UserId`** as **`bigint`**, matching Identity **`User.Id`** (`long` in .NET; JSON user `id` like `1`). JWT **`uid`** is that same id as a string. **`DepartmentId`** / **`WorkerPostId`** reference Settings. Tasks use **`ResponsibleUserId`** (`bigint?`) for the same Identity user id.
- **CRUD** under `/api/employees`.
- **`GET /api/employees/summaries`** — minimal list for a **responsible person** picker; use each row’s `userId` as `responsibleUserId` when creating/updating tasks.
- **`GET /api/employees/{id}/task-statistics`** — numeric task stats for a calendar week (Mon–Sun UTC), including per-day totals and “completed” counts when `completedStatusId` query params are supplied (task status GUIDs from Settings).
- **Employee notes** — **`EmployeeNotes`** table; **`GET /api/employees/{id}/notes`** (404 if employee missing); **`POST /api/employees/{id}/notes`** with `{ "content": "..." }` — **`noteDate`** and **`createdAtUtc`** are set automatically (UTC). Test UI: **`frontend/employee-detail.html`** → **Notes** tab.

## Deferred (you asked to remember / build later)

1. **Identity / user administration — `IsEmployee` checkbox**  
   When creating a user, toggling “is employee” should create or link an **Employee** row (same `UserId` as Identity), with **`departmentId`** and **`workerPostId`** chosen from the same Settings catalogs the UI uses today. **Not implemented** in Identity; employees are created via `POST /api/employees` until then.

2. **`ResponsiblePersonName` on tasks**  
   `TaskDto.ResponsiblePersonName` is still not resolved from Identity or Employee in `TaskMapper` (still `null`). Future: resolve display name from Employee or User API.

3. **Employee detail — projects tab / productivity project chart**  
   No employee–project aggregates yet. **`ProjectId` on tasks** may remain null per your note; dedicated **projects** features for the employee overview are **not** implemented.

4. **Profit table (operation freight / expenses / profit)**  
   **Not implemented** — no API or tables; UI structure only for later.

5. **Charts**  
   Backend returns **numbers only** for task statistics; no chart payloads.

## Task statistics API — usage notes

- **Week**: Defaults to the current week; optional `weekStartUtc` (date at UTC midnight). The service normalizes to the **Monday** of that week.
- **Scope**: Only tasks where **`ResponsibleUserId` equals the employee’s `UserId`** and **`DateOfCreation`** falls in that week.
- **Completed**: Pass one or more **`completedStatusId`** query values (GUIDs from Settings task statuses). If omitted, **`completedTasks` and per-day `completed` are 0** (status semantics are app-defined).

Example:

`GET /api/employees/{employeeId}/task-statistics?completedStatusId={doneStatusGuid}&completedStatusId={closedStatusGuid}`
