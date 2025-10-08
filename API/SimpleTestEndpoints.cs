// SimpleTestEndpoints.cs
// Your playground for learning endpoints - start here!

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