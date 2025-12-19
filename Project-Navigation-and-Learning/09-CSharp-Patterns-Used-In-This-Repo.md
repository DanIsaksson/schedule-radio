recap - A beginner-friendly guide to the C# patterns used in this repo (Minimal APIs, DI, LINQ, async/await, records, nullability).

# 09) C# Patterns Used in This Repo (Beginner Mentoring)

## A) “Where is Main()?” (Top-level statements)

In older C# projects, you might see:

- `static void Main(string[] args)`

In this project, `Program.cs` uses **top-level statements** (no explicit Main method).

- **File**: `API/Program.cs`
- **Line(s)**: `35`
- **Code**:
  - `var builder = WebApplication.CreateBuilder(args);`

This file *is* the entry point.

## B) Builder pattern (Ordering a Custom Computer)

- **File**: `API/Program.cs`
- **Line(s)**: `35-100`

Key steps:

- Create builder
- Add services
- Build app

## C) Dependency Injection (DI) in Minimal APIs

### C1) DI registration (services)

- **File**: `API/Program.cs`
- **Line(s)**: `61-64`
- **Code**:
  - `builder.Services.AddDbContext<SchedulerContext>(...)`

### C2) DI consumption (endpoint parameters)

Minimal API endpoints can “ask for” services as parameters.

Example:

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37`
- **Code**:
  - `(SchedulerContext db) => { ... }`

The runtime creates `db` and injects it.

## D) Extension methods: “MapXEndpoints(this WebApplication app)”

Instead of putting all endpoints in Program.cs, this repo uses extension methods.

Example:

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `21-23`
- **Code**:
  - `public static void MapScheduleDbEndpoints(this WebApplication app)`

Beginner explanation:

- The `this WebApplication app` parameter makes it callable like:
  - `app.MapScheduleDbEndpoints();`

That call happens here:

- **File**: `API/Program.cs`
- **Line(s)**: `209-212`

## E) Async/await basics (C#)

When you see `async` and `await`, think:

- “This operation might take time (DB, network). Don’t freeze the server thread.”

Example: Applying migrations at startup:

- **File**: `API/Program.cs`
- **Line(s)**: `106-110`
- **Code**:
  - `await db.Database.MigrateAsync();`

Example: querying EF Core asynchronously:

- **File**: `API/Program.cs`
- **Line(s)**: `151-153`
- **Code**:
  - `await db.Events.Where(...).ToListAsync();`

## F) LINQ (very important)

LINQ is like a “query language” for collections.

### F1) Where + ToList

- **File**: `API/Actions/ScheduleProjectionDb.cs`
- **Line(s)**: `42-45`
- **Code**:
  - `db.Events.Where(...).ToList();`

Beginner translation:

- “Give me all Events rows that match this filter.”

### F2) FirstOrDefault

- **File**: `API/Actions/ScheduleProjectionDb.cs`
- **Line(s)**: `50-54`
- **Code**:
  - `schedule.Days.FirstOrDefault(d => d.Date == row.Date)`

Beginner translation:

- “Find the first matching item, or return null.”

## G) Nullability: `string?` and `default!`

### G1) `string?` means “this can be null”

Example:

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `31-35`
- **Code**:
  - `public string? Title { get; set; }`

### G2) `default!` is a “promise” to the compiler

You’ll see this in DbSet properties:

- **File**: `API/Data/SchedulerContext.cs`
- **Line(s)**: `35-39`
- **Code**:
  - `= default!;`

Beginner explanation:

- EF Core will initialize these when the DbContext is created.
- `default!` tells the compiler: “I know what I’m doing; don’t warn me.”

## H) Records (simple DTO types)

A `record` is often used for simple “data-only” shapes.

Example: Change-password request:

- **File**: `API/Program.cs`
- **Line(s)**: `306-308`
- **Code**:
  - `public sealed record ChangePasswordRequest(string? OldPassword, string? NewPassword);`

Example: Admin user requests/responses:

- **File**: `API/Endpoints/AdminUserEndpoints.cs`
- **Line(s)**: `239-251`

## I) Minimal API parameter binding

Parameters can come from:

- Query string (`?date=...&hour=...`)
- Request body (JSON)
- Services (DI)

Example: booking endpoint parameters:

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `43-55`

Example: logout endpoint uses `[FromBody] object empty`:

- **File**: `API/Program.cs`
- **Line(s)**: `222-227`

Beginner hint:

- If the backend expects a body, the frontend must send a body (even `{}`).

## J) Enums? (Not used yet)

This project currently stores things like `EventType` as strings.

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `33`
- **Code**:
  - `public string? EventType { get; set; }`

In a later iteration, you could replace this with an enum.

## Next step

Continue to:

- `10-JavaScript-and-React-Patterns-Used-In-This-Repo.md`
