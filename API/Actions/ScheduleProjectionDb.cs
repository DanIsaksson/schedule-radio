// --- FILE: Actions/ScheduleProjectionDb.cs
// PURPOSE
// - Build a 7-day (today..+6) schedule projection from database rows, shaped like ScheduleData
// - Keep the shape compatible with API.Models.ScheduleData/DaySchedule/HourSchedule
//
// Cross-refs:
// - Called by ScheduleDbEndpoints.cs:XX for /db/schedule/* endpoints
// - Uses EventEntity rows from SchedulerContext (SchedulerContext.cs:45)

using System;
using System.Linq;
using API.Data; // SchedulerContext
using API.Models; // ScheduleData, DaySchedule, HourSchedule, EventEntity

namespace API.Actions
{
    public static class ScheduleProjectionDb
    {
        // Build full 7-day schedule starting from a given date (usually DateTime.Today)
        public static ScheduleData BuildSevenDaySchedule(SchedulerContext db, DateTime start)
        {
            var startDate = start.Date;
            var endDate = startDate.AddDays(7); // exclusive upper bound

            var schedule = new ScheduleData(); // initializes 7 days today..+6

            // Pre-fetch all events in range to minimize queries
            var rows = db.Events
                .Where(e => e.Date >= startDate && e.Date < endDate)
                .ToList();

            foreach (var row in rows)
            {
                // Find matching day in our schedule window
                var day = schedule.Days.FirstOrDefault(d => d.Date == row.Date);
                if (day is null) continue; // row outside our 7-day list

                var hour = day.Hours.FirstOrDefault(h => h.Hour == row.Hour);
                if (hour is null) continue; // guard

                // Mark minutes as booked using half-open interval [StartMinute, EndMinute)
                var startM = Math.Max(0, row.StartMinute);
                var endM = Math.Min(60, row.EndMinute);
                for (int m = startM; m < endM; m++)
                {
                    hour.Minutes[m] = true;
                }
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
}
