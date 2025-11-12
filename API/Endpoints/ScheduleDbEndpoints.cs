// --- FILE: Endpoints/ScheduleDbEndpoints.cs
// Beginner view: endpoints that return schedule read models from the database.
// - GET /db/schedule/today  => one day
// - GET /db/schedule/7days  => seven days
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
}
