// --- FILE: Endpoints/ScheduleDbEndpoints.cs
// Beginner view: endpoints that return schedule read models from the database.
// - GET /db/schedule/today  => one day (used for the Today card in the UI)
// - GET /db/schedule/7days  => seven days (used for the week grid views)
using API.Actions; // ScheduleProjectionDb
using API.Data; // SchedulerContext
using API.Models; // ScheduleData, DaySchedule

namespace API.Endpoints
{
    public static class ScheduleDbEndpoints
    {
        public static void MapScheduleDbEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/db/schedule");

            // GET /db/schedule/today (I ask the helper to build only today)
            group.MapGet("/today", (SchedulerContext db) =>
            {
                DaySchedule? today = ScheduleProjectionDb.BuildToday(db, DateTime.Today);
                return today is null ? Results.NotFound("No schedule for today") : Results.Ok(today);
            });

            // GET /db/schedule/7days (I ask the helper to build a 7-day window)
            group.MapGet("/7days", (SchedulerContext db) =>
            {
                ScheduleData all = ScheduleProjectionDb.BuildSevenDaySchedule(db, DateTime.Today);
                return Results.Ok(all);
            });
        }
    }

    // === Experiments: Database schedule endpoints (/db/schedule/*) ===
    // Experiment 1: Today vs week alignment.
    //   Step 1: Temporarily change BuildToday to call BuildSevenDaySchedule with a different start date.
    //   Step 2: Call /db/schedule/today and /db/schedule/7days and compare the returned dates.
    //   Step 3: Restore the original alignment so both endpoints agree on what "today" means.
    // Experiment 2: Custom windows.
    //   Step 1: Add a new endpoint /db/schedule/3days that calls a modified projection helper.
    //   Step 2: Call it from the frontend and experiment with a shorter preview grid.
    //   Step 3: Decide whether multiple window sizes are useful or if 7 days is enough.
}
