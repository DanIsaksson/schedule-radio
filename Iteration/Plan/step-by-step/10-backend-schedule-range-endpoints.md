# Step-by-step 10 – Backend: schedule range endpoints (for Bookings calendar)

## A. Goal
Add backend support so the frontend can load schedule/events for arbitrary dates (not only today + next 6 days).

This is required if we want a month calendar that can show booked days efficiently.

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (Swagger/curl)

| Item | Implemented | Verified |
| --- | --- | --- |
| Day endpoint exists: load schedule for a requested date | No | No |
| Range endpoint exists: load schedule/events for N days (calendar grid) | No | No |
| Past booking creation is rejected server-side (optional defense-in-depth) | No | No |

## B. Files we will touch (planned)
- `API/Endpoints/ScheduleDbEndpoints.cs`
- `API/Actions/ScheduleProjectionDb.cs`
- (Optional) new endpoint file: `API/Endpoints/ScheduleRangeDbEndpoints.cs`

### B.1 Related plan documents
- `../10-bookings-ux-calendar.md`
- `../04-api-contract.md`

## C. Step-by-step

### C.1 Add a day endpoint
Example:
- `GET /db/schedule/day?date=YYYY-MM-DD`

Behavior:
- validate date
- return `DaySchedule` using existing projection logic

### C.2 Add a range endpoint (calendar-friendly)
Example:
- `GET /db/schedule/range?start=YYYY-MM-DD&days=42`

Why 42:
- common month-grid size (6 weeks x 7 days)

Behavior:
- validate start date and days
- build schedule days for that range
- return a list of DaySchedules (or a ScheduleData-like container)

### C.3 (Optional) Defense-in-depth: block creating bookings in the past
Even if the UI blocks it, the backend can also reject it:
- if `date < today` return a validation error

## D. Verification checklist
- Swagger can call day endpoint for different dates
- Range endpoint returns a consistent number of days
- Frontend calendar can load booked days for a chosen month
