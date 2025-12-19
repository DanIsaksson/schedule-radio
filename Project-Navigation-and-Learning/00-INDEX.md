recap - This folder is a beginner-friendly “map + guided tour” of the Radio Scheduler codebase (backend + frontend). Start here.

# Project Navigation and Learning (Beginner Pack)

## What this folder is

This folder exists to help you read the codebase **as a beginner**.

The project is split into:

- **Backend (C# / ASP.NET Core)**
  - Stores bookings in SQLite.
  - Exposes HTTP “doors” (endpoints) that the frontend calls.
- **Frontend (React / Vite)**
  - Renders the UI.
  - Calls the backend using `fetch('/db/...')` and `fetch('/api/...')`.

## How to read the references in this pack

Most explanations include “3-step” references:

- **File**: the file you should open.
- **Line(s)**: the exact lines to look at.
- **Code**: the exact code we are talking about.

Example:

- **File**: `API/Program.cs`
- **Line(s)**: `34`
- **Code**:
  - `var builder = WebApplication.CreateBuilder(args);`

## Recommended reading order (do this in order)

- **1) 01-Project-Tour.md**
  - “Where is everything?” and the full end-to-end flow.
- **2) 02-Backend-Startup-and-HTTP-Doors.md**
  - How the backend starts, how middleware works, and how endpoints (“doors”) are mapped.
- **3) 03-Database-EFCore-and-Identity.md**
  - DbContext, entities, migrations, and how Identity tables + domain tables live in the same SQLite file.
- **4) 04-Schedule-Read-Model-and-Projection.md**
  - The “minute-grid” schedule model and how DB rows are projected into it.
- **5) 05-Booking-Write-Flow-and-Conflict-Prevention.md**
  - The booking create/reschedule/delete endpoints, and how we prevent overlaps.
- **6) 06-Auth-Roles-and-Forced-Password-Change.md**
  - Cookie auth, roles (Admin vs Contributor), and the MustChangePassword rule.
- **7) 07-Frontend-Bootstrap-Routing-and-Data-Fetching.md**
  - `index.html` → `main.jsx` → Router → `App.jsx`, plus React “Memory” (`useState`) and “Side effects” (`useEffect`).
- **8) 08-Glossary.md**
  - Student-friendly definitions of key terms (Entity, DbContext, constraints, composite index, etc.) with code references.

Optional deeper dives:

- **9) 09-CSharp-Patterns-Used-In-This-Repo.md**
  - Minimal APIs, DI, LINQ, async/await, records, nullability.
- **10) 10-JavaScript-and-React-Patterns-Used-In-This-Repo.md**
  - React hooks (Memory + Side Effects), Context, fetch(), and common JS syntax used here.

## One-sentence mental model (the “story” of the app)

A booking is stored as an **Event row** in SQLite (`EventEntity`). When the UI needs a schedule, the backend **projects** those rows into a 7‑day “minute grid” (`ScheduleData`). When staff creates a booking, the backend checks for **time conflicts** and then saves a new row.

## Key entry points (open these first)

Backend:

- **Backend entry (“Main Switch”)**: `API/Program.cs` (startup + endpoints)
- **Database (DbContext)**: `API/Data/SchedulerContext.cs`
- **Booking rules**: `API/Actions/EventActionsDb.cs`
- **Schedule projection**: `API/Actions/ScheduleProjectionDb.cs`

Frontend:

- **HTML entry**: `frontend/index.html`
- **React entry**: `frontend/src/main.jsx`
- **Main UI**: `frontend/src/App.jsx`
- **Auth “memory” (React Context)**: `frontend/src/auth/AuthContext.jsx`
- **Dev proxy**: `frontend/vite.config.js`
