// --- FILE: Program.cs ---
//
// This is the main entry point for our entire web API.
//
// PURPOSE:
// To configure and run the web server. This file is responsible for setting up
// all the services the application needs (like CORS, Swagger for documentation, etc.)
// and defining the request processing pipeline (like serving static files).
//
// CONCEPT: MINIMAL APIS & DEPENDENCY INJECTION
// We are using ASP.NET Core's "Minimal API" framework. It's designed to be
// lightweight and easy to start with. Instead of complex controller classes, we
// define endpoints directly on the 'WebApplication' object ('app').
//
// A core concept here is 'Dependency Injection' (DI). Instead of creating objects
// manually wherever we need them, we register them as 'services' with a central
// container (builder.Services). Then, ASP.NET Core automatically provides these
// services to our endpoints when they are requested as parameters.
//
using Scheduler.Models; // Use our new models
using ScheduleAction; // Keep using the actions from the original file
using Scheduler.Endpoints; // Import our new endpoint definitions

// --- 1. SETUP THE BUILDER ---
// The WebApplicationBuilder is where we configure the services our app will use.
var builder = WebApplication.CreateBuilder(args);

// --- 2. REGISTER SERVICES (Dependency Injection) ---
// Services are components that provide functionality to our app.

// AddCors: Configures Cross-Origin Resource Sharing. This is a security feature
// that controls which other domains are allowed to make requests to our API.
// Here, we are being very permissive for development purposes.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// --- IMPORTANT: REGISTERING OUR SCHEDULE AS A SINGLETON ---
// We are telling the Dependency Injection container to create ONE SINGLE INSTANCE
// of ScheduleData and share that same instance with any part of the application
// that asks for it. This is called a 'Singleton' lifetime.
// This is CRITICAL because it ensures all our endpoints are working with the
// exact same in-memory schedule data.
builder.Services.AddSingleton<ScheduleData>();


// --- 3. BUILD THE APPLICATION ---
// This creates the 'app' object, which we use to define our request pipeline.
var app = builder.Build();

// --- 4. CONFIGURE THE HTTP REQUEST PIPELINE ---
// The pipeline is a series of middleware components that process an incoming
// HTTP request. The order here is important.

// UseDefaultFiles: Allows the server to look for default files like 'index.html'
// when a request comes in for a directory.
app.UseDefaultFiles();

// UseStaticFiles: Allows the server to serve static files (HTML, CSS, JS, images)
// from the project's root folder.
app.UseStaticFiles();

// UseCors: Applies the CORS policy we defined earlier to all incoming requests.
app.UseCors();


// --- 5. MAP ENDPOINTS (The New, Modular Way) ---
// Instead of having all the MapGet/MapPost calls here, we now just call the
// extension methods we defined in our Endpoint files. This keeps Program.cs
// incredibly clean and organized.
app.MapScheduleEndpoints();
app.MapEventEndpoints();

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