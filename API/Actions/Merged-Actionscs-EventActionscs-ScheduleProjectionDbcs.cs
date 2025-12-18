// A.1 [Teaching.Artifact] Merged reference file (not compiled).
// What: A copy/merge of multiple source files (Actions/EventActionsDb/ScheduleProjectionDb) kept as a single reading artifact.
// Why: Helps teaching by reducing file-hopping, but would duplicate types if compiled.
// Where: Excluded from build via `#if false`; use the real files in API/Actions/* instead.
//#if false wrapper: this merged teaching file is excluded from compilation to avoid duplicate type definitions.
#if false
// --- FILE: Actions/Actions.cs ---
// A.5c Legacy in-memory booking logic (the "whiteboard") behind /schedule/* endpoints.
// - Booking.SendBooking applies changes directly to the in-memory ScheduleData minute grid (no SQLite, no EF Core).
// - EventActions keeps an in-memory dictionary of eventId -> time range so endpoints can list, delete, and reschedule.
// - This mirrors the DB-backed logic in EventActionsDb, but everything lives only in RAM and is lost when the app restarts.
//
// B.25 [Legacy booking lane] What this system really is:
// - A "sandbox" copy of the scheduler used for demos, labs, and algorithm experiments.
// - Uses the same 7-day × 24 × 60 minute grid as the DB-backed flow (Program.cs A.3, Models A.3a),
//   but updates it directly instead of persisting bookings to the Events table.
// - The real React UI and production flows use the /db/* endpoints and EventActionsDb/B.15* instead.

using System;
using System.Collections.Generic;
using System.Linq; // For .Where and .ToList
using API.Data; // SchedulerContext
using API.Models; // Use our new models instead of the old ones

namespace API.Actions;

public static class Booking
{
    /// <summary>
    /// B.25 Booking.SendBooking: core minute-level operation for the legacy in-memory lane.
    /// - Attempts to book (or free) a range of minutes within a given hour on a specific date.
    /// - Used by legacy in-memory endpoints (/schedule/book and /schedule/event/*) for demos and experiments.
    /// - Contrast: DB-backed lane uses EventActionsDb.CreateEvent + ScheduleProjectionDb to affect the grid indirectly.
    /// </summary>
    /// <param name="schedule">The in-memory seven-day rolling schedule.</param>
    /// <param name="date">Date we want to modify (only the Date part is considered).</param>
    /// <param name="hour">Hour of the day (0-23).</param>
    /// <param name="startMinute">Inclusive start minute (0-59).</param>
    /// <param name="endMinute">Exclusive end minute (1-60, must be > startMinute).</param>
    /// <param name="isBooked">true to book, false to free.</param>
    /// <returns>True if the operation succeeded; false if any validation failed or a conflict occurred.</returns>
    internal static bool SendBooking(
        ScheduleData schedule,
        DateTime date,
        int hour,
        int startMinute,
        int endMinute,
        bool isBooked)
    {
        // Basic validation: reject out-of-range hours/minutes and inverted ranges.

        if (schedule is null) return false;
        if (hour is < 0 or > 23) return false;
        if (startMinute is < 0 or >= 60) return false;
        if (endMinute <= startMinute || endMinute > 60) return false;

        // Find the matching day in the seven-day window; if the date is outside this window, return false.

        DaySchedule? day = schedule.Days.FirstOrDefault(d => d.Date.Date == date.Date);
        if (day is null) return false; // outside current window

        // Find the requested hour slot
        HourSchedule? hourSlot = day.Hours.FirstOrDefault(h => h.Hour == hour);
        if (hourSlot is null) return false;

        // Conflict detection: if we are booking, ensure all minutes in [startMinute, endMinute) are free.

        if (isBooked)
        {
            for (int m = startMinute; m < endMinute; m++)
            {
                if (hourSlot.Minutes[m]) return false; // already booked somewhere in range
            }
        }

        // Apply booking or freeing by setting minutes to true (booked) or false (free).

        for (int m = startMinute; m < endMinute; m++)
        {
            hourSlot.Minutes[m] = isBooked;
        }

        return true;
    }
}

// === Experiments: In-memory booking behaviour (legacy path) ===
// Experiment 1: Relax validation rules and observe invalid bookings.
//   Step 1: Temporarily loosen the guards in SendBooking (hour/minute checks) to allow out-of-range values.
//   Step 2: Call the legacy /schedule/event/post endpoint with invalid hours or minutes.
//   Step 3: Inspect how the in-memory schedule and any consumers render these bookings, then restore the original guards.
// Experiment 2: Allow overlapping bookings for the same hour.
//   Step 1: In SendBooking, remove or modify the loop that rejects already-booked minutes.
//   Step 2: Create two bookings that overlap in time via /schedule/event/post.
//   Step 3: Observe how hourly cells are rendered when multiple bookings share minutes, then restore the conflict rule.
// Experiment 3: Change the length of the rolling 7-day window.
//   Step 1: In ScheduleData (Models/ScheduleModels.cs), adjust the constructor to create more or fewer than 7 days.
//   Step 2: Rebuild and call /schedule/7days and the DB-backed /db/schedule/7days endpoints.
//   Step 3: Compare how consumers behave when the window size differs between in-memory and DB projections.

public record EventSummary(DateTime Date, int Hour, int StartMinute, int EndMinute);

public static class EventActions
{
    private static int _nextId = 1;
    private static readonly Dictionary<int, (DateTime Date, int Hour, int StartMinute, int EndMinute)> _events = new();

    // B.25a Create a new in-memory event (booking) for POST /schedule/event/post.
    // - Legacy counterpart of EventActionsDb.CreateEvent in the DB-backed flow [B.15b].
    public static int CreateEvent(ScheduleData schedule, DateTime date, int hour, int startMinute, int endMinute)
    {
        bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, true);
        if (!success) return -1;
        int id = _nextId++;
        _events[id] = (date, hour, startMinute, endMinute);
        return id;
    }

    // Delete an in-memory event (unbook) for POST /schedule/event/{eventId}/delete.
    // Legacy counterpart of EventActionsDb.DeleteEvent.
    public static bool DeleteEvent(ScheduleData schedule, int eventId)
    {
        if (!_events.TryGetValue(eventId, out var e)) return false;
        bool success = Booking.SendBooking(schedule, e.Date, e.Hour, e.StartMinute, e.EndMinute, false);
        if (!success) return false;
        _events.Remove(eventId);
        return true;
    }

    // Reschedule an existing in-memory event for POST /schedule/event/{eventId}/reschedule.
    // Uses Booking.SendBooking to free the old slot, then try to book the new one, with rollback on failure.
    public static bool RescheduleEvent(
        ScheduleData schedule,
        int eventId,
        DateTime newDate,
        int newHour,
        int newStartMinute,
        int newEndMinute)
    {
        if (!_events.TryGetValue(eventId, out var old)) return false;
        // Free old slot first
        Booking.SendBooking(schedule, old.Date, old.Hour, old.StartMinute, old.EndMinute, false);
        // Try booking new slot
        bool success = Booking.SendBooking(schedule, newDate, newHour, newStartMinute, newEndMinute, true);
        if (!success)
        {
            // roll back
            Booking.SendBooking(schedule, old.Date, old.Hour, old.StartMinute, old.EndMinute, true);
            return false;
        }
        _events[eventId] = (newDate, newHour, newStartMinute, newEndMinute);
        return true;
    }

    // Simple overview of all current in-memory events (used by GET /schedule/event and related endpoints).
    public static IReadOnlyDictionary<int, (DateTime Date, int Hour, int StartMinute, int EndMinute)> ListEvents()
        => _events;
}

public static class ScheduleQueries
{
    // Returns the whole 7-day in-memory schedule – convenient wrapper for legacy queries.
    public static ScheduleData GetSevenDays(ScheduleData schedule) => schedule;

    // Returns a list of booked hour slots as summaries
    public static List<EventSummary> GetBookedSummaries(ScheduleData schedule)
    {
        var list = new List<EventSummary>();
        foreach (var day in schedule.Days)
        {
            foreach (var hour in day.Hours)
            {
                // If at least one minute is booked, consolidate the whole hour
                if (hour.Minutes.Any(m => m))
                {
                    int first = Array.FindIndex(hour.Minutes, m => m);
                    int last = Array.FindLastIndex(hour.Minutes, m => m) + 1; // exclusive
                    list.Add(new EventSummary(day.Date, hour.Hour, first, last));
                }
            }
        }
        return list;
    }
}

// --- FILE: Actions/EventActionsDb.cs
// Beginner view: "guard" logic behind the /db/event doors (see Endpoints/EventDbEndpoints.cs).
// - Each public method is one booking action: create, delete, reschedule, list.
// How to read this file
// - Start from the method that matches the endpoint name (e.g. CreateEvent ↔ POST /db/event/post).
// - Inside each method, follow the numbered steps to see validation, conflict checks, and DB writes.
// - When you see LINQ like .Where(...).ToList(), read it as: "filter rows like this, then run the query now".

public static class EventActionsDb
{
    // B.15b CreateEvent: core booking logic behind POST /db/event/post [B.15a].
    // - Called from the React admin submit handler App.jsx submitBooking [B.15] via EventDbEndpoints.
    // - Returns the new Id on success; -1 if validation or overlap checks fail.
    public static int CreateEvent(SchedulerContext db, DateTime date, int hour, int startMinute, int endMinute, string? title, string? eventType, int? hostCount, bool hasGuest)
    {
        // 1) Validate input (keep it simple):
        //    - hour must be 0..23; minutes 0..59; end > start.
        if (hour < 0 || hour > 23) return -1;
        if (startMinute < 0 || startMinute > 59) return -1;
        if (endMinute <= startMinute || endMinute > 60) return -1;
        
        // 2) Query existing bookings for the same date and hour:
        var day = date.Date;
        var sameHour = db.Events
            .Where(e => e.Date == day && e.Hour == hour)
            .ToList();

        // 3) Check overlap with any existing booking in sameHour:
        foreach (var e in sameHour)
        {
            // Do these two time ranges overlap? (half-open [start, end))
            // - startMinute: my new booking start (0..59)
            // - endMinute: my new booking end   (1..60, must be > start)
            // - e.StartMinute: existing row start
            // - e.EndMinute:   existing row end
            // They overlap if NOT (myEnd <= otherStart || otherEnd <= myStart)
            bool overlaps = !(endMinute <= e.StartMinute || e.EndMinute <= startMinute);
            if (overlaps) return -1;
        }

        // 4) Create a new EventEntity (a single DB row for one booking). See Models/EventEntity.cs for fields.
        var entity = new EventEntity
        {
            Date = day,
            Hour = hour,
            StartMinute = startMinute,
            EndMinute = endMinute,
            Title = title,
            EventType = eventType,
            HostCount = hostCount,
            HasGuest = hasGuest
        };
        db.Events.Add(entity);
        db.SaveChanges();

        // 5) Return the generated Id.
        return entity.Id;
    }

    // Delete a booking by Id (used by POST /db/event/{eventId}/delete).
    // Returns true when a row was found and removed; false if the Id did not exist.
    public static bool DeleteEvent(SchedulerContext db, int eventId)
    {
        var entity = db.Events.Find(eventId);
        if (entity is null) return false;
        db.Events.Remove(entity);
        db.SaveChanges();
        return true;
    }

    // Change the time of an existing booking for POST /db/event/{eventId}/reschedule.
    // Returns true when the new time is valid and conflict-free, otherwise false.
    public static bool RescheduleEvent(
        SchedulerContext db,
        int eventId,
        DateTime newDate,
        int newHour,
        int newStartMinute,
        int newEndMinute)
    {
        // 1) Find existing entity (db.Events.Find(eventId)); if null => return false.
        // 2) Check conflicts at (newDate, newHour) with the same overlap rule, BUT ignore this event's own Id.
        // 3) If conflict => return false.
        // 4) Update the entity's fields and SaveChanges(); return true.
        var entity = db.Events.Find(eventId);
        if (entity is null) return false;

        // Basic validation for the new time
        if (newHour < 0 || newHour > 23) return false;
        if (newStartMinute < 0 || newStartMinute > 59) return false;
        if (newEndMinute <= newStartMinute || newEndMinute > 60) return false;

        var day = newDate.Date;
        var sameHour = db.Events
            .Where(e => e.Date == day && e.Hour == newHour && e.Id != eventId)
            .ToList();

        foreach (var e in sameHour)
        {
            bool overlaps = !(newEndMinute <= e.StartMinute || e.EndMinute <= newStartMinute);
            if (overlaps) return false;
        }

        entity.Date = day;
        entity.Hour = newHour;
        entity.StartMinute = newStartMinute;
        entity.EndMinute = newEndMinute;
        db.SaveChanges();
        return true;
    }

    // Read all bookings. Used by GET /db/event to inspect current rows.
    public static List<EventEntity> ListEvents(SchedulerContext db)
    {
        // TIP: Start simple. You can add sorting or filtering later if you want to.
        return db.Events.ToList();
    }
}

// === Experiments: Database-backed booking actions (EventActionsDb) ===
// Experiment 1: Overlap rule and conflicts.
//   Step 1: In CreateEvent, temporarily relax or remove the overlap check in step 3.
//   Step 2: Use /db/event/post to create two overlapping bookings for the same date and hour.
//   Step 3: Call /db/schedule/7days and observe how overlapping bookings appear, then restore the original overlap rule.
// Experiment 2: Input validation and error responses.
//   Step 1: Temporarily tighten or loosen the validation in step 1 (e.g., disallow late hours).
//   Step 2: Try to create bookings that violate the new rules and see how /db/event/post responds.
//   Step 3: Decide which validation shape you prefer and revert or keep the change.
// Experiment 3: Sorting and listing.
//   Step 1: In ListEvents, change the query to order events by Date then Hour.
//   Step 2: Call GET /db/event and inspect the order in the response.
//   Step 3: Compare unsorted vs sorted lists and choose the version that best serves your clients.


// --- FILE: Actions/ScheduleProjectionDb.cs
// A.3b Minute-grid painting from the database into the 7-day ScheduleData model.
// - Beginner view: transform EventEntity rows from the DB into the 7‑day grid model used by the UI.
// - Takes bookings from SchedulerContext.Events and "paints" them onto ScheduleData (see Models/ScheduleModels.cs [A.3a]).
// - Called from Endpoints/ScheduleDbEndpoints.cs for /db/schedule/today and /db/schedule/7days.
// B.10c Projection engine for the schedule data loading lane.
// - Both /db/schedule/today [B.10a] and /db/schedule/7days [B.10b] call into these methods
//   before React (App.jsx B.10/B.12) receives JSON and turns it into HourRow/HourCell [A.3c].
// How to read this file
// - Start at BuildSevenDaySchedule to see the full week flow.
// - Watch for the [startDate, endDate) and [StartMinute, EndMinute) half‑open ranges that prevent off-by-one bugs.

public static class ScheduleProjectionDb
{
    // Build a full 7‑day schedule starting from 'start' (usually DateTime.Today)
    public static ScheduleData BuildSevenDaySchedule(SchedulerContext db, DateTime start)
    {
        var startDate = start.Date; // DateTime is built-in; .Date drops the time-of-day so comparisons are clean
        var endDate = startDate.AddDays(7); // +7 days; I treat end as exclusive (include >= startDate and < endDate)

        var schedule = new ScheduleData(); // Step A: start from a blank 7-day grid (ScheduleData pre-creates 7 DaySchedule entries).

        // Step B: get all bookings in [startDate, endDate) from the DB in one query
        var rows = db.Events // 'db' is EF DbContext; Events is my table (DbSet<EventEntity>)
            .Where(e => e.Date >= startDate && e.Date < endDate) // build SQL filter (not executed yet)
            .ToList(); // run the query now and give me C# objects

        foreach (var row in rows)
        {
            // Step C: find the matching Day and Hour in our 7-day grid
            var day = schedule.Days.FirstOrDefault(d => d.Date == row.Date); // FirstOrDefault: first match or null
            if (day is null) continue; // row outside our 7-day list

            var hour = day.Hours.FirstOrDefault(h => h.Hour == row.Hour); // same idea for Hour bin
            if (hour is null) continue; // guard

            // A.3b / Step D: mark each booked minute as true (half-open [StartMinute, EndMinute)).
            // - This is where DB bookings become the minute-level true/false flags described in Program.cs A.3 and Models A.3a.
            var startM = Math.Max(0, row.StartMinute);
            var endM = Math.Min(60, row.EndMinute);
            for (int m = startM; m < endM; m++)
            {
                hour.Minutes[m] = true;
            }

            // Add booking detail for the visitor UI
            hour.Bookings.Add(new BookingDetail
            {
                Title = row.Title ?? "Untitled",
                StartMinute = startM,
                EndMinute = endM
            });
        }

        return schedule;
    }

    // Convenience: return only today's DaySchedule
    public static DaySchedule? BuildToday(SchedulerContext db, DateTime today)
    {
        var full = BuildSevenDaySchedule(db, today);
        return full.Days.FirstOrDefault(d => d.Date == today.Date);
    }
}

// === Experiments: Schedule projection from DB (ScheduleProjectionDb) ===
// Experiment 1: Shorten or extend the window.
//   Step 1: Change AddDays(7) in BuildSevenDaySchedule to another value (e.g., 3 or 10).
//   Step 2: Call /db/schedule/7days and see how many days come back.
//   Step 3: Restore the original 7-day window once you understand the impact.
// Experiment 2: Date range boundaries.
//   Step 1: Temporarily change the filter from (e.Date < endDate) to (e.Date <= endDate) or (e.Date > startDate).
//   Step 2: Create bookings on the boundary days and inspect whether they appear twice or disappear.
//   Step 3: Restore the original [startDate, endDate) rule to avoid off-by-one bugs.
// Experiment 3: Minute painting.
//   Step 1: Change the Math.Max/Math.Min or the minute loop in Step D.
//   Step 2: Create bookings and compare how cells look in the frontend grid.
//   Step 3: Revert to the original painting logic once you've seen the effect.
#endif
