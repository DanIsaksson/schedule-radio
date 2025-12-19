recap - Student-friendly definitions of key terms used in this repo, with direct file:line:code examples.

# 08) Glossary (Student-Friendly)

## Entity

**Meaning (simple):**

An **Entity** is a C# class that EF Core stores as rows in a database table.

**Example:**

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `9-43`
- **Code**:
  - `public class EventEntity { ... }`

**Why it matters in this project:**

- Each `EventEntity` is one booking segment.

## Entity relationships

**Meaning (simple):**

An **entity relationship** is how one table “points to” another.

**Example (booking owner):**

- Each booking has an owner (`ResponsibleUserId`) which points to a user (`AspNetUsers.Id`).

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `37-43`
- **Code**:
  - `public string? ResponsibleUserId { get; set; }`

**Important note:**

- This project does not configure a strict foreign key constraint yet; it enforces ownership via code checks and startup backfill.

## Entity mapping

**Meaning (simple):**

**Entity mapping** is the process of translating:

- a C# class (`EventEntity`)

into:

- a database table (`Events`).

**Where mapping happens in this repo:**

- Through EF Core conventions + migrations.

Example migration that creates the Events table:

- **File**: `API/Migrations/20251217001448_InitialIdentityAndEvents.cs`
- **Line(s)**: `61-79`
- **Code**:
  - `migrationBuilder.CreateTable(name: "Events", columns: table => ... )`

## DbContext model

**Meaning (simple):**

A **DbContext** is the main class EF Core uses to talk to the database.

Think of it like:

- “The remote control for your database”.

**Example:**

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `22-40`
- **Code**:
  - `public class SchedulerContext : IdentityDbContext<ApplicationUser>`
  - `public DbSet<EventEntity> Events { get; set; }`

## DbSet

**Meaning (simple):**

A `DbSet<T>` means:

- “There is a table for objects of type `T`.”

**Example:**

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `35`
- **Code**:
  - `public DbSet<EventEntity> Events { get; set; } = default!;`

## Constraints

**Meaning (simple):**

A **constraint** is a rule enforced by the database.

Examples:

- “This value must be unique.”
- “This value cannot be null.”

## Schema-level settings

**Meaning (simple):**

**Schema-level settings** are database rules/configuration applied to the structure of tables.

In EF Core you often configure these in `OnModelCreating`.

**Example:**

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `41-48`
- **Code**:
  - `modelBuilder.Entity<ContributorPaymentEntity>().HasIndex(...).IsUnique();`

## Composite index

**Meaning (simple):**

A **composite index** is an index built from multiple columns.

**Why we use it:**

- To guarantee one payment row per contributor per month.

**Example:**

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `45-48`
- **Code**:
  - `.HasIndex(p => new { p.PeriodYear, p.PeriodMonth, p.UserId }).IsUnique();`

## Domain entity configuration

**Meaning (simple):**

“Domain entity configuration” means:

- rules and settings we apply specifically to our project’s domain objects (Bookings, Payments, Users).

**Example:**

- Making payment rows unique per month per user (see composite index above).

## Migration

**Meaning (simple):**

A migration is a versioned set of schema changes.

Think:

- “Git commits, but for database table structure.”

**Example (add a column):**

- **File**: `API/Migrations/20251217150756_AddEventResponsibleUserId.cs`
- **Line(s)**: `17-22`
- **Code**:
  - `migrationBuilder.AddColumn<string>(name: "ResponsibleUserId", table: "Events", ...)`

## Middleware

**Meaning (simple):**

Middleware is code that runs for every request in a pipeline.

**Example (blocking public registration):**

- **File**: `API/Program.cs`
- **Line(s)**: `188-198`
- **Code**:
  - `app.Use(async (context, next) => { ... })`

## Endpoint (“door”)

**Meaning (simple):**

An endpoint is an HTTP “door” the backend exposes.

**Example:**

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37-40`
- **Code**:
  - `group.MapGet("/7days", (SchedulerContext db) => { ... })`

## Dependency Injection (DI)

**Meaning (simple):**

DI is how ASP.NET automatically gives you objects like `SchedulerContext` or `UserManager`.

**Example:**

- The endpoint function asks for `SchedulerContext db`, and ASP.NET supplies it.

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37`
- **Code**:
  - `(SchedulerContext db) => ...`

## Authentication cookie

**Meaning (simple):**

A cookie is a small piece of data stored by the browser.

In this project, the cookie proves you are logged in.

**Example (frontend sends cookies):**

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `57-59`
- **Code**:
  - `credentials: 'include'`

## Role

**Meaning (simple):**

A role is a named permission group.

In this project:

- Admin
- Contributor

**Example (AdminOnly policy):**

- **File**: `API/Program.cs`
- **Line(s)**: `70-73`

## Authorization policy

**Meaning (simple):**

A policy is a named rule used to protect endpoints.

**Example:**

- **File**: `API/Program.cs`
- **Line(s)**: `70-73`
- **Code**:
  - `options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));`

## Read model

**Meaning (simple):**

A read model is a data shape designed for reading/visualization.

In this project:

- The schedule read model is `ScheduleData` (minute grid).

**Example:**

- **File**: `API/Models/ScheduleModels.cs`
- **Line(s)**: `18-35`

## Projection

**Meaning (simple):**

Projection means transforming data from one shape to another.

In this project:

- DB rows (EventEntity) → minute grid (ScheduleData)

**Example:**

- **File**: `API/Actions/ScheduleProjectionDb.cs`
- **Line(s)**: `56-63`
- **Code**:
  - `hour.Minutes[m] = true;`

## State (React useState)

**Meaning (simple):**

React state is the component’s **Memory**.

**Example:**

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `25`
- **Code**:
  - `const [route, setRoute] = React.useState(...)`

## Effect (React useEffect)

**Meaning (simple):**

An effect is a **Side Effect** — talking to the outside world (network, DOM events).

**Example:**

- **File**: `frontend/src/main.jsx`
- **Line(s)**: `30-34`
- **Code**:
  - `window.addEventListener('hashchange', onHashChange)`

## CORS

**Meaning (simple):**

CORS is the browser security system that blocks one site from calling another, unless allowed.

Analogy:

- CORS is the **bouncer**.

**Example:**

- **File**: `API/Program.cs`
- **Line(s)**: `45-56`
- **Code**:
  - `.AllowCredentials()`

## Next step

If you want deeper syntax mentoring, continue to:

- `09-CSharp-Patterns-Used-In-This-Repo.md`
- `10-JavaScript-and-React-Patterns-Used-In-This-Repo.md`
