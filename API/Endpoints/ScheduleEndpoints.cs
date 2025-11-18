// --- FILE: Endpoints/ScheduleEndpoints.cs ---
// Beginner view: legacy READ endpoints backed by in-memory ScheduleData (not the DB path).
// - Useful for demos; consumer UI now uses /db/schedule/* (see ScheduleDbEndpoints.cs).
using API; // Use our new models
using API.Actions; // Keep using the actions from the original file
using API.Models;

namespace API.Endpoints
{
    public static class ScheduleEndpoints
    {
        // Extension method: I attach these routes to WebApplication (app).
        public static void MapScheduleEndpoints(this WebApplication app)
        {
            // Group: /schedule (legacy in-memory reads)
            var scheduleGroup = app.MapGroup("/schedule");

            // GET /schedule => return the whole in-memory 7-day schedule (Models/ScheduleModels.cs)
            scheduleGroup.MapGet("/", (ScheduleData schedule) =>
            {
                // 'schedule' is injected (registered in Program.cs as a singleton).
                return Results.Ok(schedule);
            });

            // GET /schedule/today => filter the in-memory schedule to today
            scheduleGroup.MapGet("/today", (ScheduleData schedule) =>
            {
                // FirstOrDefault: return first match or null if none.
                DaySchedule? today = schedule.Days
                    .FirstOrDefault(d => d.Date.Date == DateTime.Today);

                // Not found => 404. Found => 200 with DaySchedule.
                if (today is null)
                {
                    return Results.NotFound("No schedule found for today.");
                }

                // If we found the schedule, we return it with an HTTP 200 OK response.
                // Results.Ok() wraps our data in a standard success response.
                return Results.Ok(today);
            });

            // GET /schedule/7days => build a copy of the 7-day view from in-memory data (Actions.cs)
            scheduleGroup.MapGet("/7days", (ScheduleData schedule) =>
            {
                var sevenDaySchedule = ScheduleQueries.GetSevenDays(schedule);
                return Results.Ok(sevenDaySchedule);
            });
        }
    }
}