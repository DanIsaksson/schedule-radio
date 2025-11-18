// --- FILE: Program.cs ---
// What I do here (beginner view)
// - I configure the web API: services (DbContext, CORS, Swagger), static files, and map endpoints.
// - Read this file top→bottom to see how requests get from the browser to my code.
// How to read
// - Each section says what it does and points to the file where the logic lives (tiny refs).
// - There are two paths: legacy in-memory (/schedule/*) for experiments and DB-backed (/db/*) used by the frontend UI.
using API.Models; // ScheduleData
using API.Actions; // Booking
using API.Endpoints; // MapScheduleEndpoints, MapEventEndpoints, MapEventDbEndpoints
using Microsoft.EntityFrameworkCore; // AddDbContext, UseSqlite
using API.Data; // SchedulerContext
using Scheduler.Endpoints; // MapSimpleEndpoints

var builder = WebApplication.CreateBuilder(args);

// --- 2) REGISTER SERVICES (Dependency Injection) ---
// I add the building blocks my app needs (DbContext, CORS, Swagger). The framework injects them later.

// CORS lets my frontend (vite dev server) call this API in the browser. Dev: permissive policy is fine.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Database (EF Core): I register my SQLite DbContext. See Data/SchedulerContext.cs for DbSets.
builder.Services.AddDbContext<SchedulerContext>(options =>
{
    // UseSqlite tells EF Core to use a lightweight file database. The file will be created if it doesn't exist.
    options.UseSqlite(builder.Configuration.GetConnectionString("Scheduler"));
});

// Legacy in-memory schedule (singleton). Models/ScheduleModels.cs describes this type.
// Consumer UI uses DB-backed endpoints; this stays for comparison and simple demos.
builder.Services.AddSingleton<ScheduleData>();
// Swagger/OpenAPI: generate metadata + a UI to explore and test endpoints.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 3) BUILD THE APP ---
// Create the pipeline object. If service wiring is broken, Build() would fail.
var app = builder.Build();

// Ensure DB exists (create tables on first run).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchedulerContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

// --- 4) MIDDLEWARE PIPELINE ---
// Order matters: static files, CORS, then endpoints.

// Default files: serve index.html automatically from wwwroot (API/wwwroot).
app.UseDefaultFiles();

// Static files: host assets (HTML/CSS/JS) from API/wwwroot.
app.UseStaticFiles();

// Apply CORS policy so the frontend can call the API in the browser.
app.UseCors();


// --- 5) MAP ENDPOINTS ---
// I keep endpoints in separate files:
// - Endpoints/ScheduleDbEndpoints.cs => read-only DB schedule (/db/schedule/*)
// - Endpoints/EventDbEndpoints.cs => DB writes for bookings (/db/event/*)
// - Endpoints/ScheduleEndpoints.cs & EventEndpoints.cs => legacy in-memory endpoints for comparison/experiments
app.MapScheduleEndpoints();
app.MapEventEndpoints();
app.MapEventDbEndpoints();
app.MapScheduleDbEndpoints(); // adds /db/schedule endpoints (ScheduleDbEndpoints.cs)

// Map simple test endpoints only in Development
if (app.Environment.IsDevelopment())
{
    app.MapSimpleEndpoints();
}

// Legacy demo endpoint (simple booking against in-memory schedule). Use DB endpoints in real scenarios.
app.MapPost("/schedule/book", (DateTime date, int hour, int startMinute, int endMinute, bool isBooked, ScheduleData schedule) =>
{
    bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, isBooked);
    return success ? Results.Ok() : Results.BadRequest("Booking failed (conflict or invalid parameters)");
});


// --- 6) RUN ---
// Start the server and listen for requests.
app.Run();