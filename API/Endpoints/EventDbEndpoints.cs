// A.1 [Db.Event.Write] DB-backed booking write endpoints ("doors").
// What: Create/reschedule/delete EventEntity rows in SQLite via EF Core.
// Why: This is the authoritative booking store that the schedule projection and payment system depend on.
// Where:
// - Mapped in Program.cs via app.MapEventDbEndpoints().
// - Called by the staff portal booking form in frontend/src/App.jsx (submitBooking) and from Swagger.
//
// Beginner view:
// - Each endpoint takes simple parameters, calls Actions/EventActionsDb, and returns an HTTP result.
// - Group prefix: /db/event (this is what the real frontend booking flow uses).
using API.Data; // SchedulerContext
using API.Models; // EventEntity
using API.Actions; // EventActionsDb
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
            group.MapPost("/post", async (
                DateTime date,
                int hour,
                int startMinute,
                int endMinute,
                string? title,
                string? eventType,
                int? hostCount,
                bool hasGuest,
                string? responsibleUserId,
                HttpContext httpContext,
                UserManager<ApplicationUser> userManager,
                SchedulerContext db) =>
            {
                string? currentUserId = userManager.GetUserId(httpContext.User);
                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    return Results.Unauthorized();
                }

                string ownerUserId = currentUserId;

                if (!string.IsNullOrWhiteSpace(responsibleUserId))
                {
                    // B.1 [Booking.Ownership] Admin override for "booking on behalf of".
                    // Why: Contributors may only create bookings for themselves; admins can assign ResponsibleUserId explicitly.
                    // Where: This ownership is later used by payment calculation (Events.ResponsibleUserId -> ContributorPayments).
                    if (!httpContext.User.IsInRole("Admin"))
                    {
                        return Results.StatusCode(StatusCodes.Status403Forbidden);
                    }

                    ownerUserId = responsibleUserId.Trim();
                    var ownerUser = await userManager.FindByIdAsync(ownerUserId);
                    if (ownerUser is null)
                    {
                        return Results.BadRequest($"No user with id '{ownerUserId}'.");
                    }
                }

                int id = EventActionsDb.CreateEvent(db, date, hour, startMinute, endMinute, title, eventType, hostCount, hasGuest, ownerUserId);
                return id == -1
                    ? Results.BadRequest("Conflict or invalid input")
                    : Results.Ok(new { EventId = id });
            })
                .RequireAuthorization()
                .AddEndpointFilter<MustChangePasswordEnforcementFilter>();

            // POST /db/event/{eventId}/reschedule -> change a booking's time via EventActionsDb.RescheduleEvent (same overlap rules as CreateEvent)
            group.MapPost("/{eventId}/reschedule", async (
                int eventId,
                DateTime newDate,
                int newHour,
                int newStartMinute,
                int newEndMinute,
                HttpContext httpContext,
                UserManager<ApplicationUser> userManager,
                SchedulerContext db) =>
            {
                var entity = await db.Events.FindAsync(eventId);
                if (entity is null)
                {
                    return Results.NotFound($"No event with id {eventId}");
                }

                string? currentUserId = userManager.GetUserId(httpContext.User);
                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    return Results.Unauthorized();
                }

                if (!httpContext.User.IsInRole("Admin")
                    && !string.Equals(entity.ResponsibleUserId, currentUserId, StringComparison.Ordinal))
                {
                    // Why: Only the owning contributor (or an admin) may change an event that affects their pay + schedule.
                    return Results.StatusCode(StatusCodes.Status403Forbidden);
                }

                bool ok = EventActionsDb.RescheduleEvent(db, eventId, newDate, newHour, newStartMinute, newEndMinute);
                return ok ? Results.Ok() : Results.BadRequest("Reschedule failed (conflict or invalid id)");
            })
                .RequireAuthorization()
                .AddEndpointFilter<MustChangePasswordEnforcementFilter>();

            // POST /db/event/{eventId}/delete -> delete a booking by Id via EventActionsDb.DeleteEvent
            group.MapPost("/{eventId}/delete", async (
                int eventId,
                HttpContext httpContext,
                UserManager<ApplicationUser> userManager,
                SchedulerContext db) =>
            {
                var entity = await db.Events.FindAsync(eventId);
                if (entity is null)
                {
                    return Results.NotFound($"No event with id {eventId}");
                }

                string? currentUserId = userManager.GetUserId(httpContext.User);
                if (string.IsNullOrWhiteSpace(currentUserId))
                {
                    return Results.Unauthorized();
                }

                if (!httpContext.User.IsInRole("Admin")
                    && !string.Equals(entity.ResponsibleUserId, currentUserId, StringComparison.Ordinal))
                {
                    // Why: Deleting someone else's booking would corrupt schedule history and payment totals.
                    return Results.StatusCode(StatusCodes.Status403Forbidden);
                }

                bool ok = EventActionsDb.DeleteEvent(db, eventId);
                return ok ? Results.Ok() : Results.BadRequest("Delete failed (invalid id)");
            })
                .RequireAuthorization()
                .AddEndpointFilter<MustChangePasswordEnforcementFilter>();
        }

        private sealed class MustChangePasswordEnforcementFilter : IEndpointFilter
        {
            public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
            {
                HttpContext httpContext = context.HttpContext;

                if (HttpMethods.IsOptions(httpContext.Request.Method))
                {
                    return await next(context);
                }

                var userManager = httpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.GetUserAsync(httpContext.User);
                if (user is null)
                {
                    return Results.Unauthorized();
                }

                if (user.MustChangePassword)
                {
                    // Why: Bookings directly affect schedule + pay, so we block them until the user has set a personal password.
                    return Results.StatusCode(StatusCodes.Status403Forbidden);
                }

                return await next(context);
            }
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
