// A.1 [Dev.Lab] SimpleTestEndpoints (development-only learning "doors").
// What: A small set of GET endpoints used to practice how Minimal API routing works.
// Why: Lets you experiment safely without touching the real booking/schedule endpoints.
// Where:
// - Only mapped in development: see Program.cs -> `if (app.Environment.IsDevelopment()) app.MapSimpleEndpoints();`
// - Each app.MapGet(...) below is a "Door" the WebApplication (the "animal") can respond to.
//
// NOTE: The comments below are intentionally beginner-friendly exercises; we keep them as-is.

namespace Scheduler.Endpoints
{
    public static class SimpleTestEndpoints
    {
        public static void MapSimpleEndpoints(this WebApplication app)
        {
            // 🎯 EXERCISE 1: Start with these simple endpoints
            
            // Your first endpoint - just returns text
            app.MapGet("/hello", () => "Hello World!");
            
            // Your second endpoint - returns a number
            app.MapGet("/number", () => 42);
            
            // Your third endpoint - returns true/false
            app.MapGet("/isworking", () => true);
            
            // 📅 Schedule-related endpoints
            app.MapGet("/today", () => DateTime.Today.ToString("yyyy-MM-dd"));
            app.MapGet("/now", () => DateTime.Now.ToString("HH:mm:ss"));
            app.MapGet("/schedulefact", () => "A radio schedule has 24 hours in a day");
            
            // 🧮 Math and logic endpoints
            app.MapGet("/math", () => 5 + 3);
            app.MapGet("/random", () => new Random().Next(1, 11));
            
            // ✨ YOUR TURN: Add your custom endpoints here!
            // Example: app.MapGet("/favoriteshow", () => "Morning Radio Show");
            // Example: app.MapGet("/currenthour", () => DateTime.Now.Hour);
        }
    }
}