// --- FILE: Actions/ScheduleProjectionDb.cs
// What this file does (beginner view)
// - I read bookings from the database and paint them onto a 7‑day schedule grid that the frontend understands.
// - Returned shape matches Models/ScheduleModels.cs so old code can reuse it.

using System;
using System.Linq;
using API.Data; // SchedulerContext
using API.Models; // ScheduleData, DaySchedule, HourSchedule, EventEntity

namespace API.Actions
{
    public static class ScheduleProjectionDb
    {
        // Build a full 7‑day schedule starting from 'start' (usually DateTime.Today)
        public static ScheduleData BuildSevenDaySchedule(SchedulerContext db, DateTime start)
        {
            var startDate = start.Date; // DateTime is built-in; .Date drops the time-of-day so comparisons are clean
            var endDate = startDate.AddDays(7); // +7 days; I treat end as exclusive (include >= startDate and < endDate)

            var schedule = new ScheduleData(); // Step A: start from a blank 7-day grid (today..+6)

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

                // Step D: mark each booked minute as true (half-open [StartMinute, EndMinute))
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
