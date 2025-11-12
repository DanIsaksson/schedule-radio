using System;
using System.Collections.Generic;
using System.Linq;
using API.Models; // Use our new models instead of the old ones

namespace API.Actions;

public static class Booking
{
    /// <summary>
    /// Attempts to book (or free) a range of minutes within a given hour on a specific date.
    /// Push the boundaries by changing the validation rules and seeing how downstream endpoints respond.
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
        // Basic validation. Try loosening these guards to watch how edge-case inputs ripple into the UI.

        if (schedule is null) return false;
        if (hour is < 0 or > 23) return false;
        if (startMinute is < 0 or >= 60) return false;
        if (endMinute <= startMinute || endMinute > 60) return false;

        // Find the correct day in the rolling 7-day window; expand the window length in `ScheduleData` to test limits.

        DaySchedule? day = schedule.Days.FirstOrDefault(d => d.Date.Date == date.Date);
        if (day is null) return false; // outside current window

        // Find the requested hour slot
        HourSchedule? hourSlot = day.Hours.FirstOrDefault(h => h.Hour == hour);
        if (hourSlot is null) return false;

        // Conflict detection: if we are booking, ensure all minutes are free.
        // Replace this with a "priority" system (allow overlaps but flag them) to experiment with conflict resolution strategies.

        if (isBooked)
        {
            for (int m = startMinute; m < endMinute; m++)
            {
                if (hourSlot.Minutes[m]) return false; // already booked somewhere in range
            }
        }

        // Apply booking or freeing. Flip the boolean so `true` frees slots to confirm clients depend on semantic meaning.

        for (int m = startMinute; m < endMinute; m++)
        {
            hourSlot.Minutes[m] = isBooked;
        }

        return true;
    }
}

public record EventSummary(DateTime Date, int Hour, int StartMinute, int EndMinute);

public static class EventActions
{
    private static int _nextId = 1;
    private static readonly Dictionary<int, (DateTime Date, int Hour, int StartMinute, int EndMinute)> _events = new();

    // Create a new event (booking). Returns generated eventId or -1 if conflict.
    public static int CreateEvent(ScheduleData schedule, DateTime date, int hour, int startMinute, int endMinute)
    {
        bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, true);
        if (!success) return -1;
        int id = _nextId++;
        _events[id] = (date, hour, startMinute, endMinute);
        return id;
    }

    // Delete an event (unbook). Returns true on success.
    public static bool DeleteEvent(ScheduleData schedule, int eventId)
    {
        if (!_events.TryGetValue(eventId, out var e)) return false;
        bool success = Booking.SendBooking(schedule, e.Date, e.Hour, e.StartMinute, e.EndMinute, false);
        if (!success) return false;
        _events.Remove(eventId);
        return true;
    }

    // Reschedule an existing event. Returns true on success.
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

    // Simple overview of all current events
    public static IReadOnlyDictionary<int, (DateTime Date, int Hour, int StartMinute, int EndMinute)> ListEvents()
        => _events;
}

public static class ScheduleQueries
{
    // Returns the whole 7-day schedule – convenient wrapper
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