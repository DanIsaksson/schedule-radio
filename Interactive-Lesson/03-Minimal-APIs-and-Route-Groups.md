# 03. Minimal APIs and Route Groups

## What you’ll learn
- Describe what a "Minimal API" endpoint is (it's just a method!).
- Explain how route groups (`MapGroup`) act like folders.
- Read and write **Arrow Functions** (`=>`) in C#.

## Prerequisites
- `01. Program.cs` – You know what the "Inventory" (DI) is.
- Basic C# Methods: `int Add(int a, int b) { return a + b; }`

---

## Mental model: The Address Book
Think of `API/Endpoints` as an **Address Book** for your server.
*   **URL** (`/db/event/list`) = The Name in the book.
*   **Handler** (The code block) = The Phone Number to call.

When a request comes in for `/db/event/list`, the app looks up the address, finds the code, runs it, and returns the result.

---

## 🟢 Green Coder Tips: Arrow Functions (`=>`)
In Console Apps, you write methods with names. In Minimal APIs, we often use "nameless" methods (Lambdas) because they are short.

**The Translation:**

**Standard Method:**
```csharp
public int GetCount(SchedulerContext db) 
{
    return db.Events.Count();
}
```

**Arrow Function (Lambda):**
```csharp
(SchedulerContext db) => db.Events.Count()
```
*   `(SchedulerContext db)`: The Input parameters (Inventory items).
*   `=>`: "Goes into..."
*   `db.Events.Count()`: The Code/Result.

It's the exact same logic, just less typing!

---

## Guided walkthrough

### 1. Find the event route group

1.  Open `API/Endpoints/EventDbEndpoints.cs`.
2.  Look for `app.MapGroup("/db/event")`.
    *   Think of this as creating a folder named `event`.
3.  Look for `.MapGet("/list", ...)`.
    *   This puts a file named `list` inside the `event` folder.
    *   Full path: `/db/event/list`.

### 2. See Dependency Injection in action

1.  Pick the list endpoint:
    ```csharp
    group.MapGet("/list", (SchedulerContext db) => ...);
    ```
2.  Notice `(SchedulerContext db)`.
    *   You **didn't** write `new SchedulerContext()`.
    *   The app looked in its **Inventory** (from Program.cs), found the database connection, and handed it to you automatically.
    *   This is the magic of DI!

---

## Try it – add a count endpoint

Goal: Add a simple read‑only endpoint that returns how many events exist.

1.  In `EventDbEndpoints`, within the `/db/event` group, add this code:
    ```csharp
    // Input: SchedulerContext (from inventory)
    // Output: int (the count)
    group.MapGet("/count", (SchedulerContext db) => db.Events.Count());
    ```
2.  Run the API project.
3.  Visit `http://localhost:5219/db/event/count` in your browser.
4.  Create or delete some bookings in the UI, then refresh this page. The number should change!

---

## Check yourself

-   If you see `(int id, SchedulerContext db) => ...`:
    -   Where does `id` come from? (Hint: The URL, like `/event/5`)
    -   Where does `db` come from? (Hint: The Inventory/DI)
-   Why do we use `MapGroup`? (Imagine if every single file on your computer was on the Desktop... messy, right? Groups are folders.)

---

## 📚 External Resources

**For C# Syntax:**
*   [Microsoft Learn - Lambda expressions (=>)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) (Ignore the complex stuff, just look at the syntax)

**Next lesson:** `04. Booking – Database CRUD Endpoints`
You’ll use these patterns to build the "Create Booking" feature.
