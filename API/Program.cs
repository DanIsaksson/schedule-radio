// [Startup.Backend.1] Main Switch
// This file is the entry point for the Radio Scheduler backend.
// It wires up the web server, database, and API endpoints.
//
// -> See Lesson: Interactive-Lesson/01-Program-Startup-and-Pipeline.md
//
// Reading order:
// 1. Create Builder (The Order Form)
// 2. Register Services (The Ingredients)
// 3. Build App (The Assembly)
// 4. Configure Pipeline (The Conveyor Belt)
// 5. Map Endpoints (The Doors)

using API.Models; // ScheduleData
using API.Actions; // Booking
using API.Endpoints; // MapScheduleEndpoints, MapEventEndpoints, MapEventDbEndpoints
using Microsoft.EntityFrameworkCore; // AddDbContext, UseSqlite
using API.Data; // SchedulerContext
using Scheduler.Endpoints; // MapSimpleEndpoints

var builder = WebApplication.CreateBuilder(args);

// [Startup.Backend.2] Register Services (Dependency Injection)
// This section configures the "ingredients" our app needs:
// - Database Context (SQLite)
// - CORS Policy (Security)
// - Swagger (Documentation)
// - Legacy Services (In-Memory Data)

// [Startup.Backend.3] CORS Policy ("The Bouncer")
// Allows the React frontend (localhost:5173) to talk to this API (localhost:5219).
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// [Startup.Backend.4] Database Context
// Configures Entity Framework Core to use SQLite.
// The connection string "Scheduler" is defined in appsettings.json.
builder.Services.AddDbContext<SchedulerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Scheduler"));
});

// [Startup.Backend.5] Legacy & Utility Services
// - ScheduleData: Old in-memory storage (kept for experiments).
// - Swagger: Generates the API testing UI.
builder.Services.AddSingleton<ScheduleData>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [Startup.Backend.6] Build the App
// "Seals" the configuration and creates the runnable web application.
var app = builder.Build();

// [Startup.Backend.7] Database Initialization
// Ensures the SQLite file (scheduler.db) exists and has the correct tables.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchedulerContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

// [Startup.Backend.8] Middleware Pipeline ("The Conveyor Belt")
// Every request goes through these steps in order:
// 1. Default Files (index.html)
// 2. Static Files (JS/CSS)
// 3. CORS (Security Check)
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

// [Startup.Backend.9] Map Endpoints ("The Doors")
// Connects specific URLs (e.g., /db/schedule) to code handlers.
// We use "Extension Methods" (Map...Endpoints) to keep this file clean.

// Modern DB-backed Endpoints
app.MapScheduleDbEndpoints(); // Read schedule
app.MapEventDbEndpoints();    // Create bookings

// Legacy In-Memory Endpoints
app.MapScheduleEndpoints();
app.MapEventEndpoints();

// Development-only Endpoints
if (app.Environment.IsDevelopment())
{
    app.MapSimpleEndpoints();
}

// [Startup.Backend.10] Legacy Demo Endpoint
// A direct example of mapping a POST request to a handler function.
// Kept here to show how MapPost works without hiding it in another file.
app.MapPost("/schedule/book", (DateTime date, int hour, int startMinute, int endMinute, bool isBooked, ScheduleData schedule) =>
{
    bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, isBooked);
    return success ? Results.Ok() : Results.BadRequest("Booking failed (conflict or invalid parameters)");
});

// [Startup.Backend.11] Run
// Starts the server and listens for requests.
app.Run();