// --- FILE: Program.cs ---
//
// This is the main entry point for our entire web API.
// --- 1. SETUP THE BUILDER ---
// Think of WebApplicationBuilder as the scratchpad where you can mix in dependencies before
// "baking" the final app. If you want to trial alternative services (e.g., a mock database),
// swap things here and see how the rest of the pipeline responds.
// USINGs: kept at top so the compiler knows where types like ScheduleData, SchedulerContext, and
// extension methods (MapScheduleEndpoints, etc.) come from. Try commenting one out to observe the
// compiler errors and discover which namespaces power which features.
using API.Models; // ScheduleData
using API.Actions; // Booking
using API.Endpoints; // MapScheduleEndpoints, MapEventEndpoints, MapEventDbEndpoints
using Microsoft.EntityFrameworkCore; // AddDbContext, UseSqlite
using API.Data; // SchedulerContext
using Scheduler.Endpoints; // MapSimpleEndpoints

var builder = WebApplication.CreateBuilder(args);

// --- 2. REGISTER SERVICES (Dependency Injection) ---
// Services are components that provide functionality to our app. Experiment by adjusting their
// lifetimes (Singleton vs Scoped) to feel how state sharing changes system behavior.

// AddCors: Configures Cross-Origin Resource Sharing. This is a security feature
// that controls which other domains are allowed to make requests to our API.
// Here, we are being very permissive for development purposes. Try tightening the
// rules (AllowAnyOrigin -> WithOrigins) and observe how browsers react to blocked calls.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// --- DATABASE: REGISTER EF CORE CONTEXT ---
// This connects EF Core to SQLite using the "Scheduler" connection string (see appsettings.json).
// Swap UseSqlite for UseInMemoryDatabase or another provider to prototype without touching the file DB.
builder.Services.AddDbContext<SchedulerContext>(options =>
{
    // UseSqlite tells EF Core to use a lightweight file database. The file will be created if it doesn't exist.
    options.UseSqlite(builder.Configuration.GetConnectionString("Scheduler"));
});

// --- IMPORTANT: REGISTERING OUR SCHEDULE AS A SINGLETON ---
// We are telling the Dependency Injection container to create ONE SINGLE INSTANCE
// of ScheduleData and share that same instance with any part of the application
// that asks for it. This is called a 'Singleton' lifetime.
// Toggle this to AddScoped/AddTransient and rerun requests to witness how conflicting schedules appear
// when state is not shared. It's a safe way to feel the impact of lifetimes firsthand.
builder.Services.AddSingleton<ScheduleData>();

// Swagger addition
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 3. BUILD THE APPLICATION ---
// This creates the 'app' object, which we use to define our request pipeline.
// If configuration above breaks, Build() will throw—so this line confirms your service graph is valid.
var app = builder.Build();

// --- Ensure database exists (create tables if missing) ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchedulerContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

// --- 4. CONFIGURE THE HTTP REQUEST PIPELINE ---
// The pipeline is a series of middleware components that process an incoming
// HTTP request. The order here is important. Shuffle middleware and rerun requests to
// see how static file serving or CORS breaks when placed after endpoints.

// UseDefaultFiles: Allows the server to look for default files like 'index.html'
// when a request comes in for a directory. Temporarily remove this to see the raw directory listing behavior.
app.UseDefaultFiles();

// UseStaticFiles: Allows the server to serve static files (HTML, CSS, JS, images)
// from the project's root folder. Point StaticFiles to a subfolder to practice hosting different assets.
app.UseStaticFiles();

// UseCors: Applies the CORS policy we defined earlier to all incoming requests.
// Comment this out and verify how browsers suddenly reject cross-origin calls.
app.UseCors();


// --- 5. MAP ENDPOINTS (The New, Modular Way) ---
// Instead of having all the MapGet/MapPost calls here, we now just call the
// extension methods we defined in our Endpoint files. This keeps Program.cs
// incredibly clean and organized. Jump into each extension to practice adding new routes without
// cluttering startup code.
app.MapScheduleEndpoints();
app.MapEventEndpoints();
app.MapEventDbEndpoints();
app.MapScheduleDbEndpoints(); // adds /db/schedule endpoints (ScheduleDbEndpoints.cs)

// Map simple test endpoints only in Development
if (app.Environment.IsDevelopment())
{
    app.MapSimpleEndpoints();
}

// --- LEGACY ENDPOINT ---
// This is the original, simple booking endpoint. We can keep it for now
// or refactor it into its own file later.
app.MapPost("/schedule/book", (DateTime date, int hour, int startMinute, int endMinute, bool isBooked, ScheduleData schedule) =>
{
    bool success = Booking.SendBooking(schedule, date, hour, startMinute, endMinute, isBooked);
    return success ? Results.Ok() : Results.BadRequest("Booking failed (conflict or invalid parameters)");
});


// --- 6. RUN THE APPLICATION ---
// This starts the web server and makes it listen for incoming HTTP requests.
// It will run forever until the process is stopped.
app.Run();