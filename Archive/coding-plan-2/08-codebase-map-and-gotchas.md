# Codebase map & gotchas (for a fresh implementation session)

This file is a practical guide for "where to implement things" and what pitfalls to avoid.

## A. Where things live (authoritative files)
### A.1 Backend (API)
- **Entry point / configuration**
  - `API/Program.cs`
- **Database context**
  - `API/Data/SchedulerContext.cs`
- **Current domain model**
  - `API/Models/EventEntity.cs`
- **Current event logic**
  - `API/Actions/EventActionsDb.cs`
- **Current endpoints**
  - `API/Endpoints/EventDbEndpoints.cs`
  - `API/Endpoints/ScheduleDbEndpoints.cs`

### A.2 Frontend (React)
- **Main app + routing**
  - `frontend/src/App.jsx`
- **Vite proxy**
  - `frontend/vite.config.js`

## B. Existing behavior we must preserve
- `/db/schedule/today` and `/db/schedule/7days` should remain **public** read-only schedule endpoints.
- The schedule model assumes:
  - Day breaks strictly at `24:00`.
  - If nothing is scheduled, music is the default filler.

## C. Known gotchas / pitfalls
### C.1 “Merged teaching files” can confuse the IDE
There are "Merged-*" C# files used as teaching artifacts.
- They may duplicate types (like `ScheduleData`) and confuse the IDE.
- They are expected to be excluded from compilation (often via `#if false`).

If the IDE still shows duplicate-type errors even though `dotnet build` succeeds:
- Save all files
- Restart the C# language server
- Consider excluding merged files explicitly in `API/API.csproj` with `<Compile Remove=...>`

### C.2 Current DB bootstrapping uses `EnsureCreated()`
- Identity + schema evolution is much safer with migrations.
- Plan: replace `EnsureCreated()` with `Database.Migrate()`.

### C.3 Vite proxy must include `/api`
Right now proxy forwards `/db`.
- When we add auth endpoints under `/api/*`, also proxy `/api`.

### C.4 Cookies require credentials
Frontend requests must use cookies:
- `fetch(..., { credentials: 'include' })`

### C.5 Middleware order matters
In `Program.cs`:
- `UseRouting`
- `UseCors`
- `UseAuthentication`
- `UseAuthorization`

### C.6 Blocking `/api/auth/register`
If Identity endpoints are mapped, `/register` will exist by default.
- We must explicitly block it to enforce admin-only account creation.

## D. Implementation “map” (what to edit per step)
- **Step 01** Identity setup
  - `Program.cs`, `SchedulerContext.cs`, add `ApplicationUser`
- **Step 02** Admin provisioning
  - add `/api/admin/users` endpoints
  - block `/api/auth/register`
- **Step 03** Contributor profile
  - add `/api/contributor/me` and `/api/auth/change-password`
- **Step 04** Event responsibility
  - add `ResponsibleUserId` and ownership checks
- **Step 05** Payments
  - add payment table + calculator + admin calculate endpoint
- **Step 06-07** Frontend
  - proxy `/api`, add `AuthContext`, add `#/login`, `#/me`, `#/change-password`
