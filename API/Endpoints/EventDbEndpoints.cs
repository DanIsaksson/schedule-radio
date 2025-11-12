// --- FILE: Endpoints/EventDbEndpoints.cs ---
// Beginner view: endpoints that WRITE bookings to the database (EF Core).
// - I receive simple parameters, call Actions/EventActionsDb.cs, and return Results.*
// - Group prefix: /db/event
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
}
