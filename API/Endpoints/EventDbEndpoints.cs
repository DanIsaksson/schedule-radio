// --- FILE: Endpoints/EventDbEndpoints.cs ---
// Beginner view: endpoints that WRITE bookings to the SQLite database (EF Core).
// - I receive simple parameters, call Actions/EventActionsDb.cs, and return Results.*
// - Group prefix: /db/event (used by the real frontend for booking operations).
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

            // GET /db/event => I read all rows (Actions/EventActionsDb.ListEvents) and return them
            group.MapGet("/", (SchedulerContext db) =>
            {
                var events = EventActionsDb.ListEvents(db);
                return Results.Ok(events);
            });

            // GET /db/event/{eventId} => I find one row by Id using db.Events.Find(id)
            group.MapGet("/{eventId}", (int eventId, SchedulerContext db) =>
            {
                var e = db.Events.Find(eventId);
                return e is null ? Results.NotFound($"No event with id {eventId}") : Results.Ok(e);
            });

            // POST /db/event/post => I create a booking (Actions/EventActionsDb.CreateEvent) and return new Id
            group.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, SchedulerContext db) =>
            {
                int id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute);
                return id == -1
                    ? Results.BadRequest("Conflict or invalid input")
                    : Results.Ok(new { EventId = id });
            });

            // POST /db/event/{eventId}/reschedule => I change a booking's time (Actions/EventActionsDb.RescheduleEvent)
            group.MapPost("/{eventId}/reschedule", (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.RescheduleEvent(db, eventId, newDate, newHour, newStartMinute, newEndMinute);
                return ok ? Results.Ok() : Results.BadRequest("Reschedule failed (conflict or invalid id)");
            });

            // POST /db/event/{eventId}/delete => I delete a booking by Id (Actions/EventActionsDb.DeleteEvent)
            group.MapPost("/{eventId}/delete", (int eventId, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.DeleteEvent(db, eventId);
                return ok ? Results.Ok() : Results.BadRequest("Delete failed (invalid id)");
            });
        }
    }

    // === Experiments: Database event endpoints (/db/event/*) ===
    // Experiment 1: Conflict status codes.
    //   Step 1: In the POST /db/event/post handler, change BadRequest to Conflict (409) for id == -1.
    //   Step 2: Create an overlapping booking and inspect how clients react to 400 vs 409.
    //   Step 3: Choose the status code that best matches your API contract and keep it.
    // Experiment 2: Read vs. write separation.
    //   Step 1: Add a new GET /db/event/upcoming endpoint that filters events to future dates only.
    //   Step 2: Call it from a client and compare its payload to GET /db/event.
    //   Step 3: Decide whether a separate read model endpoint is useful for your UI.
    // Experiment 3: Error messages.
    //   Step 1: Temporarily enrich error messages (include date/hour) in reschedule/delete failures.
    //   Step 2: Trigger failures and see whether the extra context helps debugging.
    //   Step 3: Decide how much detail you want to expose in production.
}
