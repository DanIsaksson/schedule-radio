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
// Where:
// - Used by legacy in-memory endpoints in API/Endpoints/ScheduleEndpoints.cs and API/Endpoints/EventEndpoints.cs.
// - Not used by the staff portal booking + payment flows (those use /db/* + /api/*).

using System;
using System.Collections.Generic;
using System.Linq;
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