using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Schedule;

var builder = WebApplication.CreateBuilder(args);

// Register services BEFORE building the app
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

// In-memory seven-day rolling schedule
ScheduleData schedule = new ScheduleData();

// Demonstration booking (today 10:15–10:30) – lets you immediately see a booked slot
ScheduleAction.SetBooking(schedule, DateTime.Today, 10, 15, 30, true);

// GET /schedule returns JSON for the next 7 days
app.MapGet("/schedule", () => schedule);

// Redirect root to schedule JSON so hitting base URL works
app.MapGet("/", () => Results.Redirect("/schedule"));

// Simple HTML form to create a booking
app.MapGet("/schedule/book", (DateTime? date, int? hour) =>
{
     // default to today and next hour if not provided
     var chosenDate = date ?? DateTime.Today;
     var chosenHour = hour ?? DateTime.Now.Hour;
     var html = $"""
    <html><head><title>Book slot</title></head><body>
    <h2>Book {chosenDate:yyyy-MM-dd} {chosenHour:00}:00</h2>
    <form method="post" action="/schedule/book">

        <input type="hidden" name="date" value="{chosenDate:yyyy-MM-dd}" />
        <input type="hidden" name="hour" value="{chosenHour}" />
        Start minute: <input type="number" name="startMinute" min="0" max="59" required /><br/>
        End minute:   <input type="number" name="endMinute"   min="1" max="60" required /><br/>
        <label><input type="checkbox" name="isBooked" value="true" checked /> Book (uncheck to clear)</label><br/>
        <button type="submit">Save</button>
    </form>
    <p><a href="/schedule">Back to Schedule JSON</a></p>
    </body></html>
    """;
     return Results.Content(html, "text/html");
});

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