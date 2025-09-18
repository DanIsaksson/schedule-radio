using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;
using ScheduleStorage;
using ScheduleAction;

var builder = WebApplication.CreateBuilder(args);

// Register services BEFORE building the app
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

// Serve index.html + other static assets (wwwroot or project root) + cors exception
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

// In-memory seven-day rolling schedule
ScheduleData schedule = new ScheduleData();

// Book a slot demo method. This is only usable within here, not by a user.
ScheduleData.SetBooking(schedule, DateTime.Today, 10, 15, 30, true);

// GET /schedule returns JSON for the next 7 days
app.MapGet("/schedule", () => schedule);

// Serve index.html + other static assets from project root
app.UseDefaultFiles();
app.UseStaticFiles();

// New MapGet-functions for refactoring
app.MapGet("/schedule/today", () =>
{
    DaySchedule? today = schedule.Days
        // .FirstOrDefault: fills the whole DaySChedule object with its Date, Hours, list etc. if matching DaySchedule.
        .FirstOrDefault(d => d.Date.Date == DateTime.Today);
    // No result
    if (today is null) return Results.NotFound("No schedule for today");
    // Result
    return Results.Ok(today);
});

// app.MapGet("/schedule/7days", () => ? );
// app.MapGet("/schedule/event", () => ? );

// new MapPost-functions for refactoring
// app.MapPost("/schedule/event/{eventId}", (int eventId) => ? );

// app.MapPost("/schedule/event/{eventId}", (int eventId) => 
// Date.Add.Event studio1 = new Event();
// // int startingMinute = (doesn't exist but...) inputfromUser;
// int Minutes[boooked minutes variable] (and some kind of range function for the Minutes array to start fron startingMinute and continue from there and fill the array with true until inputfromUser is done, maybe a for-loop or something)
// )
// app.MapPost("schedule/event/{eventId}/reschedule", () => ? );
// app.MapPost("schedule/event/{eventId}/addhost", () => ? );
// app.MapPost("/schedule/event/post", () => ? );
// app.MapPost("schedule/event/{eventId}/removehost", () => ? );
// app.MapPost("schedule/event/{eventId}/addguest", () => ? );
// app.MapPost("schedule/event/{eventId}/removeguest", () => ? );
// app.MapPost("schedule/event/{eventId}/delete", () => ? );

app.MapPost("/schedule/book", (
    DateTime date,
    int hour,
    int startMinute,
    int endMinute,
    bool isBooked) =>
{
    bool success = ScheduleAction.SetBooking(schedule, date, hour, startMinute, endMinute, isBooked);
    return success ? Results.Ok(schedule) : Results.BadRequest("Date is outside 7-day window");
});

app.Run();