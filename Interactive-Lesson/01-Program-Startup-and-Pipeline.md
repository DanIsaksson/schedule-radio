# 01. Program Startup and Pipeline

## Introduction
This lesson covers the **Backend Startup** process. You will learn how the application boots up, connects to the database, and prepares to handle requests.

**Linked Code**: `API/Program.cs`

---

## [Startup.Backend.1] Main Switch
The `Program.cs` file is the "Main Switch" or entry point. In modern .NET (Minimal APIs), this is where everything happens.

### Key Concepts
- **Top-Level Statements**: No `public class Program` or `void Main` boilerplate. The code just runs from top to bottom.
- **Builder Pattern**: We create a "builder", configure it, and then "build" the app.

---

## [Startup.Backend.2] Register Services
Before we can build the app, we must register the "ingredients" (Services) into the Dependency Injection (DI) container.

### What is Dependency Injection?
Think of it like a restaurant kitchen. The chef (Endpoint) doesn't go out and buy eggs (Database) every time they cook. The kitchen manager (DI Container) provides the eggs when asked.

---

## [Startup.Backend.3] CORS Policy
**CORS** (Cross-Origin Resource Sharing) is a security feature in browsers.
- **Problem**: Your React app runs on `localhost:5173`. Your API runs on `localhost:5219`. Browsers block this by default.
- **Solution**: We tell the API to "Allow" requests from the React origin.

---

## [Startup.Backend.4] Database Context
We use **Entity Framework Core (EF Core)** to talk to the database.
- `AddDbContext<SchedulerContext>`: Tells the app "We have a database represented by `SchedulerContext`".
- `UseSqlite`: Tells EF Core "The physical database is a SQLite file".

---

## [Startup.Backend.5] Legacy & Utility Services
- **Swagger**: A tool that generates a testing website for your API.
- **ScheduleData**: A singleton class used for the old in-memory system.

---

## [Startup.Backend.6] Build the App
`builder.Build()` is the moment of truth. It validates the configuration and creates the `app` object.

---

## [Startup.Backend.7] Database Initialization
We want the database to exist automatically when you run the app.
- `EnsureCreated()`: Checks if `scheduler.db` exists. If not, it creates it and creates all the tables.

---

## [Startup.Backend.8] Middleware Pipeline
The **Pipeline** is a conveyor belt. Every HTTP request travels through it.
1.  **DefaultFiles**: If you ask for `/`, give you `index.html`.
2.  **StaticFiles**: If you ask for `style.css`, give you the file.
3.  **Cors**: Check if you are allowed to be here.

---

## [Startup.Backend.9] Map Endpoints
We map "Doors" (URLs) to code.
- `MapScheduleDbEndpoints()`: Registers URLs like `/db/schedule/today`.
- This keeps `Program.cs` clean by moving the actual logic to other files.

---

## [Startup.Backend.10] Legacy Demo Endpoint
This is a raw example of an endpoint defined right here in `Program.cs`.
- `MapPost("/schedule/book", ...)`: When a POST request hits this URL, run this function.

---

## [Startup.Backend.11] Run
`app.Run()` starts the web server. The app is now alive and waiting for requests.
