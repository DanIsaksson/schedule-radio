# Decision log (rationale + non-goals)

This file exists to make the plan runnable **without chat history**.

## A. Auth & Identity decisions
### A.1 Cookie-based Identity (not JWT)
- **Decision**: Use .NET 8 Identity API endpoints (`MapIdentityApi`) with **cookies**.
- **Why**:
  - Works very well with a React SPA when we use the Vite proxy.
  - Uses the browser’s normal cookie behavior for sessions.
  - Matches the lesson materials for this assignment.
- **Non-goals**:
  - We are not implementing JWT auth.

### A.2 Admin-only account creation (no public self-registration)
- **Decision**: Only admins can create users.
- **Implementation note**:
  - `/api/auth/register` must be blocked/disabled.
  - Admin provisions users via `/api/admin/users`.
- **Why**:
  - Reduces attack surface and matches the assignment + your requirement.

### A.3 Roles model
- **Decision**: Two roles:
  - `Admin`
  - `Contributor`
- **Rules**:
  - Admin can do everything a contributor can, plus manage users.
  - Contributor cannot access admin endpoints.

### A.4 Temporary password + forced change
- **Decision**: When admin creates a user, they get a temporary password.
- **Rule**:
  - The user must change their password on first login.
- **Implementation note**:
  - Use `ApplicationUser.MustChangePassword`.
  - Backend should enforce the rule (not only frontend).

### A.5 Seeded default admin
- **Decision**: Seed a default admin user.
- **Credentials (dev/testing)**:
  - Email: `tobias@test.se`
  - Password: `supersafe123`

## B. User data model decision
### B.1 Inheritance approach for user profile fields
- **Decision**: `ApplicationUser : IdentityUser`.
- **Why**:
  - Matches the course material "Building a Secure Contributor Management System".
  - Minimizes extra tables/joins for this assignment.
- **Non-goals**:
  - We are not implementing the "composition" pattern (separate Contributor table linked by userId) in this iteration.

## C. Event responsibility decision
### C.1 One contributor per event
- **Decision**: Each event has exactly one responsible contributor.
- **Implementation detail**:
  - Add `ResponsibleUserId` to `EventEntity`.

### C.2 Existing events backfill
- **Decision (Q1 = B)**: `ResponsibleUserId` should be **NOT NULL** long-term.
- **Rule**:
  - Existing events (created before Identity is introduced) are assigned to the **seeded admin user**.
- **Implementation note**:
  - For SQLite/migrations safety, we may introduce the column as nullable first, backfill, and then enforce NOT NULL in a follow-up migration.

## D. Payment calculation decision
### D.1 Hour rows must be merged into logical shows
- **Decision**: Events stored as hour-rows must be merged into logical "scheduled events" to count the `+300 SEK` bonus correctly.

### D.2 Merge identity fields
- **Decision (Q2 = B)**: Merge contiguous event rows only when ALL match:
  - `ResponsibleUserId`
  - `Title`
  - `EventType`
  - `HostCount`
  - `HasGuest`
- **Why**:
  - Prevents accidentally merging adjacent but different shows.

## E. Database strategy
### E.1 Single SQLite file
- **Decision**: Keep everything in `API/scheduler.db`.
- **Why**:
  - Simplifies teacher verification and local dev.

### E.2 Move from `EnsureCreated()` to migrations
- **Decision**: Use EF Core migrations (`Database.Migrate()`).
- **Why**:
  - Identity + schema evolution needs predictable migrations.
