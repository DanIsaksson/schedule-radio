// --- FILE: ScheduleEndpoints.cs ---
//
// This file is part of our new, modular endpoint system.
//
// PURPOSE:
// To define all API endpoints that are purely for GETTING (reading) schedule information.
// We group them here to keep our Program.cs file clean and to make it obvious
// where to find the code for specific URLs.
//
// CONCEPT: STATIC CLASS & EXTENSION METHODS
// This is a 'static class'. That means we can't create an instance of it with 'new'.
// Instead, all its methods are called directly on the class itself.
//
// We are also defining an 'extension method', MapScheduleEndpoints. An extension method
// allows us to "add" new methods to existing types without modifying them. Here, we are
// adding a 'MapScheduleEndpoints' method to the 'WebApplication' type (our 'app' object).
//
using Scheduler.Models; // Use our new models
using ScheduleAction; // Keep using the actions from the original file

namespace Scheduler.Endpoints
{
    public static class ScheduleEndpoints
    {
        // This is our extension method.
        // 'this WebApplication app' tells the compiler that this method extends the WebApplication class.
        public static void MapScheduleEndpoints(this WebApplication app)
        {
            // --- Endpoint Group ---
            // We can group related endpoints under a common prefix. Here, all endpoints
            // inside this group will start with "/schedule".
            // This helps avoid repetition and organizes the routes logically.
            var scheduleGroup = app.MapGroup("/schedule");

            // --- ENDPOINT 1: GET /schedule ---
            // The simplest endpoint. It returns the entire 7-day schedule object.
            // This is useful for a client that wants to get all data at once.
            scheduleGroup.MapGet("/", (ScheduleData schedule) =>
            {
                // The 'schedule' parameter is automatically provided by ASP.NET Core's
                // Dependency Injection system. We registered it as a service in Program.cs.
                return Results.Ok(schedule);
            });

            // --- ENDPOINT 2: GET /schedule/today ---
            // A more specific endpoint to get only today's schedule.
            scheduleGroup.MapGet("/today", (ScheduleData schedule) =>
            {
                // We use LINQ's .FirstOrDefault() to find the day schedule that matches today's date.
                // LINQ (Language Integrated Query) is a powerful feature in C# for working with collections.
                DaySchedule? today = schedule.Days
                    .FirstOrDefault(d => d.Date.Date == DateTime.Today);

                // It's important to handle cases where data might not be found.
                // If 'today' is null, we return a standard HTTP 404 Not Found response.
                if (today is null)
                {
                    return Results.NotFound("No schedule found for today.");
                }

                // If we found the schedule, we return it with an HTTP 200 OK response.
                // Results.Ok() wraps our data in a standard success response.
                return Results.Ok(today);
            });

            // --- ENDPOINT 3: GET /schedule/7days ---
            // This endpoint demonstrates calling into our "business logic" layer (the Actions.cs file).
            // The endpoint itself doesn't contain any logic; it just calls a method from ScheduleQueries.
            // This is a good practice called "Separation of Concerns".
            scheduleGroup.MapGet("/7days", (ScheduleData schedule) =>
            {
                var sevenDaySchedule = ScheduleQueries.GetSevenDays(schedule);
                return Results.Ok(sevenDaySchedule);
            });
        }
    }
}