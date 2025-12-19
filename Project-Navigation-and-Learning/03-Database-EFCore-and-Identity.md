recap - Learn how EF Core maps C# classes to SQLite tables (including Identity tables) and how migrations + constraints work in this project.

# 03) Database, EF Core, and Identity (SQLite)

## A) The database file (SQLite)

This project uses **one SQLite database file** for:

- **Identity data** (users, roles, logins)
- **Domain data** (radio bookings, payment history)

### Where the DB file name is configured

- **File**: `API/appsettings.json`
- **Line(s)**: `9-11`
- **Code**:
  - `"Scheduler": "Data Source=scheduler.db"`

This means EF Core will use a file called `scheduler.db`.

Important beginner detail: the path is **relative**.

- If you run the API while your working directory is `API/`, the file becomes:
  - `API/scheduler.db`

### Dev note: SQLite “database is locked”

SQLite uses a single file, so **two processes cannot safely write at the same time**.

If you get an error like:

- `SQLite Error 5: 'database is locked'`

It usually means:

- Another `dotnet run` is still running (or crashed but still holds the file lock).
- Or a migration was interrupted.

Beginner-friendly reset (dev only):

- Stop the backend.
- Delete `API/scheduler.db`.
- Start the backend again so migrations recreate the schema + seed the admin user.

Important:

- Deleting `scheduler.db` deletes all users + bookings stored in that file.

### Dev note: backup DB files like `scheduler.pre-identity.*.db`

Sometimes you may see backup copies with names like:

- `scheduler.pre-identity.20251217-011330.db`

These are just **snapshots** from before a migration/refactor.

- You normally should **not** commit these to GitHub.
- They are usually covered by `.gitignore` rules for `*.db`.

## B) What EF Core is doing (big picture)

EF Core is like a “translator” between:

- **C# objects** (classes like `EventEntity`)
- **Database rows** (rows in a table called `Events`)

You typically do these actions:

- **Read**: `db.Events.Where(...).ToList()`
- **Write**: `db.Events.Add(entity); db.SaveChanges();`

Concrete example of reading:

- **File**: `API/Actions/ScheduleProjectionDb.cs`
- **Line(s)**: `42-45`
- **Code**:
  - `var rows = db.Events.Where(...).ToList();`

Concrete example of writing:

- **File**: `API/Actions/EventActionsDb.cs`
- **Line(s)**: `72-73`
- **Code**:
  - `db.Events.Add(entity);`
  - `db.SaveChanges();`

## C) DbContext: “The database remote control”

In EF Core, the **DbContext** is the main object you use to talk to the database.

In this project it is:

- `SchedulerContext`

### Where the DbContext is registered (Program.cs)

- **File**: `API/Program.cs`
- **Line(s)**: `61-64`
- **Code**:
  - `builder.Services.AddDbContext<SchedulerContext>(options => { ... })`

This registration is important because later, endpoints can ask for it as a parameter:

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37-40`
- **Code**:
  - `group.MapGet("/7days", (SchedulerContext db) => { ... })`

Notice the parameter `SchedulerContext db` — ASP.NET creates it and injects it for you.

## D) Identity + Domain in ONE DbContext

This project uses ASP.NET Identity (login system). The key design choice is:

- Our `SchedulerContext` **inherits** from `IdentityDbContext<ApplicationUser>`.

That means:

- Identity tables (AspNetUsers, AspNetRoles, …) are automatically included.
- Our own tables (Events, ContributorPayments) are also included.

Concrete reference:

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `22`
- **Code**:
  - `public class SchedulerContext : IdentityDbContext<ApplicationUser>`

### Why inheritance matters (beginner explanation)

- `IdentityDbContext<TUser>` already knows how to create and manage Identity tables.
- By inheriting from it, we avoid having to “manually” recreate that logic.

## E) DbSet: “This is a table”

A `DbSet<T>` is EF Core’s way of saying:

- “There is a table that stores rows of type `T`.”

### Events table

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `35`
- **Code**:
  - `public DbSet<EventEntity> Events { get; set; } = default!;`

### ContributorPayments table

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `39`
- **Code**:
  - `public DbSet<ContributorPaymentEntity> ContributorPayments { get; set; } = default!;`

## F) Entity: “A class that becomes a table row”

An **Entity** is a C# class that EF Core stores in the database.

### Example 1: EventEntity (one booking row)

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `9-43`

Key points:

- **Primary key**: EF treats `Id` as the row’s primary key
  - **Line(s)**: `11-13`
  - **Code**: `public int Id { get; set; }`
- **Booking time**: Date + Hour + StartMinute/EndMinute
  - **Line(s)**: `18-29`
- **Ownership**: `ResponsibleUserId`
  - **Line(s)**: `37-43`

### Example 2: ContributorPaymentEntity (one monthly payment row)

- **File**: `API/Models/ContributorPaymentEntity.cs`
- **Line(s)**: `10-44`

Key points:

- **The “period key”** is (PeriodYear, PeriodMonth)
  - **Line(s)**: `17-21`
- **Money totals are decimals** (SEK)
  - **Line(s)**: `26-41`

### Example 3: ApplicationUser (one user row in AspNetUsers)

- **File**: `API/Models/ApplicationUser.cs`
- **Line(s)**: `18-31`

Key points:

- Inherits from `IdentityUser`
  - **Line(s)**: `18`
  - **Code**: `public class ApplicationUser : IdentityUser`
- Adds project-specific profile fields:
  - `Address`, `Bio`, `PhotoUrl`, `MustChangePassword`
  - **Line(s)**: `20-31`

## G) Entity relationships (how tables connect)

A “relationship” means one row **refers to** another row.

In this project we use a simple pattern:

- Store the other table’s `Id` as a string field.

### Booking → User relationship (ownership)

- `EventEntity.ResponsibleUserId` stores `AspNetUsers.Id`

Concrete reference:

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `37-43`
- **Code**:
  - `public string? ResponsibleUserId { get; set; }`

Important beginner note:

- Right now this is a **soft relationship** (no explicit FK constraint is configured).
- The project enforces it via code rules (endpoint checks + backfill).

Example: startup backfills missing owners:

- **File**: `API/Program.cs`
- **Line(s)**: `151-163`
- **Code**:
  - `eventsMissingOwner = await db.Events.Where(...).ToListAsync();`
  - `e.ResponsibleUserId = adminUser.Id;`

## H) Constraints and schema-level settings

A **constraint** is a database rule like:

- “This column cannot be null.”
- “This combination of columns must be unique.”

### Composite index (unique)

“Composite” means: the index is built from **multiple columns**.

This project enforces:

- One payment row per (Year, Month, User)

Concrete reference:

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `45-48`
- **Code**:
  - `.HasIndex(p => new { p.PeriodYear, p.PeriodMonth, p.UserId }).IsUnique();`

What this prevents:

- Two rows for the same contributor in the same month.

## I) Migrations: “Version history for the database schema”

A migration is a C# file that says:

- “To upgrade the database, do these schema changes.”

### How migrations are applied

On backend startup:

- **File**: `API/Program.cs`
- **Line(s)**: `106-110`
- **Code**:
  - `await db.Database.MigrateAsync();`

That line is what makes a new clone of the repo “just work” (tables get created automatically).

### Important migrations in this project

- **Initial Identity + Events**
  - **File**: `API/Migrations/20251217001448_InitialIdentityAndEvents.cs`
  - **Line(s)**: `18-80`
  - Creates `AspNetUsers`, `AspNetRoles`, and `Events`.

- **Add ResponsibleUserId**
  - **File**: `API/Migrations/20251217150756_AddEventResponsibleUserId.cs`
  - **Line(s)**: `17-22`
  - Adds the `ResponsibleUserId` column to `Events`.

- **Add ContributorPayments table**
  - **File**: `API/Migrations/20251217182345_AddContributorPayments.cs`
  - **Line(s)**: `18-48`
  - Adds the `ContributorPayments` table + a unique composite index.

### “Up” and “Down” (beginner view)

Each migration has:

- **Up**: how to apply the change
- **Down**: how to undo the change

Example:

- **File**: `API/Migrations/20251217150756_AddEventResponsibleUserId.cs`
- **Line(s)**: `15-30`

## J) Next step

Continue to:

- `04-Schedule-Read-Model-and-Projection.md`
