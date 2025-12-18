// A.1 [Legacy.Event] Legacy in-memory event endpoints ("doors") under /schedule/event.
// What: Creates/reschedules/deletes bookings in RAM (ScheduleData + EventActions) instead of SQLite.
// Why: Teaching sandbox for learning routing/model binding without touching the DB-backed production lane.
// Where:
// - Mapped in Program.cs via app.MapEventEndpoints().
// - Used by the API's demo pages in API/wwwroot (and Swagger).
//
// --- FILE: EventEndpoints.cs ---
// A.5b Legacy in-memory "whiteboard" event endpoints under /schedule/event.
// - Mirrors the DB-backed /db/event endpoints, but stores bookings only in memory (ScheduleData + EventActions [A.5c]).
// - Useful for experiments and comparison; the real React UI uses the DB-backed path (/db/event/*) in EventDbEndpoints.
//
// CONCEPT: ROUTE PARAMETERS & MODEL BINDING
// - URLs like "/schedule/event/{eventId}" use route parameters `{}` bound to method parameters.
// - Simple parameters like (DateTime date, int hour, ...) are model-bound from the request (query/body) by ASP.NET Core.
//
using API; // Use our new models
using API.Actions; // Keep using the actions from the original file
using API.Models;

namespace API.Endpoints
{
    public static class EventEndpoints
    {
        public static void MapEventEndpoints(this WebApplication app)
        {            
            var eventGroup = app.MapGroup("/schedule/event");

            // --- ENDPOINT 4: GET /schedule/event ---
            // Lists all created events. Each event has an ID and time slot information.
            eventGroup.MapGet("/", () =>
            {
                // This calls our business logic layer to get the data.
                var allEvents = EventActions.ListEvents();
                return Results.Ok(allEvents);
            });

            // --- ENDPOINT 5: GET /schedule/event/{eventId} ---
            // Gets the details for a single event using its unique ID.
            // {eventId} is a 'route parameter' that binds the URL segment to the eventId argument.
            eventGroup.MapGet("/{eventId}", (int eventId) =>
            {
                var events = EventActions.ListEvents();

                // We use .TryGetValue() which is a safe way to access a dictionary.
                // It returns true if the key exists and false if it doesn't.
                return events.TryGetValue(eventId, out var e)
                    ? Results.Ok(e) // If found, return the event
                    : Results.NotFound($"No event with id {eventId}"); // If not found, return 404.
            });

            // B.25a Legacy create-booking endpoint: POST /schedule/event/post.
            // - Uses in-memory ScheduleData and EventActions.CreateEvent [B.25a] (no DB persistence).
            // - Counterpart in the DB-backed flow is POST /db/event/post [B.15a] (EventDbEndpoints + EventActionsDb.CreateEvent [B.15b]).
            // The parameters here are model-bound from the request into simple primitives.
            eventGroup.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, ScheduleData schedule) =>
            {
                int id = EventActions.CreateEvent(schedule, date, hour, startMinute, endMinute);

                // If the CreateEvent method returns -1, it means there was a conflict.
                return id == -1
                    ? Results.BadRequest("Booking conflict or invalid parameters")
                    : Results.Ok(new { EventId = id }); // Return the new ID in a JSON object
            });

            // --- ENDPOINT 7: POST /schedule/event/{eventId}/reschedule ---
            // Changes the time for an existing event.
            eventGroup.MapPost("/{eventId}/reschedule", (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute, ScheduleData schedule) =>
            {
                bool success = EventActions.RescheduleEvent(schedule, eventId, newDate, newHour, newStartMinute, newEndMinute);
                return success
                    ? Results.Ok()
                    : Results.BadRequest("Reschedule failed (conflict or invalid id)");
            });

            // --- ENDPOINT 8: POST /schedule/event/{eventId}/delete ---
            // Deletes an event and frees up its time slot.
            eventGroup.MapPost("/{eventId}/delete", (int eventId, ScheduleData schedule) =>
            {
                bool success = EventActions.DeleteEvent(schedule, eventId);
                return success
                    ? Results.Ok()
                    : Results.BadRequest("Delete failed (invalid id)");
            });


            // --- PLACEHOLDER ENDPOINTS ---
            // These endpoints are for features we haven't built yet.
            // Returning StatusCode 501 (Not Implemented) is a standard way to signal this.
            eventGroup.MapPost("/{eventId}/addhost", () => Results.StatusCode(501));
            eventGroup.MapPost("/{eventId}/removehost", () => Results.StatusCode(501));
            eventGroup.MapPost("/{eventId}/addguest", () => Results.StatusCode(501));
            eventGroup.MapPost("/{eventId}/removeguest", () => Results.StatusCode(501));
        }
    }
    
    // === Experiments: Legacy event endpoints (/schedule/event/*) ===
    // Lab ideas for exploring routing, status codes, and HTTP verbs on the legacy path.
    // Experiment 1: Route parameters and binding.
    //   Step 1: Rename the `eventId` parameter in one endpoint and rebuild.
    //   Step 2: Call /schedule/event/{id} and observe when binding succeeds or fails.
    //   Step 3: Restore the original name and confirm routing works again.
    // Experiment 2: Status codes and error payloads.
    //   Step 1: In the GET /schedule/event/{eventId} endpoint, temporarily return Results.Problem for the not-found branch.
    //   Step 2: Call the endpoint for a missing id and inspect the JSON payload.
    //   Step 3: Compare it to the plain 404 NotFound response, then restore the original code.
    // Experiment 3: HTTP verbs vs. intent.
    //   Step 1: Change the delete endpoint from POST to DELETE.
    //   Step 2: Call it from a client (or curl) using DELETE and see how it differs from POST.
    //   Step 3: Decide which verb better matches the intent and keep that version.
}