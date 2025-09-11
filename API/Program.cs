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

// Book a slot demo method. This is only usable within here, not by a user.
ScheduleAction.SetBooking(schedule, DateTime.Today, 10, 15, 30, true);

// GET /schedule returns JSON for the next 7 days
app.MapGet("/schedule", () => schedule);

// Redirect root to schedule JSON so hitting base URL works
app.MapGet("/", () => Results.Redirect("/schedule"));


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