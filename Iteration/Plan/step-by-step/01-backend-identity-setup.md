# Step-by-step 01 – Backend Identity setup

## A. Goal
Add ASP.NET Core Identity (cookie-based) to the existing Minimal API so we can:
- authenticate contributors/admins
- protect endpoints with roles
- store users in the existing SQLite database (`API/scheduler.db`)

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (curl/Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| Identity packages installed | Yes | Yes |
| `ApplicationUser : IdentityUser` created (Address/Bio/PhotoUrl/MustChangePassword) | Yes | Yes |
| `SchedulerContext` upgraded to `IdentityDbContext<ApplicationUser>` | Yes | Yes |
| Identity + Authorization registered in `Program.cs` | Yes | Yes |
| Identity endpoints mapped under `/api/auth` | Yes | Yes |
| Logout endpoint (`POST /api/auth/logout`) added | Yes | No |
| EF Core migrations enabled + DB migrated (no `EnsureCreated()`) | Yes | Yes |
| Seed roles + default admin (`tobias@test.se`) | Yes | Yes |
| Public registration blocked (`POST /api/auth/register`) | Yes (Step 02) | No |

## B. Core decisions to apply
- `ApplicationUser : IdentityUser` (inheritance)
- Roles:
  - `Admin`
  - `Contributor`
- Seed default admin:
  - `tobias@test.se` / `supersafe123`

## C. Files we will touch (planned)
- `API/Program.cs`
- `API/Data/SchedulerContext.cs`
- New: `API/Models/ApplicationUser.cs`
- `API/appsettings.json` (connection string stays)

### C.1 Related plan documents
 - `../00-INDEX.md`
   - Summary of the overall system goals and the confirmed Identity decisions.
 - `../01-requirements.md`
   - Authentication and Identity requirements (contributors must log in, admin-only account creation).
 - `../02-auth-roles-security.md`
   - Roles, cookie auth, middleware order, seeded admin, and why `/register` must be blocked.
 - `../03-database-schema.md`
   - How Identity tables and `ApplicationUser` fields will live inside `API/scheduler.db`.
 - `../04-api-contract.md`
   - The planned `/api/auth/*` routes that the frontend will call.
 - `../07-decision-log.md`
   - Captures the "why" behind our Identity and database decisions so implementation doesn’t need chat context.
 - `../08-codebase-map-and-gotchas.md`
   - Practical notes for avoiding common repo pitfalls (proxy, migrations, merged teaching files).
 - `../09-definition-of-done-test-checklist.md`
   - The verification checklist used to confirm Identity is correctly integrated.

## D. Step-by-step
### D.1 Add Identity NuGet packages
Add the Identity packages needed for:
- EF Core stores (SQLite)
- Identity APIs

### D.2 Create `ApplicationUser`
Create a new class:
- `public class ApplicationUser : IdentityUser`

Add contributor-related fields:
- `Address`
- `Bio` (optional)
- `PhotoUrl` (optional)
- `MustChangePassword` (bool)

### D.3 Upgrade `SchedulerContext` to include Identity
Currently `SchedulerContext` is a plain `DbContext`.

Planned change:
- Inherit from `IdentityDbContext<ApplicationUser>`
- Keep existing `DbSet<EventEntity> Events`
- Add future `DbSet<ContributorPaymentEntity> ContributorPayments`

This lets Identity and our domain tables live in the same SQLite file.

### D.4 Register Identity in `Program.cs`
Planned service registrations:
- Add Identity services (cookie-based)
- Add Authorization services

### D.5 Map Identity endpoints
Map Identity endpoints under `/api/auth`:
- `POST /api/auth/login?useCookies=true`
- etc.

Note: `/api/auth/register` is **blocked in Step 02** (security hard requirement: admin-only account creation).

### D.6 Add logout endpoint
Identity API endpoints do not provide a logout endpoint for cookies, so we add:
- `POST /api/auth/logout`

### D.7 Add EF migrations and migrate DB
Switch from `EnsureCreated()` to migrations:
- Create initial migration that includes Identity tables + our existing tables
- Apply migrations to `scheduler.db`

(We will do the actual `dotnet ef` commands during implementation, not in the plan.)

### D.8 Seed roles + default admin
In `Program.cs` after app build:
- Create a scope
- Ensure roles exist (`Admin`, `Contributor`)
- Ensure default admin exists:
  - Email/username: `tobias@test.se`
  - Password: `supersafe123`
  - Role: `Admin`
  - `MustChangePassword = false`

## E. Verification checklist
- Login cookie is set after calling `/api/auth/login?useCookies=true`
- A protected endpoint returns:
  - `401` if not logged in
  - `200` if logged in
- Seeded admin exists in DB and can log in
