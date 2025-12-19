recap - Walk through booking creation/reschedule/delete: UI → endpoint binding → validation → overlap check → SQLite write.

# 05) Booking Write Flow and Conflict Prevention

## A) What a “booking” means in this project

A booking is stored as an **Event row** (`EventEntity`) with:

- Date (yyyy-mm-dd)
- Hour (0–23)
- StartMinute (0–59, inclusive)
- EndMinute (1–60, exclusive)

Concrete reference:

- **File**: `API/Models/EventEntity.cs`
- **Line(s)**: `18-29`

Important rule:

- We treat time ranges as **half-open**: `[startMinute, endMinute)`.

## B) Frontend booking UI flow

### B1) Pick a date + hour

The staff UI shows:

- A calendar grid (pick a date)
- A list of hours for that date

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `865-892` (calendar)
- **Line(s)**: `905-924` (hour list)

### B2) Pick minutes (Minute Matrix)

The minute matrix lets you click minutes 0–59.

- First click = start minute
- Second click = end minute

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1144-1162`
- **Code**:
  - `const handleMinuteClick = (hour, minute) => { ... }`

The “selection state” is:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1122-1123`
- **Code**:
  - `const [selection, setSelection] = useState(null)`

### B3) The booking form is a controlled form

Controlled means:

- The `<input>` value comes from React state.
- `onChange` updates React state.

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `970-1016` (form inputs)

## C) The “exclusive end minute” footgun (SUPER important)

The backend expects:

- `EndMinute` is **exclusive**

The UI selection feels like:

- “If I pick 10 to 19, that should book 10–19.”

To match the backend half-open rule, the frontend sends:

- `endMinute + 1`

Concrete reference:

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1471-1473`
- **Code**:
  - `endMinute: String(form.endMinute + 1),`

If you remove this +1, bookings will be “one minute shorter” than users expect.

## D) The POST request: frontend → backend

### D1) Frontend sends POST /db/event/post

- **File**: `frontend/src/App.jsx`
- **Line(s)**: `1480-1483`
- **Code**:
  - `fetch(`/db/event/post?${queryString}`, { method: 'POST', credentials: 'include' })`

Important:

- `credentials: 'include'` is required because booking endpoints are protected.

## E) Backend booking endpoints

The booking endpoints are grouped under:

- `/db/event/*`

Concrete reference:

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `23-24`
- **Code**:
  - `var group = app.MapGroup("/db/event");`

### E1) Create booking: POST /db/event/post

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `43-90`

Key sub-steps:

- Auth check:
  - **Line(s)**: `57-61`
  - `currentUserId = userManager.GetUserId(httpContext.User)`
- Ownership (ResponsibleUserId):
  - default: current user
  - admin can assign someone else
  - **Line(s)**: `65-81`
- Call business logic:
  - **Line(s)**: `83-86`
  - `EventActionsDb.CreateEvent(...)`

### E2) Reschedule booking

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `92-125`

Important permission rule:

- Admin can reschedule any event.
- Contributor can only reschedule events they own.

Concrete reference:

- **Line(s)**: `114-119`

### E3) Delete booking

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `128-157`

Same ownership rule:

- **Line(s)**: `146-151`

## F) Business logic: validation + conflict prevention

The booking rules live in:

- **File**: `API/Actions/EventActionsDb.cs`

### F1) Simple validation

- **Line(s)**: `33-38`

Examples:

- hour must be 0–23
- minutes must be 0–59
- endMinute must be > startMinute
- responsibleUserId must not be blank

### F2) Conflict check (overlap prevention)

The algorithm:

- Get all bookings for the same date and hour
- For each one, check if time ranges overlap

Concrete reference:

- **File**: `API/Actions/EventActionsDb.cs`
- **Line(s)**: `40-57`

The overlap formula:

- **Line(s)**: `55-56`
- **Code**:
  - `bool overlaps = !(endMinute <= e.StartMinute || e.EndMinute <= startMinute);`

Beginner explanation:

- Two bookings DO NOT overlap if:
  - myEnd is before or exactly at otherStart
  - OR otherEnd is before or exactly at myStart

Because ranges are half-open, these are allowed:

- Booking A: 10–20
- Booking B: 20–30

They “touch” but do not overlap.

### F3) Save to SQLite

- **Line(s)**: `60-76`

## G) MustChangePassword enforcement on booking endpoints

Bookings are blocked if the user must change password.

- **File**: `API/Endpoints/EventDbEndpoints.cs`
- **Line(s)**: `88-90` and `160-185`

## H) Next step

Continue to:

- `06-Auth-Roles-and-Forced-Password-Change.md`
