# 02. EF Core – DbContext, DbSet, and EventEntity

## Introduction
This lesson covers the **Database Layer**. You will learn how the code defines the database structure using Entity Framework Core (EF Core).

**Linked Code**:
- `API/Data/SchedulerContext.cs`
- `API/Models/EventEntity.cs`

---

## [Db.Schema.1] Database Context
The `SchedulerContext` class is the **bridge** between your C# code and the SQLite database.
- It inherits from `DbContext` (a class provided by EF Core).
- It acts as a "Session" with the database.

---

## [Db.Schema.2] Constructor
The constructor receives options (like "Use SQLite" and the connection string) from `Program.cs` and passes them to the base class.
- This is how the context knows *which* database to connect to.

---

## [Db.Schema.3] Tables (DbSet)
`DbSet<T>` properties represent **Tables**.
- `public DbSet<EventEntity> Events { get; set; }`
- This line tells EF Core: "Please create a table named `Events` and store `EventEntity` objects in it."

---

## [Db.Schema.4] Event Entity
The `EventEntity` class represents a **single row** in the `Events` table.
- This is a "POCO" (Plain Old CLR Object). It's just a class with properties.
- EF Core reads this class to decide what columns to create.

---

## [Db.Schema.5] Primary Key
Every table needs a unique identifier for each row.
- **Convention**: If you name a property `Id`, EF Core automatically makes it the Primary Key.

---

## [Db.Schema.6] Data Properties
These properties map 1:1 to columns in the database.
- `Date`: When the booking is.
- `Hour`: Which hour (0-23).
- `StartMinute` / `EndMinute`: The time range.

### Why Exclusive EndMinute?
We use `[Start, End)` logic.
- Start is inclusive (the booking starts at this minute).
- End is exclusive (the booking stops *before* this minute).
- Example: 15 to 30 means minutes 15, 16, ... 29. Minute 30 is free for the next booking.

---

## Try it – Add a Property
1.  Add `public string? HostName { get; set; }` to `EventEntity.cs`.
2.  Run the app.
3.  EF Core will (if configured) update the database or you might need to delete `scheduler.db` to let `EnsureCreated()` rebuild it with the new column.