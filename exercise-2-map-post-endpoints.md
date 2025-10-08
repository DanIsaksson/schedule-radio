# 🎯 Exercise 2: Mastering MapPost - Sending Data to Your API

## 📝 Learning Goals
- Understand how MapPost differs from MapGet
- Learn to send data TO your server
- Practice with the three core endpoints from your reset-mental-step-forward.md
- Build confidence through step-by-step testing

---

## 🔄 Part 1: Understanding MapPost vs MapGet

**MapGet** = Reading data (like viewing a webpage)
**MapPost** = Sending data (like submitting a form)

### The Key Difference:
```csharp
// MapGet - just shows data
app.MapGet("/hello", () => "Hello");

// MapPost - receives data, then does something
app.MapPost("/greet", (string name) => $"Hello {name}!");
```

---

## 🧪 Part 2: Create Simple MapPost Practice Endpoints

Create a new file called `PostPracticeEndpoints.cs`:

```csharp
// PostPracticeEndpoints.cs
// Practice sending data TO the server

namespace PracticeEndpoints
{
    public static class PostPracticeEndpoints
    {
        public static void MapPostPractice(this WebApplication app)
        {
            // Practice 1: Send a name, get a greeting
            app.MapPost("/greet", (string name) => $"Hello {name}!");
            
            // Practice 2: Send two numbers, get the sum
            app.MapPost("/add", (int num1, int num2) => num1 + num2);
            
            // Practice 3: Send a message, get it back in uppercase
            app.MapPost("/shout", (string message) => message.ToUpper());
        }
    }
}
```

Add to your Program.cs:
```csharp
app.MapPostPractice(); // Add this line
```

---

## 🧪 Part 3: How to Test MapPost Endpoints

**Method 1: Using a Simple HTML Form**
Create `test-post.html` in your API folder:

```html
<!DOCTYPE html>
<html>
<head>
    <title>Test POST Endpoints</title>
</head>
<body>
    <h2>Test Greeting</h2>
    <form action="/greet" method="POST">
        <input type="text" name="name" placeholder="Your name">
        <button type="submit">Greet Me!</button>
    </form>
    
    <h2>Test Addition</h2>
    <form action="/add" method="POST">
        <input type="number" name="num1" placeholder="First number">
        <input type="number" name="num2" placeholder="Second number">
        <button type="submit">Add Them!</button>
    </form>
</body>
</html>
```

**Method 2: Using Browser Console**
Open browser console (F12) and try:
```javascript
// Test the greet endpoint
fetch('/greet', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({name: 'Alice'})
}).then(r => r.text()).then(console.log);
```

---

## 🎯 Part 4: Recreate Your Three Core Endpoints

Now let's build the exact endpoints from your reset-mental-step-forward.md:

### Endpoint 1: GET /schedule/today (Simple View)
```csharp
// This goes in your main endpoint file
app.MapGet("/schedule/today", (ScheduleData schedule) =>
{
    var today = schedule.Days.FirstOrDefault(d => d.Date.Date == DateTime.Today);
    return today != null ? Results.Ok(today) : Results.NotFound("No schedule today");
});
```

### Endpoint 2: POST /schedule/event/post (Create Booking)
```csharp
app.MapPost("/schedule/event/post", (DateTime date, int hour, int startMinute, int endMinute, ScheduleData schedule) =>
{
    // Simple validation
    if (startMinute >= endMinute) return Results.BadRequest("Start must be before end");
    if (hour < 0 || hour > 23) return Results.BadRequest("Hour must be 0-23");
    
    // Try to book (simplified version)
    var day = schedule.Days.FirstOrDefault(d => d.Date.Date == date.Date);
    if (day == null) return Results.BadRequest("Date not in schedule");
    
    var hourSlot = day.Hours.FirstOrDefault(h => h.Hour == hour);
    if (hourSlot == null) return Results.BadRequest("Invalid hour");
    
    // Check if time is available (simplified)
    for (int m = startMinute; m < endMinute; m++)
    {
        if (hourSlot.Minutes[m]) return Results.BadRequest("Time slot already booked");
    }
    
    // Book the time
    for (int m = startMinute; m < endMinute; m++)
    {
        hourSlot.Minutes[m] = true;
    }
    
    return Results.Ok(new { Message = "Booked successfully!", EventId = new Random().Next(1000, 9999) });
});
```

### Endpoint 3: POST /schedule/event/{eventId}/reschedule (Move Booking)
```csharp
app.MapPost("/schedule/event/{eventId}/reschedule", 
    (int eventId, DateTime newDate, int newHour, int newStartMinute, int newEndMinute, ScheduleData schedule) =>
{
    // Simplified reschedule logic
    if (newStartMinute >= newEndMinute) return Results.BadRequest("Invalid time range");
    
    // In a real app, you'd find the existing booking first
    // For practice, we'll just validate the new time
    var day = schedule.Days.FirstOrDefault(d => d.Date.Date == newDate.Date);
    if (day == null) return Results.BadRequest("New date not available");
    
    var hourSlot = day.Hours.FirstOrDefault(h => h.Hour == newHour);
    if (hourSlot == null) return Results.BadRequest("Invalid hour");
    
    // Check if new time is available
    for (int m = newStartMinute; m < newEndMinute; m++)
    {
        if (hourSlot.Minutes[m]) return Results.BadRequest("New time slot already booked");
    }
    
    return Results.Ok(new { Message = "Rescheduled successfully!", OldEventId = eventId });
});
```

---

## 🧪 Part 5: Testing Your Three Endpoints

### Test 1: Check Today's Schedule
**Browser:** `http://localhost:5219/schedule/today`
**Expected:** JSON showing today's 24 hours

### Test 2: Book a Time Slot
**Using JavaScript Console:**
```javascript
// Book a 30-minute show at 2 PM today
fetch('/schedule/event/post', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
        date: new Date().toISOString().split('T')[0], // Today's date
        hour: 14, // 2 PM
        startMinute: 0, // Start at :00
        endMinute: 30 // End at :30
    })
}).then(r => r.json()).then(data => {
    console.log('Booking result:', data);
    if (data.eventId) {
        console.log('Remember this Event ID:', data.eventId);
    }
});
```

### Test 3: Reschedule the Booking
**Using JavaScript Console:** (Use the Event ID from Test 2)
```javascript
// Move the booking to 3 PM
fetch('/schedule/event/YOUR_EVENT_ID/reschedule', {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({
        newDate: new Date().toISOString().split('T')[0], // Same day
        newHour: 15, // 3 PM
        newStartMinute: 0, // Start at :00
        newEndMinute: 30 // End at :30
    })
}).then(r => r.json()).then(data => {
    console.log('Reschedule result:', data);
});
```

---

## 🎯 Part 6: Experiment and Learn

### Try These Modifications:

1. **Change the validation messages:**
```csharp
return Results.BadRequest("Oops! That time is already taken. Try another slot.");
```

2. **Add more validation:**
```csharp
if (startMinute < 0 || startMinute > 59) return Results.BadRequest("Minutes must be 0-59");
if (endMinute < 1 || endMinute > 60) return Results.BadRequest("End minute must be 1-60");
```

3. **Return more detailed responses:**
```csharp
return Results.Ok(new { 
    Message = "Booked successfully!", 
    EventId = eventId,
    TimeSlot = $"{hour}:00 to {hour}:30",
    Duration = "30 minutes"
});
```

---

## 🧪 Part 7: Create Your Own Practice Scenarios

### Scenario 1: Check Multiple Days
Create an endpoint that checks if a time slot is free across multiple days:
```csharp
app.MapPost("/check-week", (int hour, int startMinute, int endMinute, ScheduleData schedule) =>
{
    var availableDays = new List<string>();
    foreach (var day in schedule.Days)
    {
        var hourSlot = day.Hours.FirstOrDefault(h => h.Hour == hour);
        if (hourSlot != null)
        {
            bool available = true;
            for (int m = startMinute; m < endMinute; m++)
            {
                if (hourSlot.Minutes[m]) { available = false; break; }
            }
            if (available) availableDays.Add(day.Date.ToString("ddd"));
        }
    }
    return Results.Ok(new { AvailableDays = availableDays });
});
```

### Scenario 2: Quick Book (Simplified)
Create a 15-minute booking endpoint:
```csharp
app.MapPost("/quick-book", (DateTime date, int hour, ScheduleData schedule) =>
{
    // Book 15 minutes starting at :00
    return Results.Ok(new { 
        Message = "Quick booked 15 minutes!",
        Time = $"{hour}:00 to {hour}:15"
    });
});
```

---

## ✅ What You Learned

1. **MapPost receives data:** Parameters in the method signature become the data you send
2. **Validation is important:** Always check if the data makes sense
3. **Return meaningful responses:** Tell the user what happened
4. **Testing takes practice:** Use browser console, forms, or tools
5. **Start simple, then add features:** Get basic working first, then improve

---

## 🚀 Next Steps

Now you understand both MapGet and MapPost! You can:
- Read data with MapGet endpoints
- Send data with MapPost endpoints  
- Test your endpoints multiple ways
- Build on the radio schedule foundation

**Practice Challenge:** Create an endpoint that books a show and immediately returns "Booked for [show name] at [time] on [date]" using the data you send it!