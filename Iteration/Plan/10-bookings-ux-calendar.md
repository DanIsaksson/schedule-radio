# Bookings UX redesign + calendar navigation (guidelines)

## A. Goal
Improve the usability and correctness of the `/#/Bookings` page.

Primary outcomes:
- The page defaults to showing **today’s schedule**.
- The "Ny bokning" form **only affects the booking being created**, not the already-rendered schedule UI.
- Users can **navigate dates** (month/year/day) in a clear way.
- Users can **view** historical bookings, but **cannot create** bookings in the past.

## A.2 Non-goals
- Rewriting the schedule algorithm or the DB schema.
- Changing payment rules.

## B. UX / UI requirements (what the page should feel like)

### B.1 Layout rule (left = schedule, right = picker)
- **Left side**: The schedule for the currently selected date.
  - Shows the hour list and the booked/free state.
  - Default selected date = **today**.
- **Right side**: Date navigation + booking creation.
  - Month picker
  - Year picker (restricted window)
  - Day/date picker
  - "Ny bokning" form

### B.2 Date rules (viewing vs creating)
We should treat "viewing schedule" and "creating a booking" as two separate concerns:

- **Viewing**
  - The user may view dates within a limited window:
    - minimum: **1 year back** from today
    - maximum: **1 year forward** from today

- **Creating (Ny bokning)**
  - The user must not be able to create bookings in the past.
  - If the selected date is in the past:
    - disable the submit button
    - show a soft warning:
      - `Bokningar går inte boka bakåt i tiden.`

### B.3 Copy/text changes
- Replace `Vecka (Idag + 6 dagar)` with:
  - `Den kommande veckan...`

## C. The bug you described (likely root cause)
Symptoms:
- Entering `14` in `Timme` affects the rendered schedule list (`class="hour-list"`).

Likely cause:
- A single React state variable is currently doing double duty:
  - it both stores "draft booking time" AND drives "which hour/date the schedule UI is showing".

Guideline:
- Keep **separate state** for:
  - selected date (what schedule to display)
  - draft booking (the form inputs)
  - schedule data (API response)

## D. Calendar view (month grid) — new UI section
Add a **full calendar month view** below the page.

Purpose:
- Quickly navigate months.
- See which days already contain bookings.

Calendar behavior:
- Month navigation is restricted to:
  - 1 year back, 1 year forward
- Days with bookings should be visually marked.
- Clicking a day:
  - selects that date
  - updates the left-side schedule
  - updates the right-side picker

## E. Data / API considerations (important)
Today the UI mostly loads:
- `/db/schedule/today`
- `/db/schedule/7days`

To support month navigation, we need one of these approaches:

### E.1 Option A (recommended): add a range endpoint
- Add backend endpoints that accept a date/range, for example:
  - `GET /db/schedule/day?date=YYYY-MM-DD`
  - `GET /db/schedule/range?start=YYYY-MM-DD&days=42`

Why:
- The frontend can request exactly what it needs for a month grid.

### E.2 Option B: reuse `/db/event` (simple, but less ideal)
- Load all events and filter client-side.

Why it’s less ideal:
- It can grow large over time.
- The frontend ends up doing filtering work that the DB is better at.

## F. Definition of done (for this redesign)
- Default view shows **today**.
- Changing form inputs does **not** change the schedule view unless you select a new date.
- Month/year/date picker exists and is consistent.
- Creating bookings in the past is blocked with the Swedish warning.
- `Den kommande veckan...` text is updated.
- Calendar month view exists below and supports navigation + day selection.
