//#if false wrapper: this merged teaching file is excluded from compilation to avoid duplicate type definitions.
#if false
// --- FILE: Endpoints/EventDbEndpoints.cs ---
// Beginner view: HTTP "doors" that WRITE bookings to the SQLite database (EF Core).
// - Each endpoint takes simple parameters, calls Actions/EventActionsDb, and returns an HTTP result.
// - Group prefix: /db/event (this is what the real frontend admin flow uses for bookings).

using API; 
using API.Data;
using API.Models;
using API.Actions;

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
            group.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, string? title, string? eventType, int? hostCount, bool hasGuest, SchedulerContext db) =>
            {
                int id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute, title, eventType, hostCount, hasGuest);
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


// --- FILE: EventEndpoints.cs ---
// A.5b Legacy in-memory "whiteboard" event endpoints under /schedule/event.
// - Mirrors the DB-backed /db/event endpoints, but stores bookings only in memory (ScheduleData + EventActions [A.5c]).
// - Useful for experiments and comparison; the real React UI uses the DB-backed path (/db/event/*) in EventDbEndpoints.
//
// CONCEPT: ROUTE PARAMETERS & MODEL BINDING
// - URLs like "/schedule/event/{eventId}" use route parameters `{}` bound to method parameters.
// - Simple parameters like (DateTime date, int hour, ...) are model-bound from the request (query/body) by ASP.NET Core.

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

// [Endpoint.Schedule.1] Schedule Endpoints
// These endpoints are the "Doors" that the frontend uses to READ the schedule.
// They use the database (via ScheduleProjectionDb) to get the data.
//
// -> See Lesson: Interactive-Lesson/04-DB-Booking-CRUD.md (and 05-Schedule-Projection.md)

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


// --- FILE: Endpoints/ScheduleEndpoints.cs ---
// A.5a Legacy READ endpoints backed by in-memory ScheduleData (not the DB path).
// - These /schedule/* routes expose the in-memory "whiteboard" schedule described in Program.cs A.5 and Models/ScheduleModels.cs A.3a.
// - Useful for demos and labs; consumer/React UI now uses the DB-backed /db/schedule/* endpoints (see ScheduleDbEndpoints.cs).

namespace API.Endpoints
{
    public static class ScheduleEndpoints
    {
        // Extension method: I attach these routes to WebApplication (app).
        public static void MapScheduleEndpoints(this WebApplication app)
        {
            // Group: /schedule (legacy in-memory reads) [B.25b]
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
#endif