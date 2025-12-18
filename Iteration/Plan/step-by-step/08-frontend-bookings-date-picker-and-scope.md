# Step-by-step 08 – Frontend Bookings: date picker + correct state scope

## A. Goal
Fix the `/#/Bookings` page UX and the "Ny bokning changes the hour list" bug by:
- separating state (selected date vs booking draft)
- improving date navigation (month/year/day)
- preventing booking creation in the past

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (browser)

| Item | Implemented | Verified |
| --- | --- | --- |
| Left side shows the selected date schedule (defaults to today) | No | No |
| Right side has month/year/day navigation (+/- 1 year window) | No | No |
| "Ny bokning" state is isolated from the schedule UI state | No | No |
| Past bookings are blocked with `Bokningar går inte boka bakåt i tiden.` | No | No |
| `Vecka (Idag + 6 dagar)` copy is replaced with `Den kommande veckan...` | No | No |

## B. Files we will touch (planned)
- `frontend/src/App.jsx`
- (Optional) create components for readability:
  - `frontend/src/components/BookingsDatePicker.jsx`
  - `frontend/src/components/BookingsForm.jsx`

### B.1 Related plan documents
- `../10-bookings-ux-calendar.md`
- `../04-api-contract.md` (current schedule endpoints)
- `../08-codebase-map-and-gotchas.md`

## C. Step-by-step

### C.1 Separate the page state (the main fix)
Create explicit, separate state variables:
- `selectedDate` (the date the left schedule should show)
- `bookingDraft` (hour/start/end/title/etc)
- `scheduleForSelectedDate` (API result)
- `warningMessage` (soft UX warning)

Rule:
- Editing `bookingDraft` must never mutate `selectedDate`.

### C.2 Default to today
On first load:
- set `selectedDate = today`
- load schedule for today

### C.3 Add month/year/day picker on the right
Requirements:
- month + year dropdowns (or a calendar header)
- a day picker (date input or clickable calendar)

Constraints:
- Restrict viewing range to:
  - today - 1 year
  - today + 1 year

### C.4 Prevent booking creation in the past
If `selectedDate < today`:
- disable submit
- show warning:
  - `Bokningar går inte boka bakåt i tiden.`

Important:
- This is a UX rule. The backend may also enforce it later (defense-in-depth).

### C.5 Rename the week label
Replace:
- `Vecka (Idag + 6 dagar)`
With:
- `Den kommande veckan...`

## D. Verification checklist
- Page loads and defaults to today’s schedule
- Changing `Timme` in "Ny bokning" does NOT change the hour list selection/render
- Switching date updates the schedule (and only then)
- Past dates disable submit and show the Swedish warning
- Week label text is updated
