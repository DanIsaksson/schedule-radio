# Database schema plan (SQLite: `API/scheduler.db`)

## A. Strategy
- **A.1 One database file**
  - Keep using `API/scheduler.db`.
  - Add Identity tables + contributor fields + payments.
- **A.2 Migration strategy**
  - Current code uses `EnsureCreated()`.
  - Plan is to move to EF Core **migrations** (`Database.Migrate()`) so schema changes are reliable.

## B. Identity tables
- ASP.NET Core Identity will create tables like:
  - `AspNetUsers`
  - `AspNetRoles`
  - `AspNetUserRoles`
  - etc.

## C. Application user (inheritance approach)
We will create:
- `ApplicationUser : IdentityUser`

### C.1 Contributor fields stored on `AspNetUsers`
- **Personal data**
  - Address (string)
  - Email (already on `IdentityUser`)
  - Phone (already on `IdentityUser.PhoneNumber`)
- **Optional promotional material**
  - `Bio` (string, nullable)
  - `PhotoUrl` (string, nullable)
- **Account rules**
  - `MustChangePassword` (bool)

Note: the assignment says hourly cost is 750 SEK/hour (flat). We can still store `HourlyRate` (decimal) as a future-proof field, but calculations will use the flat rate for now.

## D. Events table update
Existing:
- `Events` via `EventEntity`

Add:
- `ResponsibleUserId` (string)
  - Foreign key to `AspNetUsers.Id`
  - Enforces "one contributor per event"

Decision note:
- `ResponsibleUserId` is intended to be **NOT NULL** long-term.
- Existing pre-Identity events must be **backfilled** to the seeded admin user (`tobias@test.se`).
- If SQLite migration constraints make this awkward in one step, we can:
  - add the column as nullable
  - backfill
  - then enforce NOT NULL in a follow-up migration

## E. Payment history tables
Add a new table:
- `ContributorPayments`

Suggested columns:
- `Id` (int)
- `UserId` (string)
- `PeriodYear` (int)
- `PeriodMonth` (int)
- `TotalMinutes` (int)
- `HourlyRate` (decimal)
- `BaseAmount` (decimal)
- `EventCount` (int)
- `EventBonusAmount` (decimal)
- `Subtotal` (decimal)
- `VatRate` (decimal)
- `VatAmount` (decimal)
- `TotalIncludingVat` (decimal)
- `CalculatedAt` (DateTime)

## F. Indexes and constraints (recommended)
- Unique index on (`PeriodYear`, `PeriodMonth`, `UserId`) in `ContributorPayments`
- Index on `Events.Date`
- Index on `Events.ResponsibleUserId`

## G. Data flow summary
- Login/roles stored in Identity tables.
- Contributor page pulls:
  - user profile (`AspNetUsers`)
  - payment history (`ContributorPayments`)
- Schedule pages continue to read from schedule projection endpoints.
