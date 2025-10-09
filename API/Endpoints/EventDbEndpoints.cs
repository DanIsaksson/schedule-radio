// --- FILE: Endpoints/EventDbEndpoints.cs ---
//
// PURPOSE
// - A parallel set of endpoints that use the database (EF Core) instead of the in-memory schedule.
// - Each endpoint returns 501 (Not Implemented) until you wire in the calls to EventActionsDb.
//
// SYNTAX PRIMER (Minimal APIs quick tour)
// - Route groups: 'var group = app.MapGroup("/db/event");' prefixes all routes with /db/event.
// - Model binding: parameters in the lambda are filled from route, query, or body by name/type.
//   Example: (int eventId) binds {eventId} from the route.
// - DI for DbContext: add 'SchedulerContext db' as a lambda parameter to get a context per request.
// - Results helpers: Results.Ok(data), Results.BadRequest(msg), Results.NotFound(msg), Results.StatusCode(501).
// - Keep endpoints thin: do validation/queries in Actions layer, return the translated Result here.
//
// HOW TO FILL IN
// - Replace the 501 bodies with simple calls to EventActionsDb.* methods.
// - Inject SchedulerContext by adding it as a parameter to the lambda (Minimal APIs will provide it).
// - Keep the logic very small inside the endpoint; push work into EventActionsDb.

using API.Data; // SchedulerContext
using API.Models; // EventEntity
using API.Actions; // EventActionsDb

namespace API.Endpoints
{
    public static class EventDbEndpoints
    {
        public static void MapEventDbEndpoints(this WebApplication app)
        {
            // Group all DB event endpoints under /db/event
            var group = app.MapGroup("/db/event");

            // GET /db/event => list all persisted bookings
            group.MapGet("/", () =>
            {
                // STEP BY STEP (replace 501 when ready):
                // 1) Add parameter: (SchedulerContext db) => {...}
                // 2) Call: var events = EventActionsDb.ListEvents(db);
                // 3) Return: Results.Ok(events);
                return Results.StatusCode(501); // Not Implemented (placeholder)
            });

            // GET /db/event/{eventId} => details of one booking
            group.MapGet("/{eventId}", (int eventId) =>
            {
                // 1) Inject db: (int eventId, SchedulerContext db) => {...}
                // 2) Find: var e = db.Events.Find(eventId);
                // 3) Return 404 if e is null; else 200 OK with e.
                return Results.StatusCode(501);
            });

            // POST /db/event/post => create a booking (returns new Id)
            group.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute) =>
            {
                // 1) Inject db: (..., SchedulerContext db)
                // 2) Call: var id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute);
                // 3) If id == -1 => return Results.BadRequest("Conflict or invalid input"); else Results.Ok(new { EventId = id })
                return Results.StatusCode(501);
            });

            // POST /db/event/{eventId}/reschedule => change time
            group.MapPost("/{eventId}/reschedule", (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute) =>
            {
                // 1) Inject db: (..., SchedulerContext db)
                // 2) Call: var ok = EventActionsDb.RescheduleEvent(db, eventId, newDate, newHour, newStartMinute, newEndMinute);
                // 3) Return Ok() or BadRequest()
                return Results.StatusCode(501);
            });

            // POST /db/event/{eventId}/delete => remove booking
            group.MapPost("/{eventId}/delete", (int eventId) =>
            {
                // 1) Inject db: (int eventId, SchedulerContext db)
                // 2) Call: var ok = EventActionsDb.DeleteEvent(db, eventId);
                // 3) Return Ok() or BadRequest()
                return Results.StatusCode(501);
            });
        }
    }
}
