recap - A high-level guided tour: how frontend + backend + database work together, with step-by-step file:line:code pointers.

# 01) Project Tour (Big Picture)

## What you are building (domain story)

This project is a **Radio Station homepage + booking system**.

- **Default content rule**: If nothing is booked, the station plays **Music** by default.
- **Scheduling window rule**: The UI commonly shows a **7-day window**.
- **Studio rules (used by the staff UI)**:
  - Live + **1 host** ⇒ **Studio 1**
  - Live + **multiple hosts** ⇒ **Studio 2**
  - Live + **guest** ⇒ mention “guest transport cost” (future business rule)

You can see these rules in the staff UI logic:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `790-794`
- **Code**:
  - `const studio = isLive ? (form.hostCount === 1 ? 'Studio 1 (Billigare)' : 'Studio 2') : 'Standard'`

## Folder map (where things live)

- **`API/`**
  - ASP.NET Core Minimal API backend
  - SQLite database + EF Core + Identity
- **`frontend/`**
  - React + Vite frontend
  - Calls backend using `fetch('/db/...')` and `fetch('/api/...')`
- **`Project-Navigation-and-Learning/`**
  - This beginner-friendly documentation pack

## The “minute-grid” idea (core model)

The schedule is represented as:

- 7 days
- 24 hours per day
- 60 minutes per hour

Each minute is a boolean:

- `false` = free (default music)
- `true` = booked (a show is scheduled)

That structure lives here:

- **File**: `API/Models/ScheduleModels.cs`
- **Line(s)**: `65-83`
- **Code**:
  - `public bool[] Minutes { get; } = new bool[60];`

## End-to-end flow A: Public schedule read (Frontend → Backend → SQLite → Frontend)

### Step A1) React calls the backend

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1232-1238`
- **Code**:
  - `const res = await fetch('/db/schedule/7days')`

### Step A2) Vite proxies `/db/*` to the backend (so the browser doesn’t fight CORS)

- **File**: `frontend/vite.config.js`
- **Line(s)**: `20-32`
- **Code**:
  - `'/db': { target: 'http://localhost:5219', changeOrigin: true }`

### Step A3) Backend maps the schedule “door”

- **File**: `API/Program.cs`
- **Line(s)**: `209-212`
- **Code**:
  - `app.MapScheduleDbEndpoints();`

### Step A4) The endpoint handler runs

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`
- **Line(s)**: `37-41`
- **Code**:
  - `group.MapGet("/7days", (SchedulerContext db) => { ... })`

### Step A5) Projection: DB rows become a minute-grid

This is the “projection algorithm” (DB → minute grid):

- **File**: `API/Actions/ScheduleProjectionDb.cs`
- **Line(s)**: `42-63`
- **Code**:
  - `var rows = db.Events.Where(e => e.Date >= start && e.Date < end).ToList();`
  - `for (int m = startM; m < endM; m++) { hour.Minutes[m] = true; }`

### Step A6) React renders “Music” when there are no bookings

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `488-489`
- **Code**:
  - `return <div className="empty-label">Musik</div>`

## End-to-end flow B: Booking creation (Staff UI → Backend → Conflict check → Save)

### Step B1) Staff user uses the booking UI

- The booking form is in `AdminPanel`.

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `941-962`
- **Code**:
  - `<h2>Ny bokning</h2>`
  - `<form onSubmit={submitBooking} className="form">`

### Step B2) The form submit sends a POST to `/db/event/post`

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1454-1483`
- **Code**:
  - ``const res = await fetch(`/db/event/post?${queryString}`, { method: 'POST', credentials: 'include' })``

### Step B3) Backend “door”: POST /db/event/post

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `43-90`
- **Code**:
  - `group.MapPost("/post", async (DateTime date, int hour, ... SchedulerContext db) => { ... })`

### Step B4) Business rule: prevent overlaps (conflict prevention)

- **File**: `API/Actions/EventActionsDb.cs`
- **Line(s)**: `46-57`
- **Code**:
  - `bool overlaps = !(endMinute <= e.StartMinute || e.EndMinute <= startMinute);`

### Step B5) Save the row to SQLite

- **File**: `API/Actions/EventActionsDb.cs`
- **Line(s)**: `60-76`
- **Code**:
  - `db.Events.Add(entity);`
  - `db.SaveChanges();`

## End-to-end flow C: Login (cookie auth)

### Step C1) Frontend calls the login “door”

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `94-102`
- **Code**:
  - `fetch('/api/auth/login?useCookies=true', { method: 'POST', ..., credentials: 'include' })`

### Step C2) Backend maps Identity endpoints under `/api/auth`

- **File**: `API/Program.cs`
- **Line(s)**: `217-220`
- **Code**:
  - `var authGroup = app.MapGroup("/api/auth");`
  - `authGroup.MapIdentityApi<ApplicationUser>();`

### Step C3) Frontend fetches `/api/contributor/me` to load roles + MustChangePassword

- **File**: `frontend/src/auth/AuthContext.jsx`
- **Line(s)**: `57-59`
- **Code**:
  - `fetch('/api/contributor/me', { credentials: 'include' })`

## End-to-end flow D: Admin user management (“Hantera konton”)

This flow is only available to users with the `Admin` role.

### Step D1) The Admin sees a special menu link

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `654-666`
- **Code**:
  - `<a href="#/portal/admin/users">Hantera konton</a>`

### Step D2) The admin page calls the backend “doors” for user management

The UI uses these endpoints:

- `GET /api/admin/users` (list)
- `POST /api/admin/users` (create, returns a temporary password)
- `PUT /api/admin/users/{userId}` (edit details + role)
- `DELETE /api/admin/users/{userId}` (delete)

Backend implementation:

- **File**: `API/Endpoints/AdminUserEndpoints.cs`

### Step D3) Temporary password (Admin creates user)

When an admin creates a user, the backend returns a temporary password.
The UI shows it so the admin can give it to the new user.

- **File**: `API/Endpoints/AdminUserEndpoints.cs`
- **Line(s)**: `102-106`

### Step D4) Profile photos are served via `/images/...`

To keep the project beginner-friendly (no file uploads yet), profile photos are stored as a URL path (example:
`/images/Contributors/some-avatar.jpg`).

- Backend serves `API/Images` as `/images`.
- The Vite dev server proxies `/images` to the backend.

Concrete references:

- **File**: `API/Program.cs`
  - **Line(s)**: `173-183`
- **File**: `frontend/vite.config.js`
  - **Line(s)**: `20-36`

## End-to-end flow E: Monthly payment calculation (Admin batch job)

- **File**: `API/Endpoints/AdminPaymentsEndpoints.cs`
- **Line(s)**: `27-40`
- **Code**:
  - `group.MapPost("/payments/calculate-previous-month", async (...) => { ... })`

The “money math” is centralized here:

- **File**: `API/Services/PaymentCalculator.cs`
- **Line(s)**: `15-19`
- **Code**:
  - `public const decimal DefaultHourlyRate = 750m;`
  - `public const decimal DefaultEventBonus = 300m;`
  - `public const decimal DefaultVatRate = 0.25m;`

## Next steps

Continue to:

- `02-Backend-Startup-and-HTTP-Doors.md`
