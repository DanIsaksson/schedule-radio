recap - Understand the backend “Main Switch” (Program.cs): builder, services (DI), middleware pipeline, and endpoint “doors”.

# 02) Backend Startup and HTTP “Doors” (API/Program.cs)

## The 3 key analogies (use these while reading)

### 1) The Builder Pattern = “Ordering a Custom Computer”

- `WebApplication.CreateBuilder(args)` is your **order form / blueprint**.
- `builder.Services` is where you **add parts** (database, auth, CORS, etc.).
- `builder.Build()` is the **final assembly** (you press “Build PC”).

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `34`
- **Code**:
  - `var builder = WebApplication.CreateBuilder(args);`

### 2) The Web App = “An animal waiting for a command”

After startup, the app basically “waits”. When an HTTP request arrives, the app wakes up and runs the matching handler.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `304`
- **Code**:
  - `app.Run();`

### 3) Endpoints = “Doors”

When you see `MapGet` / `MapPost`, imagine a **door** in a building:

- If a request knocks on `/db/schedule/7days`, run this code.

Concrete reference:

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37-40`
- **Code**:
  - `group.MapGet("/7days", (SchedulerContext db) => { ... })`

## Step 1: Add CORS (“The Bouncer”)

Because your frontend runs on a different port (5174) than your backend (5219), the browser is strict.

CORS is the **bouncer** at the backend entrance:

- It checks which frontend origins are allowed.
- It must allow **credentials** (cookies), otherwise cookie login won’t work.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `45-56`
- **Code**:
  - `builder.Services.AddCors(...)`
  - `.AllowCredentials()`

Also, CORS must be *activated* in the middleware pipeline:

- **File**: `API/Program.cs`
- **Line(s)**: `173-185`
- **Code**:
  - `app.UseCors();`

## Step 2: Register the database (EF Core)

EF Core needs a `DbContext` so it knows how to talk to SQLite.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `61-64`
- **Code**:
  - `builder.Services.AddDbContext<SchedulerContext>(options => { ... })`

The SQLite connection string is stored here:

- **File**: `API/appsettings.json`
- **Line(s)**: `9-11`
- **Code**:
  - `"Scheduler": "Data Source=scheduler.db"`

## Step 3: Register Identity (login + roles)

This is how the backend learns:

- how to store users in SQLite
- how to sign users in (cookie)
- how to check roles (Admin vs Contributor)

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `74-88`
- **Code**:
  - `.AddIdentityApiEndpoints<ApplicationUser>(...)`
  - `.AddRoles<IdentityRole>()`
  - `.AddEntityFrameworkStores<SchedulerContext>()`

## Step 4: Build the app (“assemble the computer”)

- **File**: `API/Program.cs`
- **Line(s)**: `98-100`
- **Code**:
  - `var app = builder.Build();`

## Step 5: Apply migrations + seed roles/users

On startup, the backend:

- Applies EF Core migrations (creates/updates tables)
- Seeds roles: `Admin`, `Contributor`
- Seeds a dev admin user (`tobias@test.se`)

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `105-163`
- **Code**:
  - `await db.Database.MigrateAsync();`
  - `foreach (var roleName in roleNames) { ... }`
  - `await userManager.CreateAsync(adminUser, adminPassword);`

## Step 6: Configure the middleware pipeline (request “conveyor belt”)

Order matters.

Example: authentication must happen **before** authorization.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `168-203`
- **Code**:
  - `app.UseCors();`
  - `app.UseAuthentication();`
  - `app.UseAuthorization();`

### Serving profile photos via `/images/...`

This project stores profile photos as a URL path (example: `/images/Contributors/avatar.jpg`).

To make that work, the backend serves the `API/Images` folder as static files under `/images`.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `173-183`
- **Code**:
  - `app.UseStaticFiles(new StaticFileOptions { ... RequestPath = "/images" })`

### Blocking public registration

This project rule is: **Admins create accounts**, not the public.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `188-198`
- **Code**:
  - `context.Request.Path.Equals("/api/auth/register", ...)`
  - `context.Response.StatusCode = 404;`

## Step 7: Map the endpoints (“doors”)

This is the list of “doors” your backend exposes.

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `209-215`
- **Code**:
  - `app.MapScheduleDbEndpoints();`
  - `app.MapEventDbEndpoints();`
  - `app.MapAdminUserEndpoints();`
  - `app.MapAdminPaymentsEndpoints();`
  - `app.MapContributorEndpoints();`

### Identity endpoints under /api/auth

Concrete reference:

- **File**: `API/Program.cs`
- **Line(s)**: `217-220`
- **Code**:
  - `var authGroup = app.MapGroup("/api/auth");`
  - `authGroup.MapIdentityApi<ApplicationUser>();`

### Example: a custom endpoint (logout)

Identity API endpoints do not automatically add a cookie logout endpoint in this setup, so the project adds one.

- **File**: `API/Program.cs`
- **Line(s)**: `222-231`
- **Code**:
  - `authGroup.MapPost("/logout", async (...) => { ... })`

## Step 8: How parameters get into endpoints (Minimal API binding)

In Minimal APIs, the method parameters are “filled in” automatically.

Example: booking creation endpoint signature:

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `43-55`
- **Code**:
  - `group.MapPost("/post", async (DateTime date, int hour, int startMinute, ... SchedulerContext db) => { ... })`

Where do those values come from?

- `date`, `hour`, `startMinute`… come from the query string (because the frontend sends `/db/event/post?date=...&hour=...`).
- `httpContext`, `userManager`, `db` come from ASP.NET Core services (Dependency Injection).

## Next step

Continue to:

- `03-Database-EFCore-and-Identity.md`
