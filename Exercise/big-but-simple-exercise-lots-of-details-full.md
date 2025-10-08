# 🎯 Complete Exercise: Understanding Endpoints and Object Manipulation
## Building a Radio Station Booking System from the Ground Up

---

## 📖 **INTRODUCTION**

Welcome to your comprehensive journey into understanding how web applications work! In this exercise, you'll build upon an existing radio station booking system to learn how frontend JavaScript communicates with backend C#, how objects are manipulated, and how APIs are designed.

**What You'll Build**: A complete booking system where users can reserve time slots for radio shows, view existing bookings, and manage the schedule.

**What You'll Learn**: The fundamental concepts that power modern web applications.

---

## 🎯 **YOUR MISSION**

You are a junior developer at **Radio Wave FM**, a local radio station. The station manager has asked you to improve their existing booking system by adding new features and fixing some issues. Your job is to understand how the current system works and then extend it with new functionality.

---

## 📋 **PHASE 1: FOUNDATION BUILDING**
*Time: 30 minutes*

### 🖊️ **Pen & Paper Exercise 1: Understanding the Data Model**

Before touching any code, let's understand what we're working with. Get a piece of paper and draw the following:

#### **Step 1.1: Draw the Object Hierarchy (10 minutes)**

Look at the <mcfile name="ScheduleData.cs" path="f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API\Models\ScheduleData.cs"></mcfile> file and draw a diagram showing:

```
📝 DRAWING EXERCISE:
┌─────────────────────────────────────────────────────────────┐
│ Draw boxes for each class and connect them with arrows      │
│                                                             │
│ ScheduleData                                                │
│ ├── What properties does it have?                           │
│ ├── What type is each property?                             │
│ └── How many days does it store?                            │
│                                                             │
│ DaySchedule                                                 │
│ ├── What does it represent?                                 │
│ ├── What properties does it have?                           │
│ └── How many hours does it contain?                         │
│                                                             │
│ HourSchedule                                                │
│ ├── What does it represent?                                 │
│ ├── How are minutes stored?                                 │
│ └── What does true/false mean in the Minutes array?         │
└─────────────────────────────────────────────────────────────┘
```

**🤔 Think About This**: Why do you think the system uses a boolean array for minutes instead of storing start/end times?

#### **Step 1.2: Trace a Booking Process (10 minutes)**

Now draw the flow of what happens when someone books a 30-minute show:

```
📝 DRAWING EXERCISE:
┌─────────────────────────────────────────────────────────────┐
│ Draw the step-by-step process:                              │
│                                                             │
│ 1. User wants to book: Today, 2:15 PM - 2:45 PM            │
│                                                             │
│ 2. Which Day object? _______________                        │
│                                                             │
│ 3. Which Hour object? _______________                       │
│                                                             │
│ 4. Which Minutes need to be set to true? _______________    │
│                                                             │
│ 5. Draw the Minutes array before and after booking:        │
│    Before: [false, false, false, ...]                      │
│    After:  [?, ?, ?, ...]                                  │
└─────────────────────────────────────────────────────────────┘
```

#### **Step 1.3: HTTP Communication Flow (10 minutes)**

Draw how the browser talks to the server:

```
📝 DRAWING EXERCISE:
┌─────────────────────────────────────────────────────────────┐
│ Draw the communication between Browser and Server:          │
│                                                             │
│ Browser (JavaScript)  ←→  Server (C#)                      │
│                                                             │
│ 1. What type of request is sent to GET data? ___________    │
│                                                             │
│ 2. What type of request is sent to CREATE a booking? ____  │
│                                                             │
│ 3. Where does the data travel in each request type?        │
│    GET: _______________________________________________     │
│    POST: ______________________________________________     │
│                                                             │
│ 4. What format is the data in? _______________________     │
└─────────────────────────────────────────────────────────────┘
```

---

## 🧪 **PHASE 2: HANDS-ON EXPLORATION**
*Time: 45 minutes*

### **Step 2.1: Test the Existing System (15 minutes)**

1. **Start the Application**:
   ```bash
   cd f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API
   dotnet run
   ```

2. **Open the Test Page**: Navigate to `http://localhost:5219/test-endpoints.html`

3. **Test Each Endpoint** and record your results:

#### **GET Endpoints Testing**
```
📝 TESTING LOG:
┌─────────────────────────────────────────────────────────────┐
│ Test each GET endpoint and record what happens:             │
│                                                             │
│ GET /hello                                                  │
│ Result: _______________________________________________     │
│                                                             │
│ GET /number                                                 │
│ Result: _______________________________________________     │
│                                                             │
│ GET /today                                                  │
│ Result: _______________________________________________     │
│                                                             │
│ GET /now                                                    │
│ Result: _______________________________________________     │
│                                                             │
│ GET /schedule/today                                         │
│ Result: _______________________________________________     │
│                                                             │
│ GET /schedule/event/                                        │
│ Result: _______________________________________________     │
└─────────────────────────────────────────────────────────────┘
```

#### **POST Endpoints Testing**
```
📝 TESTING LOG:
┌─────────────────────────────────────────────────────────────┐
│ Test POST endpoints with different inputs:                  │
│                                                             │
│ POST /greet with name "Alex"                                │
│ Result: _______________________________________________     │
│                                                             │
│ POST /add with numbers 5 and 3                             │
│ Result: _______________________________________________     │
│                                                             │
│ POST /schedule/event/post with:                             │
│ Hour: 14, Start: 15, End: 45                               │
│ Result: _______________________________________________     │
└─────────────────────────────────────────────────────────────┘
```

### **Step 2.2: Understand the Code Behind the Magic (15 minutes)**

Open the <mcfile name="test-endpoints.html" path="f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API\wwwroot\test-endpoints.html"></mcfile> file and examine the JavaScript functions:

#### **Code Analysis Exercise**
```javascript
// Look at this function and answer the questions:
async function testGet(endpoint) {
    try {
        const response = await fetch(endpoint);
        const data = await response.text();
        showResult(endpoint, data, response.ok);
    } catch (error) {
        showResult(endpoint, 'Error: ' + error.message, false);
    }
}
```

**🤔 Questions to Answer**:
1. What does `async` mean? ________________________________
2. What does `await` do? __________________________________
3. What is `fetch()`? ____________________________________
4. What happens if there's an error? ______________________

#### **Compare GET vs POST Functions**
```javascript
// GET function (above)
const response = await fetch(endpoint);

// POST function
const response = await fetch(endpoint, {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(data)
});
```

**🤔 Questions to Answer**:
1. What's different about the POST request? ________________
2. What is `JSON.stringify()` doing? ______________________
3. Why do we need the `Content-Type` header? ______________

### **Step 2.3: Examine the Backend Endpoints (15 minutes)**

Look at the <mcfile name="EventEndpoints.cs" path="f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API\Endpoints\EventEndpoints.cs"></mcfile> file:

#### **Endpoint Analysis Exercise**
```csharp
// Examine this endpoint:
eventGroup.MapPost("/post", (DateTime date, int hour, int startMinute, int endMinute, ScheduleData schedule) =>
{
    int id = EventActions.CreateEvent(schedule, date, hour, startMinute, endMinute);
    return id == -1
        ? Results.BadRequest("Booking conflict or invalid parameters")
        : Results.Ok(new { EventId = id });
});
```

**🤔 Questions to Answer**:
1. What HTTP method does this handle? _____________________
2. What parameters does it expect? ________________________
3. What does it return on success? ________________________
4. What does it return on failure? ________________________
5. Where does `ScheduleData schedule` come from? ___________

---

## 🛠️ **PHASE 3: DEEP DIVE IMPLEMENTATION**
*Time: 60 minutes*

### 🖊️ **Pen & Paper Exercise 2: Planning New Features**

Before coding, let's plan what we want to build. The radio station wants these new features:

1. **Get Available Time Slots**: Show which time slots are free
2. **Get Bookings for a Specific Day**: List all bookings for a chosen day
3. **Cancel a Booking**: Remove an existing booking

#### **Step 3.1: Design the New Endpoints (20 minutes)**

For each new feature, design the endpoint:

```
📝 ENDPOINT DESIGN:
┌─────────────────────────────────────────────────────────────┐
│ Feature 1: Get Available Time Slots                        │
│                                                             │
│ HTTP Method: ___________                                    │
│ URL Pattern: ___________________________________________    │
│ Parameters needed: ____________________________________     │
│ What should it return? ________________________________     │
│                                                             │
│ Feature 2: Get Bookings for a Specific Day                 │
│                                                             │
│ HTTP Method: ___________                                    │
│ URL Pattern: ___________________________________________    │
│ Parameters needed: ____________________________________     │
│ What should it return? ________________________________     │
│                                                             │
│ Feature 3: Cancel a Booking                                │
│                                                             │
│ HTTP Method: ___________                                    │
│ URL Pattern: ___________________________________________    │
│ Parameters needed: ____________________________________     │
│ What should it return? ________________________________     │
└─────────────────────────────────────────────────────────────┘
```

**🤔 Design Questions**:
1. Should "Get Available Time Slots" be GET or POST? Why?
2. How should we specify which day we want bookings for?
3. What information do we need to cancel a booking?

### **Step 3.2: Implement the Backend Endpoints (20 minutes)**

Now let's implement these endpoints. Add the following to your <mcfile name="EventEndpoints.cs" path="f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API\Endpoints\EventEndpoints.cs"></mcfile> file:

#### **Implementation Task 1: Get Available Time Slots**

Add this endpoint after the existing ones:

```csharp
// --- NEW ENDPOINT: GET /schedule/available/{dayOffset} ---
// Gets all available (free) time slots for a specific day
// dayOffset: 0 = today, 1 = tomorrow, etc.
eventGroup.MapGet("/available/{dayOffset}", (int dayOffset, ScheduleData schedule) =>
{
    // Validate day offset
    if (dayOffset < 0 || dayOffset >= 7)
    {
        return Results.BadRequest("Day offset must be between 0 and 6");
    }

    var day = schedule.Days[dayOffset];
    var availableSlots = new List<object>();

    // Check each hour
    for (int hour = 0; hour < 24; hour++)
    {
        var hourSchedule = day.Hours[hour];
        
        // Find continuous free time slots
        int startMinute = -1;
        for (int minute = 0; minute < 60; minute++)
        {
            if (!hourSchedule.Minutes[minute]) // Free minute
            {
                if (startMinute == -1)
                    startMinute = minute; // Start of free period
            }
            else // Booked minute
            {
                if (startMinute != -1)
                {
                    // End of free period, add to available slots
                    availableSlots.Add(new
                    {
                        Date = day.Date.ToString("yyyy-MM-dd"),
                        Hour = hour,
                        StartMinute = startMinute,
                        EndMinute = minute - 1,
                        Duration = minute - startMinute
                    });
                    startMinute = -1;
                }
            }
        }
        
        // Handle free period that goes to end of hour
        if (startMinute != -1)
        {
            availableSlots.Add(new
            {
                Date = day.Date.ToString("yyyy-MM-dd"),
                Hour = hour,
                StartMinute = startMinute,
                EndMinute = 59,
                Duration = 60 - startMinute
            });
        }
    }

    return Results.Ok(availableSlots);
});
```

#### **🤔 Code Understanding Questions**:
1. What does `dayOffset` represent?
2. Why do we validate `dayOffset < 0 || dayOffset >= 7`?
3. What is the purpose of the `startMinute` variable?
4. What information does each available slot contain?

#### **Implementation Task 2: Get Day Summary**

Add this endpoint:

```csharp
// --- NEW ENDPOINT: GET /schedule/day/{dayOffset}/summary ---
// Gets a summary of all bookings for a specific day
eventGroup.MapGet("/day/{dayOffset}/summary", (int dayOffset, ScheduleData schedule) =>
{
    if (dayOffset < 0 || dayOffset >= 7)
    {
        return Results.BadRequest("Day offset must be between 0 and 6");
    }

    var day = schedule.Days[dayOffset];
    var events = EventActions.ListEvents();
    var dayBookings = new List<object>();

    // Find all events for this day
    foreach (var eventPair in events)
    {
        var eventData = eventPair.Value;
        if (eventData.Date.Date == day.Date.Date)
        {
            dayBookings.Add(new
            {
                EventId = eventPair.Key,
                Date = eventData.Date.ToString("yyyy-MM-dd"),
                Hour = eventData.Hour,
                StartMinute = eventData.StartMinute,
                EndMinute = eventData.EndMinute,
                Duration = eventData.EndMinute - eventData.StartMinute,
                TimeSlot = $"{eventData.Hour:D2}:{eventData.StartMinute:D2} - {eventData.Hour:D2}:{eventData.EndMinute:D2}"
            });
        }
    }

    return Results.Ok(new
    {
        Date = day.Date.ToString("yyyy-MM-dd"),
        DayOfWeek = day.Date.DayOfWeek.ToString(),
        TotalBookings = dayBookings.Count,
        Bookings = dayBookings.OrderBy(b => ((dynamic)b).Hour).ThenBy(b => ((dynamic)b).StartMinute)
    });
});
```

#### **🤔 Code Understanding Questions**:
1. How does this endpoint find events for a specific day?
2. What information is included in the summary?
3. Why do we order the bookings by Hour and then StartMinute?

### **Step 3.3: Create Frontend Integration (20 minutes)**

Now let's add JavaScript functions to call our new endpoints. Add these to your <mcfile name="test-endpoints.html" path="f:\Code-Projects\00-Study-00\Lexicon\Weeks\Week 10-12\Assignment\Scheduler-Radio\API\wwwroot\test-endpoints.html"></mcfile> file:

#### **Add New HTML Section**

Add this section before the "Test Results" section:

```html
<!-- Exercise 4: New Features -->
<div class="test-section">
    <h2>🆕 Exercise 4: New Features You Built</h2>
    <p>Test the new endpoints you just created!</p>
    
    <div class="endpoint">GET /schedule/available/{dayOffset}</div>
    <input type="number" id="available-day" placeholder="Day (0=today, 1=tomorrow)" style="width:200px;" value="0">
    <button onclick="getAvailableSlots()">Get Available Time Slots</button>
    <div id="available-result" class="result" style="display:none;"></div>

    <div class="endpoint">GET /schedule/day/{dayOffset}/summary</div>
    <input type="number" id="summary-day" placeholder="Day (0=today, 1=tomorrow)" style="width:200px;" value="0">
    <button onclick="getDaySummary()">Get Day Summary</button>
    <div id="summary-result" class="result" style="display:none;"></div>
</div>
```

#### **Add New JavaScript Functions**

Add these functions to the `<script>` section:

```javascript
// Function to get available time slots
async function getAvailableSlots() {
    const dayOffset = parseInt(document.getElementById('available-day').value);
    
    if (isNaN(dayOffset) || dayOffset < 0 || dayOffset > 6) {
        showResult('Available Slots', 'Please enter a valid day offset (0-6)', false);
        return;
    }

    try {
        const response = await fetch(`/schedule/available/${dayOffset}`);
        const data = await response.text();
        showResult(`Available Slots (Day ${dayOffset})`, data, response.ok);
    } catch (error) {
        showResult('Available Slots', 'Error: ' + error.message, false);
    }
}

// Function to get day summary
async function getDaySummary() {
    const dayOffset = parseInt(document.getElementById('summary-day').value);
    
    if (isNaN(dayOffset) || dayOffset < 0 || dayOffset > 6) {
        showResult('Day Summary', 'Please enter a valid day offset (0-6)', false);
        return;
    }

    try {
        const response = await fetch(`/schedule/day/${dayOffset}/summary`);
        const data = await response.text();
        showResult(`Day Summary (Day ${dayOffset})`, data, response.ok);
    } catch (error) {
        showResult('Day Summary', 'Error: ' + error.message, false);
    }
}
```

#### **🤔 Code Understanding Questions**:
1. Why do we validate the `dayOffset` in JavaScript?
2. What happens if the user enters an invalid day offset?
3. How is the response displayed to the user?

---

## 🧪 **PHASE 4: TESTING AND DEBUGGING**
*Time: 30 minutes*

### **Step 4.1: Systematic Testing (15 minutes)**

Now let's test everything systematically:

#### **Testing Checklist**
```
📝 TESTING CHECKLIST:
┌─────────────────────────────────────────────────────────────┐
│ Test each feature and record results:                       │
│                                                             │
│ ✅ Original Features:                                       │
│ □ Book a time slot (Hour: 10, Start: 0, End: 30)           │
│   Result: ___________________________________________       │
│                                                             │
│ □ List all events                                           │
│   Result: ___________________________________________       │
│                                                             │
│ □ Get specific event by ID                                  │
│   Result: ___________________________________________       │
│                                                             │
│ ✅ New Features:                                            │
│ □ Get available slots for today (Day 0)                    │
│   Result: ___________________________________________       │
│                                                             │
│ □ Get day summary for today (Day 0)                        │
│   Result: ___________________________________________       │
│                                                             │
│ □ Book another slot, then check available slots again      │
│   Result: ___________________________________________       │
│                                                             │
│ ✅ Edge Cases:                                              │
│ □ Try invalid day offset (Day 10)                          │
│   Result: ___________________________________________       │
│                                                             │
│ □ Try negative day offset (Day -1)                         │
│   Result: ___________________________________________       │
└─────────────────────────────────────────────────────────────┘
```

### **Step 4.2: Debug and Fix Issues (15 minutes)**

If you encounter any errors, use this debugging process:

#### **Debugging Checklist**
```
📝 DEBUGGING PROCESS:
┌─────────────────────────────────────────────────────────────┐
│ When something doesn't work:                                │
│                                                             │
│ 1. Check the Browser Console (F12):                        │
│    What error messages do you see? ___________________     │
│                                                             │
│ 2. Check the Network Tab:                                  │
│    What HTTP status code? ____________________________     │
│    What's in the response? ____________________________    │
│                                                             │
│ 3. Check the Server Console:                               │
│    Any error messages there? ___________________________   │
│                                                             │
│ 4. Verify Your Code:                                       │
│    Did you save all files? ____________________________    │
│    Are there any typos? ________________________________   │
│                                                             │
│ 5. Test Step by Step:                                      │
│    Does the endpoint exist? ____________________________   │
│    Are parameters correct? _____________________________   │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎓 **PHASE 5: UNDERSTANDING AND REFLECTION**
*Time: 20 minutes*

### 🖊️ **Pen & Paper Exercise 3: Connecting the Dots**

Now that you've built and tested everything, let's make sure you understand how it all works together:

#### **Step 5.1: Trace the Complete Flow (10 minutes)**

Pick one of your new features and trace the complete flow from user click to result display:

```
📝 COMPLETE FLOW TRACE:
┌─────────────────────────────────────────────────────────────┐
│ Feature: Get Available Time Slots                           │
│                                                             │
│ 1. User Action: ______________________________________      │
│                                                             │
│ 2. JavaScript Function Called: _______________________     │
│                                                             │
│ 3. HTTP Request Details:                                    │
│    Method: ___________________________________________      │
│    URL: ______________________________________________      │
│    Body: _____________________________________________      │
│                                                             │
│ 4. C# Endpoint Receives:                                    │
│    Parameters: ____________________________________        │
│                                                             │
│ 5. C# Code Processes:                                       │
│    What objects are accessed? _________________________    │
│    What calculations are done? _________________________   │
│                                                             │
│ 6. C# Returns:                                              │
│    Data format: ___________________________________        │
│    Success/Error: __________________________________       │
│                                                             │
│ 7. JavaScript Receives and Displays:                       │
│    How is data shown to user? __________________________   │
└─────────────────────────────────────────────────────────────┘
```

#### **Step 5.2: Identify Key Concepts (10 minutes)**

Answer these reflection questions:

```
📝 REFLECTION QUESTIONS:
┌─────────────────────────────────────────────────────────────┐
│ 1. What's the difference between GET and POST requests?     │
│    ___________________________________________________      │
│                                                             │
│ 2. How does ASP.NET Core know which C# method to call?     │
│    ___________________________________________________      │
│                                                             │
│ 3. What is "model binding" and how does it work?           │
│    ___________________________________________________      │
│                                                             │
│ 4. Why do we use async/await in JavaScript?                │
│    ___________________________________________________      │
│                                                             │
│ 5. How does the ScheduleData object keep track of          │
│    bookings?                                               │
│    ___________________________________________________      │
│                                                             │
│ 6. What would happen if two people tried to book the       │
│    same time slot at exactly the same time?                │
│    ___________________________________________________      │
└─────────────────────────────────────────────────────────────┘
```

---

## 🚀 **PHASE 6: EXTENSION CHALLENGES**
*Time: Optional - As much as you want!*

Ready for more? Try these challenges to extend your learning:

### **Challenge 1: Add Booking Validation**
Prevent bookings that:
- Are in the past
- Overlap with existing bookings
- Are longer than 4 hours
- Start or end outside business hours (6 AM - 11 PM)

### **Challenge 2: Add Show Information**
Extend bookings to include:
- Show name
- DJ name
- Show description
- Show category (Music, Talk, News, etc.)

### **Challenge 3: Add Recurring Bookings**
Allow DJs to book:
- Daily shows (same time every day)
- Weekly shows (same time every week)
- Custom patterns

### **Challenge 4: Add Conflict Resolution**
When there's a booking conflict:
- Show alternative available times
- Allow users to join a waiting list
- Send notifications when slots become available

### 🖊️ **Planning Your Extension**

Choose one challenge and plan it out:

```
📝 EXTENSION PLANNING:
┌─────────────────────────────────────────────────────────────┐
│ Challenge Chosen: ____________________________________      │
│                                                             │
│ What new properties/fields are needed?                      │
│ ___________________________________________________         │
│                                                             │
│ What new endpoints are needed?                              │
│ ___________________________________________________         │
│                                                             │
│ What changes to existing code are needed?                   │
│ ___________________________________________________         │
│                                                             │
│ What new JavaScript functions are needed?                   │
│ ___________________________________________________         │
│                                                             │
│ How will you test this?                                     │
│ ___________________________________________________         │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎯 **FINAL ASSESSMENT**

### **Knowledge Check**
Test your understanding by answering these questions without looking back:

1. **HTTP Methods**: When would you use GET vs POST vs PUT vs DELETE?

2. **Object Relationships**: Draw the ScheduleData object hierarchy from memory.

3. **API Design**: Design an endpoint to get all bookings for a specific DJ.

4. **Error Handling**: What should happen when a user tries to book a time slot that's already taken?

5. **JavaScript Async**: Explain why we need async/await when calling APIs.

### **Practical Skills Check**
Can you:
- [ ] Create a new endpoint from scratch?
- [ ] Write JavaScript to call your endpoint?
- [ ] Handle both success and error responses?
- [ ] Debug issues using browser developer tools?
- [ ] Explain how data flows from frontend to backend and back?

### **Problem-Solving Check**
- [ ] Can you break down a complex feature into smaller tasks?
- [ ] Can you plan your implementation before coding?
- [ ] Can you identify potential issues before they occur?
- [ ] Can you think of ways to improve the existing system?

---

## 🎉 **CONGRATULATIONS!**

You've successfully built a complete understanding of how web applications work! You've learned:

✅ **HTTP Communication**: How browsers and servers talk to each other
✅ **Object Manipulation**: How to work with complex data structures
✅ **API Design**: How to create endpoints that are easy to use
✅ **Full-Stack Development**: How frontend and backend work together
✅ **Testing and Debugging**: How to find and fix problems
✅ **Planning and Design**: How to think before you code

### **What's Next?**

Now that you understand the fundamentals, you're ready to:
- Build more complex applications
- Learn about databases and data persistence
- Explore authentication and security
- Try different frameworks and technologies
- Build your own projects from scratch

### **Keep Learning!**

The best way to solidify your learning is to:
1. **Practice**: Build small projects regularly
2. **Experiment**: Try new features and see what happens
3. **Read Code**: Look at other people's projects
4. **Ask Questions**: Never stop being curious
5. **Teach Others**: Explaining concepts helps you understand them better

---

## 📚 **REFERENCE MATERIALS**

### **Key Concepts Summary**

**HTTP Methods**:
- `GET`: Retrieve data (read-only)
- `POST`: Send data to create something new
- `PUT`: Send data to update something existing
- `DELETE`: Remove something

**C# Endpoint Patterns**:
```csharp
// GET with route parameter
app.MapGet("/api/items/{id}", (int id) => { ... });

// POST with JSON body
app.MapPost("/api/items", (ItemModel item) => { ... });

// GET with query parameters
app.MapGet("/api/search", (string query, int page) => { ... });
```

**JavaScript Fetch Patterns**:
```javascript
// GET request
const response = await fetch('/api/items');
const data = await response.json();

// POST request
const response = await fetch('/api/items', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(itemData)
});
```

### **Common Error Messages and Solutions**

| Error | Cause | Solution |
|-------|-------|----------|
| 404 Not Found | Wrong URL or endpoint doesn't exist | Check URL spelling and endpoint registration |
| 400 Bad Request | Invalid parameters or data format | Validate input data and parameter types |
| 500 Internal Server Error | Exception in C# code | Check server console for detailed error |
| CORS Error | Cross-origin request blocked | Configure CORS in ASP.NET Core |
| JSON Parse Error | Invalid JSON format | Validate JSON structure |

---

*Remember: The goal isn't to memorize everything, but to understand the patterns and principles. With these fundamentals, you can learn any web technology!*