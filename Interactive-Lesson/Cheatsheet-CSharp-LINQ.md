# Cheatsheet: C# + LINQ (Radio Scheduler Edition)

Use this as a quick reference while working on the scheduler. Names and examples are chosen to reduce mental mapping and tie directly to this project.

---

## DateTime

- `DateTime.Today` → today’s date at `00:00`.
- `someDateTime.Date` → strips the time-of-day.
- Use `.Date` when comparing days, so `2024-01-01 10:00` and `2024-01-01 15:00` match as the same day.

**In this project:**
- Use `.Date` consistently when checking if an event belongs to “today”.

---

## Ranges (half-open intervals)

- Use `[start, end)` → includes `start`, excludes `end`.
- Minutes: valid range is `0..60`.
  - `StartMinute` in `0..59`.
  - `EndMinute` in `1..60` with `EndMinute > StartMinute`.

**In this project:**
- A booking from `0` to `30` occupies minutes `0..29`.
- A booking ending at `60` covers the whole hour without overlapping the next hour.

---

## LINQ basics

- `.Where(predicate)` → filter a sequence.
- `.FirstOrDefault(predicate)` → first match or `null`.
- `.ToList()` → execute the query and materialize results.

**In this project:**
- Filter events for a specific date and hour using `.Where(e => e.Date == date && e.Hour == hour)`.
- Use `.FirstOrDefault` when looking up a single item that may not exist.

---

## EF Core essentials

- `DbContext` → your unit of work; represents a session with the database.
- `DbSet<T>` → represents a table of `T` entities.
- `Find(id)` → lookup by primary key.
- `SaveChanges()` → commit pending changes to the database.

**In this project:**
- `SchedulerContext` is your `DbContext`.
- `DbSet<EventEntity> Events` lets you query and modify bookings.

---

## Validation quick rules (Bookings)

- Hour: `0–23`.
- Start minute: `0–59`.
- End minute: `1–60`.
- Always ensure `EndMinute > StartMinute`.

**In this project:**
- Keep these rules in mind when writing or debugging validation logic in `EventActionsDb` and in the admin form.
