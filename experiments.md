# START OF TOO EASY SAMPLES #
>> SUPERFLUOUS: VERY COMMON KNOWLEDGE
>> THESE SAMPLES ARE TOO SIMPLE AND DOESN'T REALLY LEAD TO DEEPER UNDERSTANDING.
6.  **Intentionally cause a "Not Found" error.**
    *   **What it is:** See how the app handles a URL that doesn't exist.
    *   **How:** Change the URL to `fetch("http://localhost:5219/this-does-not-exist")`.
    *   **Why it works:** Your C# server doesn't have an endpoint for that URL, so it will send back a `404 Not Found` error. Your JavaScript's `if (!response.ok)` block is built to catch this and will show the error message you wrote.

7.  **Simulate a "server is offline" error.**
    *   **What it is:** See what happens if the backend server isn't running.
    *   **How:** Stop the C# application, then try to click the "Load from API" button on your webpage.
    *   **Why it works:** The `fetch` call will fail at a network level before it can even get a `404` response. This will trigger the `catch (err)` block, showing you a different kind of error message (like "Failed to fetch").
5.  **Get the whole week's schedule.**
    *   **What it is:** Request the `/schedule` endpoint instead of `/schedule/today`.
    *   **How:** Change `fetch("http://localhost:5219/schedule/today")` to `fetch("http://localhost:5219/schedule")`.
    *   **Why it works:** You're pointing the request to a different URL in the C# backend. This endpoint is programmed to send back the entire `schedule` object with all 7 days. The raw JSON in your `<pre>` tag will become much larger. The simple list might break, because it's expecting a single day's `hours` array, not a `days` array!

# END OF TOO SIMPLE SAMPLES #    

---

### Part 1: JavaScript Experiments (The Dashboard & Remote Control)

These changes happen in `script.js` and will affect how your web page *behaves* and *displays* information after you click the button.

# START OF FINISHED EXPERIMENTS #
>> DONE
>> THESE ARE FINISHED. FAIRLY SIMPLE BUT 
>> PERHAPS TOO LITTLE COMPLEXITY IN TERMS OF 
>> WHAT UNDERSTANDING THEY LEAD TO. 
#### A. Seeing What's Happening: The Magic of `console.log()`
Your browser's Developer Console (press F12) is your best friend. It's like a secret window into your code.

1.  **Is the script even running?**
    *   **What it is:** A simple check to see if your `script.js` file is loaded.
    *   **How:** Add `console.log("Hello from script.js!");` to the very top of the file.
    *   **Why it works:** This code runs the moment the browser loads the script. If you see the message in your console, you know the file is connected properly.

2.  **Confirm the button is wired up.**
    *   **What it is:** Check that your `click` listener is working.
    *   **How:** Add `console.log("Button was clicked!");` as the first line inside the `btnLoad.addEventListener` function.
    *   **Why it works:** This will only print its message when the specific event (a click on `btnLoad`) happens.

3.  **Peek at the raw data.**
    *   **What it is:** See the raw data object you get from the server.
    *   **How:** Add `console.log(payload);` right after the `const payload = await response.json();` line.
    *   **Why it works:** `payload` is the JavaScript object that was converted from the JSON text the server sent. The console lets you expand and inspect it, showing you its structure (like the `hours` array, and the `minutes` array inside each hour).


4.  **Inspect one "hour" at a time.**
    *   **What it is:** Look at each hourly chunk as the code loops through them.
    *   **How:** Inside the `payload.hours.forEach(h => { ... });` loop, add `console.log(h);` as the first line.
    *   **Why it works:** This will print each `h` object as the loop processes it. You'll see 24 separate log messages, letting you examine each hour's data individually.

#### B. Changing What You Ask For: The `fetch()` Call

# END OF FINISHED EXPERIMENTS #



>> NOT DONE
#### C. Manipulating the Data Before Display

8.  **Show only the afternoon schedule.**
    *   **What it is:** Filter the data *after* you receive it.
    *   **How:** After `const payload = await response.json();`, add this line: `payload.hours = payload.hours.filter(h => h.hour >= 12);`.
    *   **Why it works:** The `.filter()` array method creates a *new* array containing only the elements that pass a test. Here, we're keeping only the hours where the `hour` property is 12 or greater. The rest of the code then renders a list using this smaller, filtered array.

9.  **Reverse the schedule order.**
    *   **What it is:** Display the hours from 23:00 down to 00:00.
    *   **How:** After `payload.hours = payload.hours || ...`, add this line: `payload.hours.reverse();`.
    *   **Why it works:** The `.reverse()` method reverses the order of elements in an array in place. The `forEach` loop will now go through the hours from last to first.

10. **Find the first booked slot.**
    *   **What it is:** Instead of a list, just display a single piece of information.
    *   **How:** Replace the entire `payload.hours.forEach(...)` block with this:
        ```javascript
        const firstBooked = payload.hours.find(h => h.minutes.some(m => m));
        if (firstBooked) {
          list.innerHTML = `<li>The first booked slot is at ${firstBooked.hour}:00</li>`;
        } else {
          list.innerHTML = `<li>No slots are booked today!</li>`;
        }
        ```
    *   **Why it works:** `.find()` returns the *first element* in an array that matches a condition. `.some()` checks if *any* element in an array matches a condition. So this code finds the first hour that has at least one booked minute (`m` is `true`) and reports it.

#### D. Changing How Things Look: The Rendering Logic

11. **Don't clear the list on each click.**
    *   **What it is:** See what happens if you keep adding to the list.
    *   **How:** Comment out or delete the line `list.innerHTML = "";`.
    *   **Why it works:** Normally, the list is cleared to make way for the new data. Without this line, clicking the button repeatedly will just append the same schedule to the bottom of the list over and over.

12. **Change the "Booked" / "Free" text.**
    *   **What it is:** Simple text change for the status.
    *   **How:** Change `status.textContent = booked ? "Booked" : "Free";` to `status.textContent = booked ? "🔴 On Air" : "⚪️ Off Air";`.
    *   **Why it works:** You're just changing the string values used in the ternary operator. You can even use emojis!

13. **Add a CSS class based on status.**
    *   **What it is:** Make "Booked" and "Free" slots look different (e.g., different colors).
    *   **How:** Add this line right after you create the `li` element: `li.className = booked ? "booked-slot" : "free-slot";`. (You'll also need to add `.booked-slot { color: red; }` and `.free-slot { color: green; }` to your `style.css` file).
    *   **Why it works:** This dynamically adds a class to the `<li>` element. CSS can then use these classes to apply specific styles, making your UI much more readable.

14. **Show the number of minutes booked.**
    *   **What it is:** Instead of just "Booked," show *how much* of the hour is booked.
    *   **How:** Change `status.textContent = booked ? "Booked" : "Free";` to:
        ```javascript
        const bookedMinutes = h.minutes.filter(m => m).length;
        status.textContent = bookedMinutes > 0 ? `${bookedMinutes} mins booked` : "Free";
        ```
    *   **Why it works:** `h.minutes.filter(m => m)` creates an array of only the `true` values. `.length` then counts how many there are. This gives you a precise count of the booked minutes for that hour.

15. **Handle inconsistent C# property casing.**
    *   **What it is:** The code already has `(h.hour ?? h.Hour)`. This is to handle if the C# code sends `hour` (lowercase) or `Hour` (uppercase). Let's see it in action.
    *   **How:** In `ScheduleData.cs`, change `public int Hour { get; }` to `public int hour { get; }` in the `HourSchedule` class. Restart your C# app.
    *   **Why it works:** C# properties are usually `PascalCase` (like `Hour`), while JavaScript properties are `camelCase` (like `hour`). The `??` (Nullish Coalescing Operator) in JavaScript says "use the value on the left if it's not null/undefined, otherwise use the value on the right." Your code is robustly checking for either casing style, and this experiment proves it works.

---

### Part 2: C# Experiments (The Engine & The Data)

These changes happen in `Program.cs` and `ScheduleData.cs`. You must **stop and restart** your C# application every time you make a change here for it to take effect.

#### E. Changing the Default Data

16. **Start with two pre-booked slots.**
    *   **What it is:** Add a second default booking when the server starts.
    *   **How:** In `Program.cs`, duplicate the `ScheduleAction.SetBooking(...)` line and change the values:
        ```csharp
        ScheduleAction.SetBooking(schedule, DateTime.Today, 10, 15, 30, true);
        ScheduleAction.SetBooking(schedule, DateTime.Today, 18, 0, 60, true); // New line
        ```
    *   **Why it works:** The server now performs two booking actions on its in-memory `schedule` object right at startup. The 18:00 hour will now show as fully booked.

17. **Book a slot for tomorrow.**
    *   **What it is:** See how data for other days is handled.
    *   **How:** Change `DateTime.Today` to `DateTime.Today.AddDays(1)` in the `SetBooking` call.
    *   **Why it works:** You are now modifying the *second* `DaySchedule` object in the `Days` list. If your JavaScript is still fetching `/schedule/today`, you won't see this change. You'll need to change your JS `fetch` to `/schedule` to see the full 7-day data and find your booking on the next day.

18. **"Un-book" a slot instead.**
    *   **What it is:** The `SetBooking` method can also free up time.
    *   **How:** First, book a whole hour by setting minutes 0 to 60 as `true`. Then, un-book a 15-minute slot in the middle.
        ```csharp
        ScheduleAction.SetBooking(schedule, DateTime.Today, 9, 0, 60, true);   // Book 9:00-10:00
        ScheduleAction.SetBooking(schedule, DateTime.Today, 9, 15, 30, false); // Un-book 9:15-9:30
        ```
    *   **Why it works:** The `isBooked` parameter determines whether the loop sets the `Minutes` array values to `true` or `false`. This shows how you can use the same function for both booking and cancelling.

19. **Change the size of the schedule.**
    *   **What it is:** Generate a 3-day schedule instead of a 7-day one.
    *   **How:** In `ScheduleData.cs`, change `Enumerable.Range(0, 7)` to `Enumerable.Range(0, 3)`.
    *   **Why it works:** This LINQ statement is what generates the initial list of `DaySchedule` objects. Changing `7` to `3` means the `Days` list will only ever have three items in it.

20. **Run on a "work day" schedule.**
    *   **What it is:** Only generate hours from 9 AM to 5 PM.
    *   **How:** In `DaySchedule.cs`, change `Enumerable.Range(0, 24)` to `Enumerable.Range(9, 8)`.
    *   **Why it works:** `Enumerable.Range(start, count)` generates a sequence of numbers. By starting at `9` and asking for `8` numbers, you will generate `9, 10, 11, 12, 13, 14, 15, 16`—the hours for a 9-to-5 workday. Your JSON will now only contain these 8-hour objects per day.

#### F. Modifying and Creating API Endpoints

21. **Create a "Hello World" endpoint.**
    *   **What it is:** The simplest possible API endpoint.
    *   **How:** In `Program.cs`, before `app.Run()`, add: `app.MapGet("/hello", () => "Hello from the C# server!");`
    *   **Why it works:** You've told the server that if it gets a `GET` request for the URL `/hello`, it should just respond with that simple text string. You can test this by navigating your browser to `http://localhost:5219/hello`.

22. **Create an endpoint for tomorrow's schedule.**
    *   **What it is:** A new endpoint dedicated to tomorrow's data.
    *   **How:** Copy the entire `app.MapGet("/schedule/today", ...)` block and paste it below. Change the URL to `/schedule/tomorrow` and change the LINQ query to `...d.Date.Date == DateTime.Today.AddDays(1))`.
    *   **Why it works:** You've registered a new URL that runs slightly different logic. It now searches the `Days` list for the entry where the date matches tomorrow.

23. **Create an endpoint that takes a parameter.**
    *   **What it is:** An endpoint where the user can specify which hour they want.
    *   **How:** Add this new endpoint in `Program.cs`:
        ```csharp
        app.MapGet("/schedule/today/{hour}", (int hour) =>
        {
            DaySchedule? today = schedule.Days.FirstOrDefault(d => d.Date.Date == DateTime.Today);
            if (today is null) return Results.NotFound();

            HourSchedule? specificHour = today.Hours.FirstOrDefault(h => h.Hour == hour);
            if (specificHour is null) return Results.NotFound($"No data for hour {hour}");

            return Results.Ok(specificHour);
        });
        ```
    *   **Why it works:** The `{hour}` in the URL is a route parameter. ASP.NET Core automatically matches it to the `int hour` argument in the function. The code then finds that specific hour and returns it. You can test this by browsing to `/schedule/today/10` or `/schedule/today/18`.

24. **Change the data "shape" from an endpoint.**
    *   **What it is:** Return data in a different structure than the official `DaySchedule` class.
    *   **How:** Change the `/schedule/today` endpoint's `Results.Ok(today)` to:
        ```csharp
        return Results.Ok(new {
            Message = "Here is today's data!",
            When = today.Date,
            AvailableSlots = today.Hours.Where(h => !h.Minutes.All(m => m)).ToList()
        });
        ```
    *   **Why it works:** You are creating a new, temporary "anonymous object" on the fly. The JSON serializer will convert this new shape into JSON. Your JavaScript will likely break because it's expecting a property named `hours`, but the JSON in the `<pre>` tag will show this new, custom structure. It shows that your API can send back anything, not just your defined data models.

25. **Change the `/book` endpoint's response.**
    *   **What it is:** Make the POST endpoint give a more descriptive success message.
    *   **How:** Change `return success ? Results.Ok(schedule) : ...` to `return success ? Results.Ok("Booking confirmed!") : ...`.
    *   **Why it works:** You're changing the successful result from the entire (and very large) `schedule` object to a simple string. This is more efficient. To test this, you'd need a tool like Postman or Insomnia to send a POST request, as a browser can only do GET requests from the address bar.