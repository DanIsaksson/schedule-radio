// --- FILE: EventEndpoints.cs ---
//
// This file is part of our new, modular endpoint system.
//
// PURPOSE:
// To define all API endpoints related to creating, updating, deleting, and viewing
// specific events. An "event" is a booked time slot with a unique ID.
//
// CONCEPT: ROUTE PARAMETERS & MODEL BINDING
// You will see URLs like "/event/{eventId}". The part in curly braces `{}` is a
// 'route parameter'. It's a placeholder that captures a value from the URL.
// ASP.NET Core automatically converts this value to the type specified in the method
// (e.g., 'int eventId').
//
// You will also see parameters like '(DateTime date, int hour, ...)'. This is called
// 'model binding'. ASP.NET Core automatically reads the JSON body of the POST request
// and matches the JSON properties to the method parameters by name.
//
using Scheduler.Models; // Use our new models
using ScheduleAction; // Keep using the actions from the original file

namespace Scheduler.Endpoints
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
            // {eventId} is a 'route parameter'.
            eventGroup.MapGet("/{eventId}", (int eventId) =>
            {
                var events = EventActions.ListEvents();

                // We use .TryGetValue() which is a safe way to access a dictionary.
                // It returns true if the key exists and false if it doesn't.
                return events.TryGetValue(eventId, out var e)
                    ? Results.Ok(e) // If found, return the event
                    : Results.NotFound($"No event with id {eventId}"); // If not found, return 404
            });

            // --- ENDPOINT 6: POST /schedule/event/post ---
            // Creates a new event (a new booking) and returns its new ID.
            // The parameters here are 'model bound' from the JSON request body.
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
}