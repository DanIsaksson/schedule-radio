# 05. Schedule Projection (DB → 7-Day Grid)

## What you’ll learn
- Describe the data structure that represents the 7-day schedule (days, hours, minutes).
- Explain how bookings in the `Events` table are projected onto that structure.
- Understand why the code uses **half-open ranges** and an exclusive end date.

## Prerequisites
- `02. EF Core – DbContext, DbSet, and EventEntity` – you know what one booking row looks like.
- `04. Booking – Database CRUD Endpoints` – you’ve seen how bookings are validated and saved.

---

## Mental model: colouring in a blank calendar

Imagine a blank **7-day calendar** with:

- 7 days
- Each day split into hours
- Each hour split into 60 minute slots (an array of length 60)

The projection code:

1. Starts with a completely blank calendar (all minutes are `false` / empty).
2. Reads event rows from the database.
3. For each event, finds the matching day and hour.
4. Marks its occupied minutes as `true` inside the minutes array.

The UI later uses this structure to decide which cells to highlight.

---

## 🟢 Green Coder Tips: Avoiding Mental Mapping

When dealing with complex data structures like a 3D grid (Days → Hours → Minutes), it's easy to get lost in indices like `grid[i][j][k]`.

> "Choose an expressive name... and can reduce the need for other developers to mentally map the code elements to their intended meanings."
> — *Clean Code Fundamentals*

In `ScheduleProjectionDb`:
- Notice if we use named classes like `ScheduleData`, `DayData`, `HourData` instead of generic lists of lists.
- A class named `ScheduleData` is much easier to understand than `List<List<List<bool>>>`!
- This reduces the cognitive load – you don't have to remember "Level 1 is days, Level 2 is hours".

---

## Guided walkthrough

### 1. Meet `BuildSevenDaySchedule`

1. Open `API/Actions/ScheduleProjectionDb.cs`.
2. Find the method that builds the 7-day schedule (for example, `BuildSevenDaySchedule`).
3. Skim the code to identify:
   - How the empty `ScheduleData` object is created.
   - How days, hours, and minutes are represented (lists/arrays, nested objects).

> Focus on the **shape**: how many levels there are (week → day → hour → minutes[60]).

### 2. See how rows are selected

1. Still in `ScheduleProjectionDb`, find where it queries events from the database.
2. Notice the date range filter:
   - It typically uses `[startDate, endDate)` – the end date is **exclusive**.
3. Confirm for yourself which rows are included when you ask for a 7-day window starting today.

**Connect to earlier lessons:**  
This is the same half-open idea you saw with minutes and with interval validation: include the start, exclude the end.

### 3. See how minutes are filled

1. Find the loop that walks through minutes for each event.
2. Look for logic similar to:
   - `for (int m = max(0, startMinute); m < min(60, endMinute); m++)`
3. Note what happens inside the loop:
   - Which minute index is set.
   - How the code avoids going below 0 or above 59.

> The `max`/`min` (or `Math.Clamp`) pattern is there to **defend against bad data** and keep indices in range.

---

## Fun snippet – half-open date ranges in practice

The idea of using `[start, end)` for date ranges shows up in many real systems:

- **Subscriptions**: a monthly subscription might be valid from `2024-01-01` (inclusive) to `2024-02-01` (exclusive).
- **Analytics**: dashboards often say “show data from 1 Jan to 8 Jan” and internally use `[2024-01-01, 2024-01-08)`.
- **Programming APIs**: many libraries accept a start date and treat the end date as “up to but not including this day”.

The scheduler uses the same approach so that adjacent windows line up cleanly without overlapping the last day twice.

---

## Try it

1. Create a few bookings:
   - One today at a known hour and minute range.
   - One tomorrow.
2. Call `GET /db/schedule/7days`.
3. Inspect the returned JSON:
   - Find the day corresponding to today.
   - Within that day, find the matching hour.
   - Check that the minutes array has `true` values exactly where you expect.
4. Repeat for tomorrow’s booking.

Optional: also call `GET /db/schedule/today` and compare its structure to the 7-day version.

---

## Check yourself

- Why is the end date in the date filter **exclusive** instead of inclusive?
- What would go wrong if you allowed minute indices outside `0–59`?
- How does the projection logic ensure that a booking ending exactly at the top of the hour doesn’t spill into the next hour’s minutes array?

---

## 📚 External Resources

- [Structure of a Date Range](https://martinfowler.com/eaaDev/Range.html) (Martin Fowler on Ranges)
- [C# LINQ Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) (Used in projection)

---

## Where to go next

**Next lesson:** `06. Legacy In-Memory System`
You’ll compare this database-backed projection to the original in-memory implementation and see when each approach is useful.
