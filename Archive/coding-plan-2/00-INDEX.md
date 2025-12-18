# Contributor Management System – Plan Index

## A. Goals (what we are building)
- **A.1 Contributor portal**
  - Login required.
  - A personal "My Contributor" page that shows:
    - Address
    - Phone
    - Email
    - Payment history
    - Optional: photo + short bio
- **A.2 Admin portal**
  - Login required.
  - Admin can create/delete:
    - Contributors
    - Admins
  - Admin can access everything a Contributor can.
- **A.3 Payments (monthly)**
  - Pay is calculated from the **previous month**.
  - Flat rules:
    - 750 SEK per hour
    - +300 SEK per scheduled event
    - VAT 25% on monthly total
- **A.4 Event responsibility**
  - **One contributor per event**.
  - Contributor is responsible for adding/editing their events.

## B. Key decisions (confirmed)
- **B.1 Account creation**: Admin-only (no public self-registration)
- **B.2 Identity model**: `ApplicationUser : IdentityUser` (inheritance)
- **B.3 Roles**:
  - `Admin`
  - `Contributor`
- **B.4 Default seeded admin**:
  - Email: `tobias@test.se`
  - Password: `supersafe123`
- **B.5 Temporary passwords**:
  - Admin generates a temporary password when creating a contributor.
  - Contributor must change password on first login.

## C. Current codebase baseline (what already exists)
- **C.1 Backend (API)**
  - Minimal API + EF Core + SQLite database `API/scheduler.db`
  - Existing tables/logic:
    - `Events` table via `EventEntity`
    - Booking endpoints under `/db/event/*`
    - Schedule read endpoints under `/db/schedule/*`
- **C.2 Frontend (React + Vite)**
  - Existing schedule UI and booking UI.
  - Vite proxy already forwards `/db` to the backend.

## D. Architecture plan (high level)
- **D.1 Authentication (cookie-based Identity)**
  - Use .NET 8 Identity API endpoints (`MapIdentityApi`) under `/api/auth`.
  - We will **block/disable `/api/auth/register`** so only admins can create accounts.
  - We will add a custom `/api/auth/logout` endpoint.
- **D.2 Authorization (roles + protected doors)**
  - Think of endpoints like "doors":
    - `/api/admin/*` doors require `Admin`.
    - `/api/contributor/*` doors require login.
  - If a contributor has `MustChangePassword = true`, we restrict access until they change it.
- **D.3 Database strategy**
  - Keep a **single SQLite file**: `API/scheduler.db`
  - Add Identity tables + contributor fields + payment tables.
  - Add `ResponsibleUserId` to `EventEntity`.
- **D.4 Payments strategy (important)**
  - Events are stored per-hour (`Date`, `Hour`, `StartMinute`, `EndMinute`).
  - To correctly apply the "+300 per scheduled event" rule, we will **merge contiguous rows** into a single logical show (see `06-payment-calculation.md`).

## E. File map (this folder)
- `01-requirements.md`
- `02-auth-roles-security.md`
- `03-database-schema.md`
- `04-api-contract.md`
- `05-frontend-pages.md`
- `06-payment-calculation.md`
- `07-decision-log.md`
- `08-codebase-map-and-gotchas.md`
- `09-definition-of-done-test-checklist.md`

## F. Read order (for a fresh implementation session)
- **F.1 Start here**
  - `07-decision-log.md`
  - `08-codebase-map-and-gotchas.md`
- **F.2 Then review the concrete specs**
  - `01-requirements.md`
  - `02-auth-roles-security.md`
  - `03-database-schema.md`
  - `04-api-contract.md`
  - `06-payment-calculation.md`
- **F.3 Implement using the step-by-step guides**
  - `step-by-step/*`
- **F.4 Verify using the finish line checklist**
  - `09-definition-of-done-test-checklist.md`

## G. Step-by-step implementation map
See `step-by-step/`:
- `01-backend-identity-setup.md`
- `02-backend-admin-user-management.md`
- `03-backend-contributor-profile-me.md`
- `04-backend-event-assignment.md`
- `05-backend-payment-history.md`
- `06-frontend-auth-context-login.md`
- `07-frontend-contributor-page.md`
