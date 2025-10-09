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
            group.MapGet("/", (SchedulerContext db) =>
            {
                var events = EventActionsDb.ListEvents(db);
                return Results.Ok(events);
            });

            // GET /db/event/{eventId} => details of one booking
            group.MapGet("/{eventId}", (int eventId, SchedulerContext db) =>
            {
                var e = db.Events.Find(eventId);
                return e is null ? Results.NotFound($"No event with id {eventId}") : Results.Ok(e);
            });

            // POST /db/event/post => create a booking (returns new Id)
            group.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, SchedulerContext db) =>
            {
                int id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute);
                return id == -1
                    ? Results.BadRequest("Conflict or invalid input")
                    : Results.Ok(new { EventId = id });
            });

            // POST /db/event/{eventId}/reschedule => change time
            group.MapPost("/{eventId}/reschedule", (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.RescheduleEvent(db, eventId, newDate, newHour, newStartMinute, newEndMinute);
                return ok ? Results.Ok() : Results.BadRequest("Reschedule failed (conflict or invalid id)");
            });

            // POST /db/event/{eventId}/delete => remove booking
            group.MapPost("/{eventId}/delete", (int eventId, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.DeleteEvent(db, eventId);
                return ok ? Results.Ok() : Results.BadRequest("Delete failed (invalid id)");
            });
        }
    }
}
