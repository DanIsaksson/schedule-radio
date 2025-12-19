# Radio Scheduler (School Assignment)

This repository is a **full-stack school assignment** for a small radio station website.

- The **public side** lets visitors see what’s playing / what is booked in the station’s studio schedule.
- The **staff side** (Admin + Contributor) lets logged-in users create bookings and view “My account” information.

## What you can do (features)

- **Public schedule (no login)**
  - Shows a 7‑day schedule.
  - **Default content rule**: if a minute is not booked, the station plays **Music** as filler.
- **Staff portal (login required)**
  - Login using **cookie auth**.
  - Create bookings with conflict prevention.
  - Forced password-change flow for admin-created accounts.
- **Admin tools (role: Admin)**
  - Create/edit/delete users (no public self-registration).
  - Admin user management page: `#/portal/admin/users` (“Hantera konton”).
  - Run the monthly payment calculation batch job.

## Tech stack

- **Backend**
  - ASP.NET Core Minimal API (.NET 9)
  - EF Core + SQLite (`API/scheduler.db`)
  - ASP.NET Core Identity (cookie authentication)
- **Frontend**
  - React 18
  - Vite dev server + dev proxy to the backend
  - Hash-based routing (`#/...`) (no react-router)

## Run locally (development)

### 1) Start the backend API

Run in the `API/` folder:

- `dotnet run`

What happens:

- The API listens on **http://localhost:5219**
  - See: `API/Properties/launchSettings.json:8`
- It uses a SQLite DB file called `scheduler.db`
  - See: `API/appsettings.json:10`
- On startup it applies migrations and seeds a dev admin user
  - See: `API/Program.cs:105-163`

You can test the backend via Swagger:

- http://localhost:5219/swagger

### 2) Start the frontend (React + Vite)

Run in the `frontend/` folder:

- `npm install`
- `npm run dev`

What happens:

- The frontend runs on **http://localhost:5174**
  - See: `frontend/vite.config.js:14`
- Vite proxies requests so the frontend can call the backend using:
  - `fetch('/db/...')`
  - `fetch('/api/...')`
  - And load profile photos using:
    - `/images/...`
  - See: `frontend/vite.config.js:20-33`

## Default dev accounts

- **Admin user (seeded on startup)**
  - Email: `tobias@test.se`
  - Password: `supersafe123`
  - See: `API/Program.cs:123-148`

## How to use the app (frontend)

### Public routes (no login)

- `#/` (Home)
- `#/schedule` (Full schedule)

### Staff routes (login)

- `#/portal` (Login)
- `#/bookings` (Booking UI)
- `#/portal/me` (My account + payment history)
- `#/portal/change-password` (Forced password-change screen)
- `#/portal/admin/users` (Admin: Hantera konton)

## Backend API “doors” overview

Schedule (public, read-only):

- `GET /db/schedule/today`
- `GET /db/schedule/7days`
- `GET /db/schedule/day?date=YYYY-MM-DD`
- `GET /db/schedule/range?start=YYYY-MM-DD&days=42`

Bookings (requires login + not blocked by MustChangePassword):

- `POST /db/event/post?...` (create)
- `POST /db/event/{eventId}/reschedule?...`
- `POST /db/event/{eventId}/delete`

Authentication (Identity cookies):

- `POST /api/auth/login?useCookies=true`
- `POST /api/auth/logout`
- `POST /api/auth/change-password`

Contributor (requires login):

- `GET /api/contributor/me`
- `GET /api/contributor/payments`

Admin (requires role Admin):

- `POST /api/admin/users`
- `GET /api/admin/users`
- `PUT /api/admin/users/{userId}`
- `DELETE /api/admin/users/{userId}`
- `POST /api/admin/payments/calculate-previous-month`

Profile photos (static files):

- `GET /images/...` (served from `API/Images/...`)

## Where to start reading the code (beginner-friendly)

- **Backend entry point**: `API/Program.cs`
- **Database context (DbContext)**: `API/Data/SchedulerContext.cs`
- **Booking rules / conflict checks**: `API/Actions/EventActionsDb.cs`
- **Schedule projection (DB → minute-grid)**: `API/Actions/ScheduleProjectionDb.cs`
- **Frontend entry**: `frontend/index.html` → `frontend/src/main.jsx`
- **Main UI**: `frontend/src/App.jsx`
- **Auth “memory” (React Context)**: `frontend/src/auth/AuthContext.jsx`

For an onboarding / learning path, start here:

- `Project-Navigation-and-Learning/00-INDEX.md`

## Notes / gotchas

- `frontend/README.md` may mention port 5173; this repo’s actual Vite dev port is **5174** (`frontend/vite.config.js:14`).
- If you are in a pure dev scenario and want a clean DB, you can delete `API/scheduler.db` and re-run the backend so migrations recreate it.
  - Be careful: that deletes all bookings and users in that DB file.
