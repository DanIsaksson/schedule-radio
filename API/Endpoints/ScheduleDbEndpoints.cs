// [Endpoint.Schedule.1] Schedule Endpoints
// These endpoints are the "Doors" that the frontend uses to READ the schedule.
// They use the database (via ScheduleProjectionDb) to get the data.
//
// -> See Lesson: Interactive-Lesson/04-DB-Booking-CRUD.md (and 05-Schedule-Projection.md)

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

            // [Endpoint.Schedule.2] Get Today's Schedule
            // URL: GET /db/schedule/today
            // Returns: A single DaySchedule object for today.
            group.MapGet("/today", (SchedulerContext db) =>
            {
                DaySchedule? today = ScheduleProjectionDb.BuildToday(db, DateTime.Today);
                return today is null ? Results.NotFound("No schedule for today") : Results.Ok(today);
            });

            // [Endpoint.Schedule.3] Get 7-Day Schedule
            // URL: GET /db/schedule/7days
            // Returns: A ScheduleData object containing 7 days of data.
            group.MapGet("/7days", (SchedulerContext db) =>
            {
                ScheduleData all = ScheduleProjectionDb.BuildSevenDaySchedule(db, DateTime.Today);
                return Results.Ok(all);
            });
        }
    }

    // === Experiments: Database schedule endpoints (/db/schedule/*) ===
    // Lab ideas for exploring different windows and date boundaries; not required for the core app.
    // Experiment 1: Today vs week alignment.
    //   Step 1: Temporarily change BuildToday to call BuildSevenDaySchedule with a different start date.
    //   Step 2: Call /db/schedule/today and /db/schedule/7days and compare the returned dates.
    //   Step 3: Restore the original alignment so both endpoints agree on what "today" means.
    // Experiment 2: Custom windows.
    //   Step 1: Add a new endpoint /db/schedule/3days that calls a modified projection helper.
    //   Step 2: Call it from the frontend and experiment with a shorter preview grid.
    //   Step 3: Decide whether multiple window sizes are useful or if 7 days is enough.
}
