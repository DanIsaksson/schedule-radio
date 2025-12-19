recap - Understand the “minute-grid” schedule read model and how the backend projects EventEntity rows into it.

# 04) Schedule Read Model and Projection (Minute Grid)

## A) Two models: “write model” vs “read model”

This project uses **two different shapes of data**:

- **Write model** (what we store)
  - `EventEntity` rows in the `Events` table
  - One row = one booking segment
- **Read model** (what we return to the UI)
  - `ScheduleData` → `DaySchedule` → `HourSchedule` → `Minutes[60]`

Why do this?

- The database is good at storing rows.
- The UI is good at drawing grids.
- The backend “projects” rows into a grid so the UI doesn’t have to.

## B) The schedule read model (DTO classes)

The schedule read model is defined here:

- **File**: `API/Models/ScheduleModels.cs`

### ScheduleData (many days)

- **Line(s)**: `18-35`

Key idea:

- It pre-creates a rolling window of days.

### DaySchedule (one day)

- **Line(s)**: `41-60`

Key idea:

- It pre-creates 24 `HourSchedule` objects.

### HourSchedule (one hour)

- **Line(s)**: `65-87`

Key fields:

- `Minutes[60]` = minute occupancy flags
  - **Line(s)**: `70-72`
  - `public bool[] Minutes { get; } = new bool[60];`

### BookingDetail (safe info for the visitor UI)

- **Line(s)**: `92-97`

The visitor UI uses this to show titles.

## C) The domain rule: default music

If a minute is not booked, the UI should show **Music**.

That’s why the minute-grid uses `false` as “free”.

Concrete example (frontend):

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `488-489`
- **Code**:
  - `<div className="empty-label">Musik</div>`

## D) Projection: Painting DB rows into the minute grid

The projection happens here:

- **File**: `API/Actions/ScheduleProjectionDb.cs`

### Step D1) Decide the date range: [startDate, endDate)

- **Line(s)**: `37-40`
- **Code**:
  - `var start = startDate.Date;`
  - `var end = start.AddDays(dayCount);`

Beginner note:

- This uses a **half-open date range**: include start, exclude end.
- That avoids “double counting” the boundary day.

### Step D2) Query all EventEntity rows in that date range

- **Line(s)**: `42-45`
- **Code**:
  - `var rows = db.Events.Where(e => e.Date >= start && e.Date < end).ToList();`

### Step D3) For each row, find the matching Day + Hour

- **Line(s)**: `47-54`
- **Code**:
  - `var day = schedule.Days.FirstOrDefault(d => d.Date == row.Date);`
  - `var hour = day.Hours.FirstOrDefault(h => h.Hour == row.Hour);`

### Step D4) Paint minutes (the most important loop)

- **Line(s)**: `56-63`
- **Code**:
  - `for (int m = startM; m < endM; m++) { hour.Minutes[m] = true; }`

This is another half-open rule:

- The booking covers minutes in `[StartMinute, EndMinute)`

Meaning:

- Start minute is included.
- End minute is excluded.

Example:

- StartMinute = 10
- EndMinute = 20
- Booked minutes are: 10, 11, 12, ..., 19

### Step D5) Add BookingDetail titles for UI

- **Line(s)**: `65-71`

## E) Schedule “doors” (API endpoints)

These are mapped under `/db/schedule/*`:

- **File**: `API/Endpoints/ScheduleDbEndpoints.cs`

Key endpoints:

- `GET /db/schedule/today`
  - **Line(s)**: `28-32`
- `GET /db/schedule/7days`
  - **Line(s)**: `37-41`
- `GET /db/schedule/day?date=YYYY-MM-DD`
  - **Line(s)**: `43-59`
- `GET /db/schedule/range?start=YYYY-MM-DD&days=42`
  - **Line(s)**: `61-80`

## F) How the frontend consumes schedule data

### Load a 7-day schedule

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1232-1239`
- **Code**:
  - `const res = await fetch('/db/schedule/7days')`
  - `setWeek(data)`

### Load one day (used by the booking UI)

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1171-1189`
- **Code**:
  - `fetch(
    `/db/schedule/day?date=${encodeURIComponent(dateIso)}`
    )`

### Why the frontend “normalizes” JSON casing

C# properties like `Hours` and `Minutes` might show up in JSON as `Hours` or `hours`.

So the frontend often uses:

- `day.hours ?? day.Hours`

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `518-522`
- **Code**:
  - `const hours = (day.hours ?? day.Hours ?? []).map(...)`

## G) Next step

Continue to:

- `05-Booking-Write-Flow-and-Conflict-Prevention.md`
