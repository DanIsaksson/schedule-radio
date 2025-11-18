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
}
