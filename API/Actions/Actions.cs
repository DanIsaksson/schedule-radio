using Schedule;
using System;
using System.Linq;

namespace Schedule;

internal static class ScheduleAction
{

public static bool SetBooking(
    ScheduleData schedule,
    DateTime date,
    int hour,
    int startMinute,
    int endMinute,
    bool isBooked)
{
    // Basic null check – fail fast if no schedule is supplied
    if (schedule is null)
        throw new ArgumentNullException(nameof(schedule));

    // Only dates within the existing 7-day collection are valid
    var day = schedule.Days.FirstOrDefault(d => d.Date.Date == date.Date);
    if (day is null)
    {
        // Date outside the 7-day rolling window – treat as failure but do not throw to keep flow clear
        return false;
    }

    // Validate hour range (00-23)
    if (hour < 0 || hour > 23)
        throw new ArgumentOutOfRangeException(nameof(hour), "Hour must be between 0 and 23 (inclusive).");

    // Validate minute boundaries – start < end and both within 0-60
    if (startMinute < 0 || startMinute >= endMinute || endMinute > 60)
        throw new ArgumentOutOfRangeException("Minute range must satisfy 0 ≤ start < end ≤ 60.");

    var slot = day.Hours[hour];

    // Apply (or remove) booking across the requested minute span
    foreach (var m in Enumerable.Range(startMinute, endMinute - startMinute))
        slot.Minutes[m] = isBooked;

    return true;
}

}