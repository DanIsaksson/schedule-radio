// --- FILE: Endpoints/ScheduleDbEndpoints.cs
// PURPOSE
// - Expose DB-backed schedule projections that mirror the in-memory shape
//   GET /db/schedule/today  => DaySchedule
//   GET /db/schedule/7days  => ScheduleData
//
// Cross-refs:
// - Uses ScheduleProjectionDb.BuildSevenDaySchedule() and BuildToday() (ScheduleProjectionDb.cs:18–45)
// - Mapped from Program.cs:XX via app.MapScheduleDbEndpoints()

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

            // GET /db/schedule/today
            group.MapGet("/today", (SchedulerContext db) =>
            {
                DaySchedule? today = ScheduleProjectionDb.BuildToday(db, DateTime.Today);
                return today is null ? Results.NotFound("No schedule for today") : Results.Ok(today);
            });

            // GET /db/schedule/7days
            group.MapGet("/7days", (SchedulerContext db) =>
            {
                ScheduleData all = ScheduleProjectionDb.BuildSevenDaySchedule(db, DateTime.Today);
                return Results.Ok(all);
            });
        }
    }
}
