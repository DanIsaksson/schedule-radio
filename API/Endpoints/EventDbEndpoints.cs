// --- FILE: Endpoints/EventDbEndpoints.cs ---
// Beginner view: HTTP "doors" that WRITE bookings to the SQLite database (EF Core).
// - Each endpoint takes simple parameters, calls Actions/EventActionsDb, and returns an HTTP result.
// - Group prefix: /db/event (this is what the real frontend admin flow uses for bookings).
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

            // GET /db/event -> list all bookings from the DB using EventActionsDb.ListEvents (good starting point to inspect data)
            group.MapGet("/", (SchedulerContext db) =>
            {
                var events = EventActionsDb.ListEvents(db);
                return Results.Ok(events);
            });

            // GET /db/event/{eventId} -> look up a single booking by Id using db.Events.Find; 404 if it does not exist
            group.MapGet("/{eventId}", (int eventId, SchedulerContext db) =>
            {
                var e = db.Events.Find(eventId);
                return e is null ? Results.NotFound($"No event with id {eventId}") : Results.Ok(e);
            });

            // B.15a POST /db/event/post -> backend "door" for the booking creation flow.
            // - Called by the React admin flow handler App.jsx submitBooking [B.15].
            // - Delegates to EventActionsDb.CreateEvent [B.15b] to validate, check overlaps, and write the booking row.
            group.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, SchedulerContext db) =>
            {
                int id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute);
                return id == -1
                    ? Results.BadRequest("Conflict or invalid input")
                    : Results.Ok(new { EventId = id });
            });

            // POST /db/event/{eventId}/reschedule -> change a booking's time via EventActionsDb.RescheduleEvent (same overlap rules as CreateEvent)
            group.MapPost("/{eventId}/reschedule", (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.RescheduleEvent(db, eventId, newDate, newHour, newStartMinute, newEndMinute);
                return ok ? Results.Ok() : Results.BadRequest("Reschedule failed (conflict or invalid id)");
            });

            // POST /db/event/{eventId}/delete -> delete a booking by Id via EventActionsDb.DeleteEvent
            group.MapPost("/{eventId}/delete", (int eventId, SchedulerContext db) =>
            {
                bool ok = EventActionsDb.DeleteEvent(db, eventId);
                return ok ? Results.Ok() : Results.BadRequest("Delete failed (invalid id)");
            });
        }
    }

    // === Experiments: Database event endpoints (/db/event/*) ===
    // Lab ideas for exploring API design; they are not required for the app to run.
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
