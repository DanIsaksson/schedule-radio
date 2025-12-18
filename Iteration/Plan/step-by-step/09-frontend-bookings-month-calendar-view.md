# Step-by-step 09 – Frontend Bookings: month calendar view

## A. Goal
Add a full month calendar view below `/#/Bookings` so you can:
- navigate between months
- see which days have bookings
- click a day to update the schedule view

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (browser)

| Item | Implemented | Verified |
| --- | --- | --- |
| Month grid calendar is visible below the schedule area | No | No |
| Days with bookings are visually marked | No | No |
| Clicking a day selects it and updates the left schedule | No | No |
| Month navigation is restricted to +/- 1 year | No | No |

## B. Files we will touch (planned)
- `frontend/src/App.jsx`
- New (recommended): `frontend/src/components/MonthCalendar.jsx`

### B.1 Related plan documents
- `../10-bookings-ux-calendar.md`
- `./08-frontend-bookings-date-picker-and-scope.md`

## C. Step-by-step

### C.1 Choose a data strategy (backend dependency)
The calendar needs to know: "which days in this month have bookings?"

Pick one approach:

Option A (recommended): add a backend range endpoint (see Step 10).

Option B (temporary): call `GET /db/event` and filter client-side.

### C.2 Render a month grid
Render a standard month view:
- 7 columns (Mon–Sun or Sun–Sat, pick one and keep consistent)
- leading/trailing empty cells for alignment

### C.3 Mark booked days
A day is "booked" if at least one event exists on that date.

UI guideline:
- use a subtle dot/badge
- keep contrast accessible

### C.4 Navigation rules
Month navigation must be restricted:
- minimum: today - 1 year
- maximum: today + 1 year

### C.5 Day selection
Clicking a day:
- sets `selectedDate`
- triggers reload of schedule for that day
- updates the right-side picker (month/year/day)

## D. Verification checklist
- You can move months within +/- 1 year
- Booked days are marked
- Clicking a day updates the schedule view
- The calendar does not allow navigation outside the allowed window
