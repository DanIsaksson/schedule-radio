# 🎯 Exercise 1: Building Your First Simple Endpoints

## 📝 Learning Goals
- Understand how to create basic MapGet endpoints
- Learn to test endpoints with your browser
- Practice returning different types of data
- Build confidence with simple, working examples

---

## 🏗️ Part 1: The "Hello World" of Endpoints

Let's start with the simplest possible endpoint to understand the pattern.

### Step 1: Create a Basic Test File
Create a new file called `SimpleTestEndpoints.cs` in your `API` folder:

```csharp
// SimpleTestEndpoints.cs
// This is our playground for learning endpoints

namespace SimpleTests
{
    public static class SimpleTestEndpoints
    {
        public static void MapSimpleEndpoints(this WebApplication app)
        {
            // Your first endpoint - just returns text
            app.MapGet("/hello", () => "Hello World!");
            
            // Your second endpoint - returns a number
            app.MapGet("/number", () => 42);
            
            // Your third endpoint - returns true/false
            app.MapGet("/isworking", () => true);
        }
    }
}
```

### Step 2: Add It to Your Program.cs
Add this line to your `Program.cs` file, right before `app.Run();`:

```csharp
// Add our simple test endpoints
app.MapSimpleEndpoints();
```

### Step 3: Test Your Endpoints
1. Start your API: `dotnet run`
2. Open your browser and try these URLs:
   - `http://localhost:5219/hello` → Should show "Hello World!"
   - `http://localhost:5219/number` → Should show 42
   - `http://localhost:5219/isworking` → Should show true

✅ **Milestone 1 Complete!** You just created 3 working endpoints!

---

## 🔍 Part 2: Understanding the Pattern

Let's break down what we just did:

```csharp
app.MapGet("/hello", () => "Hello World!");
```

- `app` - Your web application
- `.MapGet` - Says "when someone visits this URL with GET"
- `"/hello"` - The URL path (what comes after localhost:5219/)
- `() => "Hello World!"` - What to return (a lambda function)

**Key Concept:** The part after `=>` is what gets sent back to the browser!

---

## 🧪 Part 3: Experiment Time!

Try these modifications to understand how it works:

### Experiment 1: Change the Messages
```csharp
app.MapGet("/hello", () => "Hi there!");  // Changed from "Hello World!"
app.MapGet("/number", () => 100);          // Changed from 42
```

**Test:** Restart your API and visit the URLs again.

### Experiment 2: Add Math
```csharp
app.MapGet("/math", () => 5 + 3);          // Returns 8
app.MapGet("/message", () => "The answer is " + (2 + 2)); // Returns "The answer is 4"
```

### Experiment 3: Use Variables
```csharp
app.MapGet("/variable", () => 
{
    string name = "Radio Station";
    int shows = 24;
    return $"Welcome to {name}! We have {shows} hours of content.";
});
```

✅ **Milestone 2 Complete!** You understand how to modify what endpoints return!

---

## 📊 Part 4: Return Schedule-Related Data

Now let's connect this to your radio schedule. Add these endpoints:

```csharp
// Return today's date
app.MapGet("/today", () => DateTime.Today.ToString("yyyy-MM-dd"));

// Return current time
app.MapGet("/now", () => DateTime.Now.ToString("HH:mm:ss"));

// Return a simple schedule fact
app.MapGet("/schedulefact", () => "A radio schedule has 24 hours in a day");

// Return how many days in your schedule
app.MapGet("/days", () => "7 days");
```

**Test these URLs:**
- `http://localhost:5219/today` → Shows today's date
- `http://localhost:5219/now` → Shows current time
- `http://localhost:5219/schedulefact` → Shows schedule fact
- `http://localhost:5219/days` → Shows "7 days"

---

## 🎯 Part 5: Challenge - Create Your Own Endpoints

**Challenge:** Create 3 new endpoints that return:
1. Your favorite radio show name
2. The current hour (0-23)
3. A random number between 1 and 10

**Hints:**
```csharp
// For random number:
Random rand = new Random();
int randomNum = rand.Next(1, 11); // 1 to 10

// For current hour:
int hour = DateTime.Now.Hour;
```

**Solution:**
```csharp
app.MapGet("/favoriteshow", () => "Morning Radio Show");
app.MapGet("/currenthour", () => DateTime.Now.Hour);
app.MapGet("/random", () => new Random().Next(1, 11));
```

---

## 🧪 Final Experiment: Combine with Existing Endpoints

Try adding your simple endpoints to your main Program.cs file. You can have both the simple ones and the complex schedule ones:

```csharp
// Add these BEFORE app.MapScheduleEndpoints();
app.MapGet("/hello", () => "Hello World!");
app.MapGet("/today", () => DateTime.Today.ToString("yyyy-MM-dd"));

// Then add your existing endpoints
app.MapScheduleEndpoints();
app.MapEventEndpoints();
```

---

## ✅ What You Learned

1. **MapGet Pattern:** `app.MapGet("URL", () => what_to_return)`
2. **Testing:** Visit URLs in your browser to see results
3. **Return Types:** Can return text, numbers, true/false
4. **Variables:** Can use variables and calculations
5. **Experimentation:** Try changing things and see what happens!

---

## 🚀 Next Steps

Once you're comfortable with these simple endpoints, you're ready for **Exercise 2** where we'll:
- Learn MapPost endpoints
- Practice sending data TO the server
- Build on what you learned here

**Remember:** The key is to start simple, test often, and gradually add complexity!